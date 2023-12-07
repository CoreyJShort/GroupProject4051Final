using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject4051Final.Model;

internal class Employee
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<DateTime> AvailableTimes { get; set; }

    public Employee(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        AvailableTimes = GetAvailableAppointmentTimes();
    }

    private List<DateTime> GetAvailableAppointmentTimes()
    {
        var availableTimes = new List<DateTime>();
        DateTime currentTime = DateTime.Today.AddHours(9);

        for (int i = 0; i < 8; i++)
        {
            availableTimes.Add(currentTime);
            currentTime = currentTime.AddHours(1);
        }

        return availableTimes;
    }
}