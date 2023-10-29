using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.Constants.Enum
{
    public class StatusEnum
    {
        public enum ProjectStatus
        {
            Active = 0,
            Deactive = 1,
        }

        public enum ProjectUserType
        {
            Member = 0,
            PM = 1,
            DeActive = 3,
        }

        public enum TaskType
        {
            Task = 0,
            Bug = 1,
        }

        public enum PriorityType
        {
            VeryLow = 0,
            Low = 1,
            High = 2,
            VeryHigh = 3,
        }

        public enum Sex : byte
        {
            Male = 0,
            Female = 1
        }

        public enum ColumnType
        {
            Ticket = 0,
            Task = 1
        }
    }
}
