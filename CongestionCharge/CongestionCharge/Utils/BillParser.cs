using System;
using System.Globalization;
using System.Text.RegularExpressions;
using CongestionCharge.Enums;

namespace CongestionCharge.Utils
{
    public class BillParser
    {
        public static Bill Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            var bill = new Bill();

            if (Regex.Match(input, @"\w+:\s\d{2}/\d{2}/\d{4}\s\d{2}:\d{2}\s-\s\d{2}/\d{2}/\d{4}\s\d{2}:\d{2}").Success)
            {
                var rxVehicle = new Regex(@"(?<VEHICLE>^[^\:]+)", RegexOptions.Compiled);
                var matchVehicle = rxVehicle.Match(input);

                if (matchVehicle.Success && matchVehicle.Groups["VEHICLE"] != null)
                {
                    var vehicle = matchVehicle.Groups["VEHICLE"].Value.Trim();

                    foreach (Vehicle value in Enum.GetValues(typeof(Vehicle)))
                    {
                        if (value.ToString().Equals(vehicle))
                        {
                            bill.Vehicle = value;
                            break;
                        }
                    }

                    if (bill.Vehicle == Vehicle.Unknown)
                        throw new Exception("Unable to parse vehicle.");
                }

                var rxEntry = new Regex(@"\:\s(?<ENTRY>[^\-]+)", RegexOptions.Compiled);
                var matchEntry = rxEntry.Match(input);

                if (matchEntry.Success && matchEntry.Groups["ENTRY"] != null)
                {
                    var entry = matchEntry.Groups["ENTRY"].Value.Trim();

                    DateTime outEntry;
                    
                    if(!DateTime.TryParseExact(entry, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out outEntry))
                        throw new Exception("Unable to parse entry date.");

                    bill.EntryDate = outEntry;
                }

                var rxLeave = new Regex(@"\-\s(?<LEAVE>[^$]+)", RegexOptions.Compiled);
                var matchLeave = rxLeave.Match(input);

                if (matchLeave.Success && matchLeave.Groups["LEAVE"] != null)
                {
                    var leave = matchLeave.Groups["LEAVE"].Value.Trim();

                    DateTime outLeave;

                    if(!DateTime.TryParseExact(leave, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out outLeave))
                        throw new Exception("Unable to parse leave date.");

                    bill.LeaveDate = outLeave;
                }
            }
            else
                throw new ArgumentException("Input is of invalid format.", "input");

            return bill;
        }
    }
}
