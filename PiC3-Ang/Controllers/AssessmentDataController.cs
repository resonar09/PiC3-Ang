using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Diagnostics;
using PiC3_Ang.Models;

namespace PiC3_Ang.Controllers
{
    [Route("api/[controller]")]
    public class AssessmentDataController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public AssessmentDataController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("[action]/{id?}")]
        public async Task<IEnumerable<AssessmentReviewDto>> GetAssessments(int? id)
        {
            try
            {

                CoreServiceDevReference.CoreServiceClient coreServiceClient = new CoreServiceDevReference.CoreServiceClient();
                if (await IsAlive())
                {
                    var clientAssessments = await coreServiceClient.GetClientAssessmentsForReview_NEWAsync(null, 54338, null, null);
                    
                    var clientAsses = clientAssessments
                        .Select(x => new AssessmentReviewDto
                        {
                            Assessment = x.AssessmentForm.Assessment.Name + " " + x.AssessmentForm.Name,
                            ClientName = x.Client.FirstName + " " + x.Client.LastName,
                            LastUpdated = x.TestDate,
                            StatusKey = x.StatusKey
                        });
                    if(id > 0)
                    {
                        clientAsses = clientAsses.Where(x => x.StatusKey == id);
                    }
                    return clientAsses;
                }
                else if(_hostingEnvironment.IsDevelopment())
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var JSON = System.IO.File.ReadAllText(webRootPath + "/data/clientAssessments.json");
                    var newClientAsses = JsonConvert.DeserializeObject<IEnumerable<AssessmentReviewDto>>(JSON);
                    if (id > 0)
                    {
                        newClientAsses = newClientAsses.Where(x => x.StatusKey == id);
                    }
                    return newClientAsses;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        [HttpGet("[action]/{id?}")]
        public async Task<IEnumerable<AssessmentReviewDto>> GetAssessmentsByStatus(int id)
        {
            try
            {
                CoreServiceDevReference.CoreServiceClient coreServiceClient = new CoreServiceDevReference.CoreServiceClient();
                if (await IsAlive())
                {
                    var clientAssessments = await coreServiceClient.GetClientAssessmentsForReview_NEWAsync(null, 54338, null, null); //54338

                    var clientAsses = clientAssessments
                        .Select(x => new AssessmentReviewDto
                        {
                            Assessment = x.AssessmentForm.Assessment.Name + " " + x.AssessmentForm.Name,
                            ClientName = x.Client.FirstName + " " + x.Client.LastName,
                            LastUpdated = x.TestDate,
                            StatusKey = x.StatusKey
                        });
                    if (id > 0)
                    {
                        clientAsses= clientAsses.Where(x => StatusTypes.GetCompletedStatuses(true).Contains(x.StatusKey));
                    }
                    else
                    {
                        clientAsses = clientAsses.Where(x => StatusTypes.GetCompletedStatuses(false).Contains(x.StatusKey));
                    }
                    return clientAsses;
                }
                else if (_hostingEnvironment.IsDevelopment())
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var JSON = System.IO.File.ReadAllText(webRootPath + "/data/clientAssessments.json");
                    var newClientAsses = JsonConvert.DeserializeObject<IEnumerable<AssessmentReviewDto>>(JSON);
                    if (id > 0)
                    {
                        var statusTypeList = StatusTypes.GetCompletedStatuses(true).ToArray();

                        Debug.WriteLine(statusTypeList);
                        //newClientAsses = newClientAsses.Where(x => statusTypeList.Contains(x.StatusKey));
                        //newClientAsses = newClientAsses.Where(x => x.StatusKey == 12);

                        return newClientAsses.Where(x => statusTypeList.Contains(x.StatusKey));
                    }
                    else
                    {
                        var statusTypeList = StatusTypes.GetCompletedStatuses(false).ToArray();
                        Debug.WriteLine(statusTypeList);
                        //newClientAsses = newClientAsses.Where(x => x.StatusKey == 6);
                        return newClientAsses.Where(x => statusTypeList.Contains(x.StatusKey));

                    }
                    return newClientAsses;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet("[action]")]
        public string GetAssessmentsMock()
        {
            //if (_hostingEnvironment.IsDevelopment())
            //{ }
            string webRootPath = _hostingEnvironment.WebRootPath;
            var JSON = System.IO.File.ReadAllText(webRootPath + "/data/clientAssessments.json");
            return JSON;
        }
        [HttpGet("[action]")]
        public string GetString()
        {
            return "test";
        }
        [HttpGet("[action]")]
        public async Task<bool> IsAlive()
        {
            CoreServiceDevReference.CoreServiceClient coreServiceClient = new CoreServiceDevReference.CoreServiceClient();
            try
            {
                await coreServiceClient.HelloWorldAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
    public class AssessmentReviewDto
    {
        private int _statusKey;
        private string _status;
        private bool _completed;

        public string Assessment { get; set; }
        public string ClientName { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Status
        {
            get
            {
                return _status;
            }
        }
        public int StatusKey
        {
            get
            {
                return _statusKey;
            }
            set
            {
                _statusKey = value;
                _status = ClientAssessmentStatus.GetStatus(value);
                _completed = ClientAssessmentStatus.GetCompleted(value);
            }
        }

        public bool Completed
        {
            get
            {
                return _completed;
            }

        }

    }

    public sealed class ClientAssessmentStatus
    {

        private readonly String name;
        private readonly int value;

        public static readonly ClientAssessmentStatus PaperPencilEntryIncomplete = new ClientAssessmentStatus(1, "Not Completed");
        public static readonly ClientAssessmentStatus PaperPencilEntryCompletedScored = new ClientAssessmentStatus(6, "Completed, Scored 6");
        public static readonly ClientAssessmentStatus AdminOnsiteEntryCompletedScored = new ClientAssessmentStatus(12, "Completed, Scored 12");

        private ClientAssessmentStatus(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }
        public static string GetStatus(int statusKey)
        {
            string status = "";
            switch (statusKey)
            {
                case 1:
                    status = ClientAssessmentStatus.PaperPencilEntryIncomplete.ToString();
                    break;
                case 6:
                    status = ClientAssessmentStatus.PaperPencilEntryCompletedScored.ToString();
                    break;
                case 12:
                    status = ClientAssessmentStatus.AdminOnsiteEntryCompletedScored.ToString();
                    break;
                default:
                    status = "Unknown Status";
                    break;
            }
            return status;
        }
        public static bool GetCompleted(int statusKey)
        {
            bool completed = true;
            switch (statusKey)
            {
                case 1:
                case 6:
                case 12:
                    completed = true;
                    break;
                default:
                    completed = false;
                    break;
            }
            return completed;
        }
    }

}
