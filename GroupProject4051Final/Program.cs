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
            Console.WriteLine("1. Login"); //display menu
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
        Console.Write("Enter employee password: "); // ask for password
        var enteredPassword = Console.ReadLine(); // read input

        if (enteredPassword == employeePassword)
        {
            // Successful authentication, performing employee actions
            Console.WriteLine("Employee Section Accessed!");

            while (true)
            {
                Console.WriteLine("1. Assigned Appointment Times"); //display menu
                Console.WriteLine("2. Schedule Appointment");
                Console.WriteLine("3. Manager Login");
                Console.WriteLine("4. Exit To Main Menu");
                Console.Write("Choose an option: ");

                var employeeChoice = Console.ReadLine();

                switch (employeeChoice)
                {
                    case "1":
                        // Assigned Appointment Times
                        break;

                    case "2":
                        // Schedule Appointment
                        break;

                    case "3":
                        // Manager Login
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

        for (int i = 0; i < 8; i++) //generate available times for 8 hours, starting from the current time.
        {
            availableTimes.Add(currentTime);// add the current time to the list of available times 
            currentTime = currentTime.AddHours(1); // move to the next hour
        }

        return availableTimes;
    }

    // Method to handle the login process
    private static Customer Login()
    {
        Console.Write("Enter username: "); // Asking for username
        var username = Console.ReadLine(); // inputing the information
        Console.Write("Enter password: "); // Asking for password
        var password = Console.ReadLine(); // inputing the informaiton

        var user = customers.Authenticate(username, password); //

        if (user != null)
        {
            Console.WriteLine($"Welcome, {user.FirstName} {user.LastName}!"); //display a welcome message when authentication is successful
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again."); // display error message if authentication fails
        }

        return user; // continue to menu, or not if failed 
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
            Console.Write("Enter the number of the employee (or 'x' to return to the main menu): ");// have user enter the number for the employee they want to select
            var input = Console.ReadLine(); // read user input

            if (input.ToLower() == "x") // check if they want to go back to main menu
            {
                return; // User wants to return to the main menu, exit loop
            }

            if (int.TryParse(input, out selectedEmployeeIndex) && //attempt parse input as an integer and check if it is a valid employee number
                selectedEmployeeIndex >= 1 && selectedEmployeeIndex <= employees.Count)
            {
                break; //valid selection, exit the loop
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'."); // error message for invalid input
            }
        }

        var selectedEmployee = employees[selectedEmployeeIndex - 1]; // retrieve the selected employee based on the users input
        availableTimes = selectedEmployee.AvailableTimes;

        DateTime selectedDate; //variables to store user-selected date
        while (true) // continous loop to prompt the user to enter the date in the correct format
        {
            Console.Write("Enter the date to view available appointment times (MM/dd/yyyy): "); //prompt the user to enter the date to view available appointment times
            var selectedDateInput = Console.ReadLine(); // read user input for date

            if (DateTime.TryParseExact(selectedDateInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out selectedDate)) //attempt to parse the input as DateTime with the specified format
            {
                if (selectedDate.Date < DateTime.Today) // Check if the selected date is in the future
                {
                    Console.WriteLine("Invalid date. Please select a date in the future."); // If its an invalid date, display an error 
                }
                else
                {
                    break; //Valid date? break out of the loop
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format MM/dd/yyyy."); // if invalid date format, display error
            }
        }

        Console.WriteLine($"Available Appointment Times for {selectedEmployee.FirstName} on {selectedDate.ToString("MM/dd/yyyy")}:"); // Display a header indicating the selected employee's name and date chosen
        foreach (var time in availableTimes.Where(t => t > DateTime.Now)) // iterate through available times for the selected employee, filtering those in the future
        {
            Console.WriteLine(time.ToString("hh:mm tt")); // display the time in a 12 hour format
        }

        Console.WriteLine(); // empty line
    }

    private static void ScheduleAppointment() // this will store the index of the selected employee
    {
        if (loggedInUser == null) //checks for logged in user, if not, prompt to log in first
        {
            Console.WriteLine("Please log in before scheduling an appointment.");
            return;
        }

        Console.WriteLine("Select an employee to schedule with:"); //selecting the employee you want to schedule with
        for (int i = 0; i < employees.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {employees[i].FirstName}");
        }

        int selectedEmployeeIndex; // variable to store the index of the selected employee
        while (true) // loop to prompt the user to enter the number next to the employee
        {
            Console.Write("Enter the number of the employee (or 'x' to return to the main menu): "); // prompt to select employee
            var input = Console.ReadLine();// read input

            if (input.ToLower() == "x") // x cancels and sends back to menu 
            {
                return; // User wants to return to the main menu
            }

            if (int.TryParse(input, out selectedEmployeeIndex) && //check if number is valid
                selectedEmployeeIndex >= 1 && selectedEmployeeIndex <= employees.Count)
            {
                break; //if valid, exit
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'."); //if invalid show error
            }
        }
        
        var selectedEmployee = employees[selectedEmployeeIndex - 1]; //access employee
        availableTimes = selectedEmployee.AvailableTimes;// store on that employee

        DateTime selectedDate; //store user-selected date
        while (true) //loop for correct date input
        {
            Console.Write("Enter the date for the appointment (MM/dd/yyyy): "); // prompt user to enter date 
            var selectedDateInput = Console.ReadLine(); // take in input

            if (DateTime.TryParseExact(selectedDateInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out selectedDate)) // parse input as DateTime
            {
                if (selectedDate.Date < DateTime.Today) // check if date is in the future
                {
                    Console.WriteLine("Invalid date. Please select a date in the future."); //if invalid, display error
                }
                else
                {
                    break;// Valid date, break out of the loop
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format MM/dd/yyyy."); //If date is invalid, display error
            }
        }

        var futureTimes = availableTimes.Where(t => selectedDate.Date == DateTime.Today ? t > DateTime.Now : true).ToList(); //filter out times and dates in the past

        if (futureTimes.Count == 0) // varify if there are no available appointment times for the selected date
        {
            Console.WriteLine("No available appointment times for the selected date.");// display message if that is the case
            return;
        }

        Console.WriteLine($"Available Appointment Times for {selectedEmployee.FirstName} on {selectedDate.ToString("MM/dd/yyyy")}:"); //display results of selection
        for (int i = 0; i < futureTimes.Count; i++) //go through the available appointment times for the chosen date and show them
        {
            Console.WriteLine($"{i + 1}. {futureTimes[i].ToString("hh:mm tt")}"); //display available times for selected date 
        }

        int selectedTimeIndex; //store the index of the selected appointment time
        while (true)
        {
            Console.Write("Select an appointment time by entering the number (or 'x' to return to the main menu): "); //prompt for choosing an appointment time
            var input = Console.ReadLine(); //read input

            if (input.ToLower() == "x")// return to menu?
            {
                return; //return to main menu if true
            }

            if (int.TryParse(input, out selectedTimeIndex) && //parse input as int, check if valid
                selectedTimeIndex >= 1 && selectedTimeIndex <= futureTimes.Count)
            {
                var selectedTime = futureTimes[selectedTimeIndex - 1]; //get the chosen appointment time from the list

                availableTimes.Remove(selectedTime); // remove selected time 

                var appointment = new Appointment//create an appointment and associate it with the logged-in user
                {
                    Id = Guid.NewGuid().ToString(),
                    DateTime = selectedDate.Add(selectedTime.TimeOfDay) //combine the date and time
                };

                var customerAppointment = new CustomerAppointment(loggedInUser, appointment);

                Console.WriteLine($"Appointment scheduled at {appointment.DateTime} for {loggedInUser.FirstName} {loggedInUser.LastName} with {selectedEmployee.FirstName}"); //message confirming appointment details

                break; //exit loop
            }
            else //if wrong
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 'x'.");//prompt user
            }
        }
    }

    private static void Register()// Method to handle the registration process
    {
        string firstName, lastName, username, password;

        do
        {
            Console.Write("Enter first name: ");
            firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName)) // recognizing no entry
            {
                Console.WriteLine("Please enter valid information."); // asking for valid information
            }
        } while (string.IsNullOrWhiteSpace(firstName)); // if ok, lets goo

        do
        {
            Console.Write("Enter last name: "); // asking for last name
            lastName = Console.ReadLine(); // reading information
            if (string.IsNullOrWhiteSpace(lastName)) // recognizing if there is no entry
            {
                Console.WriteLine("Please enter valid information."); // error message
            }
        } while (string.IsNullOrWhiteSpace(lastName)); //is it ok, no? Go back, yes, move forward

        do
        {
            Console.Write("Enter username: "); // enter user name 
            username = Console.ReadLine(); // read username

            if (customers.CustomerList.Any(c => c.UserName == username)) // if this is the same as any other, 
            {
                Console.WriteLine("Username already exists. Please choose a different username."); // send an error
            }
            else if (string.IsNullOrWhiteSpace(username)) // is it empty space?
            {
                Console.WriteLine("Please enter valid information."); // if so send error
            }
        } while (string.IsNullOrWhiteSpace(username) || customers.CustomerList.Any(c => c.UserName == username)); // if ok, move forward

        do
        {
            Console.Write("Enter password: "); // ask for a password
            password = Console.ReadLine(); // intake password input
            if (string.IsNullOrWhiteSpace(password)) // is there white space?
            {
                Console.WriteLine("Please enter valid information."); // send error message
            }
        } while (string.IsNullOrWhiteSpace(password)); // move forward if ok

        customers.AddCustomer(firstName, lastName, username, password); // add the customer with information entered
    }

    private static void InitializeData()
    {
        // baked in accounts for testing needs
        customers.CustomerList.Add(new Customer { Id = 1, FirstName = "John", LastName = "Povinelli", UserName = "JP", Password = "100" });
        customers.CustomerList.Add(new Customer { Id = 2, FirstName = "Pat", LastName = "Cratz", UserName = "PC", Password = "100" });
        customers.CustomerList.Add(new Customer { Id = 3, FirstName = "Corey", LastName = "Short", UserName = "CS", Password = "100" });
    }
}
