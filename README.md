CongestionChargeProblem
=======================
CONGESTION CHARGE PROBLEM

Glasgow has implemented an hourly congestion charge for cars and larger vehicles on weekdays. The charge is set at £2 per hour between 7am and 12pm, and £2.50 per hour between 12pm and 7pm. Between 7pm and 7am travel is free. Travel at the weekend is always free. Motorbikes are charged a flat rate of £1 per hour, and share the same free travel periods as cars. The charge per period is rounded down to the nearest 10p for parts of hours.

Write an application to output the expected receipt when a driver leaves the congestion zone, based on the following inputs.  You may implement any mechanism for feeding input into your solution, such as using hard coded data within a unit test. There is NO requirement for a UI. You should provide sufficient evidence that your solution is complete by, as a minimum, indicating that it works correctly with the supplied test data. 

INPUT 1

Car: 24/04/2008 11:32 - 24/04/2008 14:42

INPUT 2

Motorbike: 24/04/2008 17:00 - 24/04/2008 22:11

INPUT 3

Van: 25/04/2008 10:23 - 28/04/2008 09:02

OUTPUT 1

Charge for 0h 28m (AM rate): £0.90

Charge for 2h 42m (PM rate): £6.70

Total Charge: £7.60

OUTPUT 2

Charge for 0h 0m (AM rate): £0.00

Charge for 2h 0m (PM rate): £2.00

Total Charge: £2.00

OUTPUT 3

Charge for 3h 39m (AM rate): £7.30

Charge for 7h 0m (PM rate): £17.50

Total Charge: £24.80
