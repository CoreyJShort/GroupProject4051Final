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

        return c; // Returns authenticated customer or null if not found
    }

    // Method to add a new customer to the CustomerList
    public void AddCustomer(string firstName, string lastName, string username, string password)
    {
        int newCustomerId = CustomerList.Count + 1; // Generate a new unique customer ID

        var newCustomer = new Customer
        {
            Id = newCustomerId,
            FirstName = firstName,
            LastName = lastName,
            UserName = username,
            Password = password
        };

        CustomerList.Add(newCustomer); // Add the new customer to the list
        Console.WriteLine($"User {username} added successfully!"); // Display a success message
    }
}