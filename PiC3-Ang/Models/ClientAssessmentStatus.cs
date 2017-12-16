using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiC3_Ang.Models
{
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


        //public static int[] GetCompletedStatuses(bool isCompleted)
        //{
        //    List<int> statusList = new List<int>();
        //    if (isCompleted)
        //    {
        //        statusList.Add(ClientAssessmentStatus.PaperPencilEntryCompletedScored.value);
        //        statusList.Add(ClientAssessmentStatus.AdminOnsiteEntryCompletedScored.value);
        //    }
        //    else
        //    {
        //        statusList.Add(ClientAssessmentStatus.PaperPencilEntryIncomplete.value);
        //    }
        //    return statusList.ToArray();
        //}
    }
}
