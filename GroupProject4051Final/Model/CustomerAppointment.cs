using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject4051Final.Model;

// Internal class representing the association of a customer with an appointment
internal class CustomerAppointment
{ 
    public Customer Customer { get; set; } // Customer associated with the appointment
    public Appointment Appointment { get; set; } // Appointment associated with the customer

    // Constructor to create a CustomerAppointment object
    public CustomerAppointment(Customer c, Appointment a)
    {
        Customer = c;
        Appointment = a;
    }
}