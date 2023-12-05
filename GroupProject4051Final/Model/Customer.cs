using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject4051Final.Model;

internal class Customer
{
    public int Id { get; set; } // Unique identifier for the customer
    public string FirstName { get; set; } // First name of the customer
    public string LastName { get; set; } // Last name of the customer
    public string UserName { get; set; } // Username for authentication
    public string Password { get; set; } // Password for authentication
}