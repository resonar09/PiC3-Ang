using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiC3_Ang.Models
{
    public sealed class StatusTypes
    {

        private readonly String name;
        private readonly int value;

        public static readonly StatusTypes PaperPencilEntryIncomplete = new StatusTypes(13, "Not Completed");
        public static readonly StatusTypes PaperPencilEntryCompletedScored = new StatusTypes(6, "Completed, Scored 6");
        public static readonly StatusTypes AdminOnsiteEntryCompletedScored = new StatusTypes(12, "Completed, Scored 12");




        private StatusTypes(int value, String name)
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

                case 6:
                    status = StatusTypes.PaperPencilEntryCompletedScored.ToString();
                    break;
                case 12:
                    status = StatusTypes.AdminOnsiteEntryCompletedScored.ToString();
                    break;
                case 13:
                    status = StatusTypes.PaperPencilEntryIncomplete.ToString();
                    break;
                default:
                    status = "Not Completed";
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


        public static List<int> GetCompletedStatuses(bool isCompleted)
        {
            List<int> statusList = new List<int>();
            if (isCompleted)
            {
                statusList.Add(StatusTypes.PaperPencilEntryCompletedScored.value);
                statusList.Add(StatusTypes.AdminOnsiteEntryCompletedScored.value);
            }
            else
            {
                statusList.Add(StatusTypes.PaperPencilEntryIncomplete.value);
            }
            return statusList;
        }
    }
}
