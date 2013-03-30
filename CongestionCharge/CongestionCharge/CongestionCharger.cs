using System;
using CongestionCharge.Enums;
using CongestionCharge.Utils;

namespace CongestionCharge
{
    public class CongestionCharger
    {
        public static Charge Charge(Bill bill)
        {
            var charge = new Charge();
            var amRate = 0f;
            var pmRate = 0f;

            switch (bill.Vehicle)
            {
                case Vehicle.Motorbike:
                    amRate = pmRate = 1f;
                    break;
                case Vehicle.Car:
                case Vehicle.Van:
                    amRate = 2f;
                    pmRate = 2.5f;
                    break;
            }

            ChargeForBillableHours(ref charge, bill, amRate, pmRate);

            return charge;
        }

        private static void ChargeForBillableHours(ref Charge charge, Bill bill, float amRate, float pmRate)
        {
            var chargeOverlap = false;
            var dayOverlap = false;

            for (var date = bill.EntryDate; date < bill.LeaveDate; date = date.AddHours(1))
            {
                if (date.IsWeekend() || date.IsFreeOfCharge())
                {
                    dayOverlap = true;
                    continue;
                }

                var epsilon = 60f;

                if ((bill.LeaveDate - date).TotalHours < 1)
                    epsilon = (float)(bill.LeaveDate - date).TotalMinutes;

                TimeSpan overlap;
                TimeSpan margin;
                if (date.InAmRate())
                {
                    overlap = (date.TimeOfDay - TimeSpan.FromHours(7));
                    if (overlap.TotalHours < 1 && dayOverlap)
                    {
                        epsilon += (float) overlap.TotalMinutes;
                        dayOverlap = false;
                    }

                    margin = TimeSpan.FromHours(12) - date.TimeOfDay;
                    if (margin.TotalHours < 1)
                    {
                        epsilon = (float) margin.TotalMinutes;
                        chargeOverlap = true;
                    }

                    charge.AmRateSpan = charge.AmRateSpan.Add(TimeSpan.FromMinutes(epsilon));
                    charge.AmRateCharge += (amRate*(epsilon/60f));
                }

                if (date.InPmRate())
                {
                    overlap = date.TimeOfDay - TimeSpan.FromHours(12);
                    if (overlap.TotalHours < 1 && chargeOverlap)
                    {
                        epsilon += (float) overlap.TotalMinutes;
                        chargeOverlap = false;
                    }

                    margin = TimeSpan.FromHours(19) - date.TimeOfDay;
                    if (margin.TotalHours < 1)
                        epsilon = (float) margin.TotalMinutes;

                    charge.PmRateSpan = charge.PmRateSpan.Add(TimeSpan.FromMinutes(epsilon));
                    charge.PmRateCharge += (pmRate*(epsilon/60f));
                }
            }
        }
    }
}
