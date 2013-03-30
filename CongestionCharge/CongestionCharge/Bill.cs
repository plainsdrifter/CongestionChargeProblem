using System;
using CongestionCharge.Enums;

namespace CongestionCharge
{
    public class Bill
    {
        public Vehicle Vehicle { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime LeaveDate { get; set; }

        public Bill()
        {
            Vehicle = Vehicle.Unknown;
        }

        public Bill(Vehicle vehicle, DateTime entryDate, DateTime leaveDate)
        {
            Vehicle = vehicle;
            EntryDate = entryDate;
            LeaveDate = leaveDate;
        }
    }
}
