using PiC3_Ang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiC3_Ang.Dtos
{
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
}
