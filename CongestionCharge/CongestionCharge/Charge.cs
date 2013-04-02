using System;
﻿using System.Globalization;

namespace CongestionCharge
{
    public class Charge
    {
        public TimeSpan AmRateSpan { get; set; }
        public TimeSpan PmRateSpan { get; set; }
        public float AmRateCharge { get; set; }
        public float PmRateCharge { get; set; }

        public float AmRateChargeRounded
        {
            get { return (float)Math.Round(AmRateCharge, 1); }
        }

        public float PmRateChargeRounded
        {
            get { return (float)Math.Round(PmRateCharge, 1); }
        }

        public float TotalCharge
        {
            get { return AmRateChargeRounded + PmRateChargeRounded; }
        }

        public Charge()
        {
            AmRateCharge = 0f;
            PmRateCharge = 0f;
        }

        public override string ToString()
        {
            return
                string.Format(new CultureInfo("lt-LT"),
                    "Charge for {0}h {1}m (AM rate): £{2:F}\n\n" +
                    "Charge for {3}h {4}m (PM rate): £{5:F}\n\n" +
                    "Total Charge: £{6:F}",
                    (int)AmRateSpan.TotalHours, AmRateSpan.Minutes, AmRateChargeRounded,
                    (int)PmRateSpan.TotalHours, PmRateSpan.Minutes, PmRateChargeRounded,
                    TotalCharge);
        }
    }
}
