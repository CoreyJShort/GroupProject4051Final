using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject4051Final.Model
{
    internal class Employee
    { 
        // Properties to store employee information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DateTime> AvailableTimes { get; set; }

        // Constructor to initialize employee with first and last name
        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            AvailableTimes = GetAvailableAppointmentTimes(); // Initialize available times
        }

        // Private method to generate a list of available appointment times
        private List<DateTime> GetAvailableAppointmentTimes()
        {
            var availableTimes = new List<DateTime>();
            DateTime currentTime = DateTime.Today.AddHours(9); // Start from 9 AM today

            // Generate available times for the next 8 hours
            for (int i = 0; i < 8; i++)
            {
                availableTimes.Add(currentTime); // Add current time to the list
                currentTime = currentTime.AddHours(1); // Move to the next hour
            }

            return availableTimes;
        }
    }
}
