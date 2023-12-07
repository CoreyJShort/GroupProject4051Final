using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject4051Final.Model;

internal class Customers
{
    public List<Customer> CustomerList { get; set; } // List to store customer objects

    // Constructor to initialize the CustomerList
    public Customers()
    {
        CustomerList = new List<Customer>();
    }

    // Method to authenticate a customer based on username and password
    public Customer Authenticate(string username, string password)
    {
        var c = CustomerList.FirstOrDefault(o => o.UserName == username && o.Password == password);

        return c;
    }
}
