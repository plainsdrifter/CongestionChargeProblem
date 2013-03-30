using System;
using CongestionCharge.Enums;
using CongestionCharge.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace CongestionCharge.Tests
{
    public class ChargerTests
    {
        [Test]
        public void Should_check_is_weekend()
        {
            //arrange
            var date1 = new DateTime(2013, 03, 30, 0, 0, 0);
            var date2 = new DateTime(2013, 03, 28, 0, 0, 0);

            //act
            var res1 = date1.IsWeekend();
            var res2 = date2.IsWeekend();

            //assert
            res1.Should().BeTrue();
            res2.Should().BeFalse();
        }

        [Test]
        public void Should_check_is_free_of_charge()
        {
            //arange
            var date1 = new DateTime(2013, 04, 30, 6, 59, 0);
            var date2 = new DateTime(2013, 04, 30, 19, 10, 54);
            var date3 = new DateTime(2013, 04, 30, 7, 0, 54);
            var date4 = new DateTime(2013, 04, 30, 12, 0, 0);

            //act
            var res1 = date1.IsFreeOfCharge();
            var res2 = date2.IsFreeOfCharge();
            var res3 = date3.IsFreeOfCharge();
            var res4 = date4.IsFreeOfCharge();

            //assert
            res1.Should().BeTrue();
            res2.Should().BeTrue();
            res3.Should().BeFalse();
            res4.Should().BeFalse();
        }

        [Test]
        public void Should_check_in_am_rate()
        {
            //arange
            var date1 = new DateTime(2013, 04, 30, 7, 0, 0);
            var date2 = new DateTime(2013, 04, 30, 9, 10, 54);
            var date3 = new DateTime(2013, 04, 30, 6, 55, 54);
            var date4 = new DateTime(2013, 04, 30, 12, 0, 54);

            //act
            var res1 = date1.InAmRate();
            var res2 = date2.InAmRate();
            var res3 = date3.InAmRate();
            var res4 = date4.InAmRate();

            //assert
            res1.Should().BeTrue();
            res2.Should().BeTrue();
            res3.Should().BeFalse();
            res4.Should().BeFalse();
        }

        [Test]
        public void Should_check_in_pm_rate()
        {
            //arange
            var date1 = new DateTime(2013, 04, 30, 12, 1, 0);
            var date2 = new DateTime(2013, 04, 30, 18, 10, 54);
            var date3 = new DateTime(2013, 04, 30, 11, 59, 59);
            var date4 = new DateTime(2013, 04, 30, 19, 0, 0);

            //act
            var res1 = date1.InPmRate();
            var res2 = date2.InPmRate();
            var res3 = date3.InPmRate();
            var res4 = date4.InPmRate();

            //assert
            res1.Should().BeTrue();
            res2.Should().BeTrue();
            res3.Should().BeFalse();
            res4.Should().BeFalse();
        }

        [Test]
        public void Should_charge_car_in_am_rate()
        {
            //arrange
            var bill = new Bill(Vehicle.Car, new DateTime(2013, 04, 30, 7, 15, 0), new DateTime(2013, 04, 30, 11, 0, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(7.5f);
            charge.PmRateChargeRounded.Should().Be(0f);
            charge.TotalCharge.Should().Be(7.5f);
        }

        [Test]
        public void Should_charge_car_in_pm_rate()
        {
            //arrange
            var bill = new Bill(Vehicle.Car, new DateTime(2013, 04, 30, 12, 45, 0), new DateTime(2013, 04, 30, 15, 15, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(0f);
            charge.PmRateChargeRounded.Should().Be(6.2f);
            charge.TotalCharge.Should().Be(6.2f);
        }

        [Test]
        public void Should_charge_car_in_overlapping_rates()
        {
            //arrange
            var bill = new Bill(Vehicle.Car, new DateTime(2013, 04, 30, 9, 45, 0), new DateTime(2013, 04, 30, 15, 15, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(4.5f);
            charge.PmRateChargeRounded.Should().Be(8.1f);
            charge.TotalCharge.Should().Be(12.6f);
        }

        [Test]
        public void Should_charge_car_in_overlapping_rates_for_input1()
        {
            //arrange
            var bill = new Bill(Vehicle.Car, new DateTime(2008, 04, 24, 11, 32, 0), new DateTime(2008, 04, 24, 14, 42, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(0.9f);
            charge.PmRateChargeRounded.Should().Be(6.7f);
            charge.TotalCharge.Should().Be(7.6f);
            charge.ToString().Should().Be("Charge for 0h 28m (AM rate): £0,90\n\n" +
                                          "Charge for 2h 42m (PM rate): £6,70\n\n" +
                                          "Total Charge: £7,60");
        }

        [Test]
        public void Should_charge_motorcycle_in_overlapping_rates_for_input2()
        {
            //arrange
            var bill = new Bill(Vehicle.Motorbike, new DateTime(2008, 04, 24, 17, 0, 0), new DateTime(2008, 04, 24, 22, 11, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(0f);
            charge.PmRateChargeRounded.Should().Be(2f);
            charge.TotalCharge.Should().Be(2f);
            charge.ToString().Should().Be("Charge for 0h 0m (AM rate): £0,00\n\n" +
                                          "Charge for 2h 0m (PM rate): £2,00\n\n" +
                                          "Total Charge: £2,00");
        }

        [Test]
        public void Should_charge_van_in_overlapping_rates_for_input3()
        {
            //arrange
            var bill = new Bill(Vehicle.Van, new DateTime(2008, 04, 25, 10, 23, 0), new DateTime(2008, 04, 28, 9, 2, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(7.3f);
            charge.PmRateChargeRounded.Should().Be(17.5f);
            charge.TotalCharge.Should().Be(24.8f);
            charge.ToString().Should().Be("Charge for 3h 39m (AM rate): £7,30\n\n" +
                                          "Charge for 7h 0m (PM rate): £17,50\n\n" +
                                          "Total Charge: £24,80");
        }

        [Test]
        public void Should_charge_van_in_overlapping_rates_week_long_period()
        {
            //arrange
            var bill = new Bill(Vehicle.Van, new DateTime(2013, 03, 25, 10, 23, 0), new DateTime(2013, 04, 01, 9, 2, 0));

            //act
            var charge = CongestionCharger.Charge(bill);

            //assert
            charge.AmRateChargeRounded.Should().Be(47.3f);
            charge.PmRateChargeRounded.Should().Be(87.5f);
            charge.TotalCharge.Should().Be(134.8f);
            charge.AmRateSpan.Should().Be(TimeSpan.FromMinutes(1419));
            charge.PmRateSpan.Should().Be(TimeSpan.FromMinutes(2100));
            charge.ToString().Should().Be("Charge for 23h 39m (AM rate): £47,30\n\n" +
                                          "Charge for 35h 0m (PM rate): £87,50\n\n" +
                                          "Total Charge: £134,80");
        }

        [Test]
        public void Should_parse_the_input()
        {
            //arrange
            var expected = new Bill(Vehicle.Car, new DateTime(2008, 04, 24, 11, 32, 0), new DateTime(2008, 04, 24, 14, 42, 0));

            //act
            var bill = BillParser.Parse("Car: 24/04/2008 11:32 - 24/04/2008 14:42");

            //assert
            bill.ShouldBeEquivalentTo(expected);
        }

        [TestCase("", "Value cannot be null.\r\nParameter name: input")]
        [TestCase("Van: 25/70/2008 10:23 - 28/04/2008 09:02", "Unable to parse entry date.")]
        [TestCase("Car: 24/04/2008 11:32 - 24/04/2008 80:42", "Unable to parse leave date.")]
        [TestCase("asdf: 24/04/2008 11:32 - 24/04/2008 09:42", "Unable to parse vehicle.")]
        [TestCase("Van: 25/04/2008 10:23. - 28/04/2008 09:02", "Input is of invalid format.\r\nParameter name: input")]
        public void Should_throw_invalid_input_exceptions(string input, string message)
        {
            //arrange
            //act
            Action act = () => BillParser.Parse(input);

            //assert
            act.ShouldThrow<Exception>().WithMessage(message);
        }
    }
}
