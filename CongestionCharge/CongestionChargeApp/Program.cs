using System;
using System.Collections.Generic;
using CongestionCharge;
using CongestionCharge.Utils;

namespace CongestionChargeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new[]
                {
                    "Car: 24/04/2008 11:32 - 24/04/2008 14:42",
                    "Motorbike: 24/04/2008 17:00 - 24/04/2008 22:11",
                    "Van: 25/04/2008 10:23 - 28/04/2008 09:02",
                };

            var charges = new List<Charge>();

            var i = 1;
            foreach (var bill in input)
            {
                Console.WriteLine("INPUT " + i + "\n\n" + bill);
                try
                {
                    charges.Add(CongestionCharger.Charge(BillParser.Parse(bill)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine();
                i++;
            }

            i = 1;
            foreach (var charge in charges)
            {
                Console.WriteLine("OUTPUT " + i + '\n');
                Console.WriteLine(charge.ToString());
                Console.WriteLine();
                i++;
            }

            Console.ReadLine();
        }
    }
}
