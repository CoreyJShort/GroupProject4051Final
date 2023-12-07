using GroupProject4051Final.Model;

namespace GroupProject4051Final;

internal class Program
{
    private static Customers customers;
    private static Customer loggedInUser;
    private static List<Employee> employees;
    private static List<DateTime> availableTimes; // Declare at the class level
    private static string employeePassword = "admin"; // Employee password

    private static void Main()
    {   
        customers = new Customers();
        employees = new List<Employee>
    {
        new Employee("Corey Short", "Employee1"),
        new Employee("John Povinelli", "Employee2"),
        new Employee("Pat Crantz", "Employee3")
    };
        availableTimes = employees.First().AvailableTimes; // Use the times of the first employee
        InitializeData();

        while (true)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. View Appointment Times");
            Console.WriteLine("4. Schedule Appointment");
            Console.WriteLine("5. Employee Section");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Entering 'x' = back to main menu.");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            if (choice.ToLower() == "x")
            {
                // User wants to return to the main menu
                continue;
            }

            switch (choice)
            {
                case "1":
                    loggedInUser = Login(); // Attempt to log in
                    break;
                case "2":
                    Register(); // Register new user
                    break;
                case "3":
                    ViewAppointmentTimes(); // View available appointment times
                    break;
                case "4":
                    ScheduleAppointment(); // Schedule new appointment
                    break;
                case "5":
                    EmployeeSection(); // Access the employee section
                    break;
                case "6":
                    Environment.Exit(0); // Exit program
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again."); // Notification that the user entered an unavailable choice
                    break;
            }
        }
    }

    private static void EmployeeSection()
    {
        Console.Write("Enter employee password: ");
        var enteredPassword = Console.ReadLine();

        if (enteredPassword == employeePassword)
        {
            // Successful authentication, perform employee actions
            Console.WriteLine("Employee Section Accessed!");

            while (true)
            {
                Console.WriteLine("1. Assigned Appointment Times");
                Console.WriteLine("2. Schedule Appointment");
                Console.WriteLine("3. Manager Login");
                Console.WriteLine("4. Exit To Main Menu");
                Console.Write("Choose an option: ");

                var employeeChoice = Console.ReadLine();

                switch (employeeChoice)
                {
                    case "1":
                        // Assigned Appointment Times logic
                        break;

                    case "2":
                        // Schedule Appointment logic
                        break;

                    case "3":
                        // Manager Login logic
                        break;

                    case "4":
                        Console.WriteLine("Exiting to Main Menu...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please enter a valid number.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Incorrect password. Access denied.");
        }
    }

    // Method to generate a list of 8 available appointment times
    private static List<DateTime> GetAvailableAppointmentTimes()
    {
        // Generate a list of 8 available appointment times
        var availableTimes = new List<DateTime>(); //Creating a new empty list that stores DateTime
        DateTime currentTime = DateTime.Today.AddHours(9);

        for (int i = 0; i < 8; i++)
        {
            availableTimes.Add(currentTime);
            currentTime = currentTime.AddHours(1);
        }

        return availableTimes;
    }

    // Method to handle the login process
    private static Customer Login()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        var user = customers.Authenticate(username, password);

        if (user != null)
        {
            Console.WriteLine($"Welcome, {user.FirstName} {user.LastName}!");
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
        }

        return user;
    }

    private static void ViewAppointmentTimes()
    {
        Console.WriteLine("Select an employee to view available appointment times:");
        for (int i = 0; i < employees.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {employees[i].FirstName}");
        }

        int selectedEmployeeIndex;
        while (true)
        {
            Console.Write("Enter the number of the employee (or 'x' to return to the main menu): ");
            var input = Console.ReadLine();

            if (input.ToLower() == "x")
            {
                // User wants to return to the main menu
                return;
            }

            if (int.TryParse(input, out selectedEmployeeIndex) &&
                selectedEmployeeIndex >= 1 && selectedEmployeeIndex <= employees.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'.");
            }
        }

        // Use the times of the selected employee
        var selectedEmployee = employees[selectedEmployeeIndex - 1];
        availableTimes = selectedEmployee.AvailableTimes;

        DateTime selectedDate;
        while (true)
        {
            Console.Write("Enter the date to view available appointment times (MM/dd/yyyy): ");
            var selectedDateInput = Console.ReadLine();

            if (DateTime.TryParseExact(selectedDateInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out selectedDate))
            {
                if (selectedDate.Date < DateTime.Today)
                {
                    Console.WriteLine("Invalid date. Please select a date in the future.");
                }
                else
                {
                    // Valid date, break out of the loop
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format MM/dd/yyyy.");
            }
        }

        Console.WriteLine($"Available Appointment Times for {selectedEmployee.FirstName} on {selectedDate.ToString("MM/dd/yyyy")}:");
        foreach (var time in availableTimes.Where(t => t > DateTime.Now))
        {
            Console.WriteLine(time.ToString("hh:mm tt"));
        }

        Console.WriteLine();
    }

    private static void ScheduleAppointment()
    {
        if (loggedInUser == null)
        {
            Console.WriteLine("Please log in before scheduling an appointment.");
            return;
        }

        Console.WriteLine("Select an employee to schedule with:");
        for (int i = 0; i < employees.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {employees[i].FirstName}");
        }

        int selectedEmployeeIndex;
        while (true)
        {
            Console.Write("Enter the number of the employee (or 'x' to return to the main menu): ");
            var input = Console.ReadLine();

            if (input.ToLower() == "x")
            {
                // User wants to return to the main menu
                return;
            }

            if (int.TryParse(input, out selectedEmployeeIndex) &&
                selectedEmployeeIndex >= 1 && selectedEmployeeIndex <= employees.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'.");
            }
        }

        // Use the times of the selected employee
        var selectedEmployee = employees[selectedEmployeeIndex - 1];
        availableTimes = selectedEmployee.AvailableTimes;

        DateTime selectedDate;
        while (true)
        {
            Console.Write("Enter the date for the appointment (MM/dd/yyyy): ");
            var selectedDateInput = Console.ReadLine();

            if (DateTime.TryParseExact(selectedDateInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out selectedDate))
            {
                if (selectedDate.Date < DateTime.Today)
                {
                    Console.WriteLine("Invalid date. Please select a date in the future.");
                }
                else
                {
                    // Valid date, break out of the loop
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format MM/dd/yyyy.");
            }
        }

        // Filter out appointment times that are in the past
        var futureTimes = availableTimes.Where(t => selectedDate.Date == DateTime.Today ? t > DateTime.Now : true).ToList();

        if (futureTimes.Count == 0)
        {
            Console.WriteLine("No available appointment times for the selected date.");
            return;
        }

        Console.WriteLine($"Available Appointment Times for {selectedEmployee.FirstName} on {selectedDate.ToString("MM/dd/yyyy")}:");
        for (int i = 0; i < futureTimes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {futureTimes[i].ToString("hh:mm tt")}");
        }

        int selectedTimeIndex;
        while (true)
        {
            Console.Write("Select an appointment time by entering the number (or 'x' to return to the main menu): ");
            var input = Console.ReadLine();

            if (input.ToLower() == "x")
            {
                // User wants to return to the main menu
                return;
            }

            if (int.TryParse(input, out selectedTimeIndex) &&
                selectedTimeIndex >= 1 && selectedTimeIndex <= futureTimes.Count)
            {
                // Adjust index to access the selected time
                var selectedTime = futureTimes[selectedTimeIndex - 1];

                // Remove the selected time from the list of available times
                availableTimes.Remove(selectedTime);

                // Create an appointment and associate it with the logged-in user
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid().ToString(),
                    DateTime = selectedDate.Add(selectedTime.TimeOfDay) // Combine date and time
                };

                var customerAppointment = new CustomerAppointment(loggedInUser, appointment);

                // Implement additional logic for storing or displaying the scheduled appointment
                Console.WriteLine($"Appointment scheduled at {appointment.DateTime} for {loggedInUser.FirstName} {loggedInUser.LastName} with {selectedEmployee.FirstName}");

                // Exit the loop after a successful appointment scheduling
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'.");
            }
        }
    }

    // Method to handle the registration process
    private static void Register()
    {
        string firstName, lastName, username, password;

        do
        {
            Console.Write("Enter first name: ");
            firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Please enter valid information.");
            }
        } while (string.IsNullOrWhiteSpace(firstName));

        do
        {
            Console.Write("Enter last name: ");
            lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Please enter valid information.");
            }
        } while (string.IsNullOrWhiteSpace(lastName));

        do
        {
            Console.Write("Enter username: ");
            username = Console.ReadLine();

            if (customers.CustomerList.Any(c => c.UserName == username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
            }
            else if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Please enter valid information.");
            }
        } while (string.IsNullOrWhiteSpace(username) || customers.CustomerList.Any(c => c.UserName == username));

        do
        {
            Console.Write("Enter password: ");
            password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Please enter valid information.");
            }
        } while (string.IsNullOrWhiteSpace(password));

        customers.AddCustomer(firstName, lastName, username, password);
    }

    private static void InitializeData()
    {
        // Add initial customers to the list of customers
        customers.CustomerList.Add(new Customer { Id = 1, FirstName = "John", LastName = "Povinelli", UserName = "JP", Password = "100" });
        customers.CustomerList.Add(new Customer { Id = 1, FirstName = "Pat", LastName = "Cratz", UserName = "PC", Password = "100" });
        customers.CustomerList.Add(new Customer { Id = 1, FirstName = "Corey", LastName = "Short", UserName = "CS", Password = "100" });
        // Add more customers if needed

    }
}
