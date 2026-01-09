using Assignment_1;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // ============================
        // 1. Company Class Demo
        // ============================
        // Creating Company object
        Company emp1 = new Company("121", "Vanshita", 10000);
        emp1.DisplayDetails();

        // ============================
        // 2. ref and out Demonstration
        // ============================
        double salary = 50000;
        double bonus;

        // ref updates salary, out returns bonus
        emp1.UpdateSalary(ref salary, out bonus);

        Console.WriteLine("Updated Salary : " + salary);
        Console.WriteLine("Bonus : " + bonus);

        // ============================
        // 3. Salary Slab Program
        // ============================
        Console.Write("Enter Basic Salary: ");
        double basicSalary = Convert.ToDouble(Console.ReadLine());

        double hra, da;

        // Calculate HRA and DA based on salary slab
        if (basicSalary <= 10000)
        {
            hra = basicSalary * 0.20;
            da = basicSalary * 0.80;
        }
        else if (basicSalary <= 20000)
        {
            hra = basicSalary * 0.25;
            da = basicSalary * 0.90;
        }
        else
        {
            hra = basicSalary * 0.30;
            da = basicSalary * 0.95;
        }

        Console.WriteLine("HRA : " + hra);
        Console.WriteLine("DA : " + da);
        Console.WriteLine("Basic Salary : " + basicSalary);
        Console.WriteLine("Gross Salary : " + (basicSalary + hra + da));

        // ============================
        // 4. Menu Driven Calculator
        // ============================
        double a = 0, b = 0;
        int option;

        do
        {
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            Console.WriteLine("5. Exit");

            option = Convert.ToInt32(Console.ReadLine());

            // Input numbers except Exit
            if (option != 5)
            {
                Console.Write("Enter first number: ");
                a = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter second number: ");
                b = Convert.ToDouble(Console.ReadLine());
            }

            // Switch case for operations
            switch (option)
            {
                case 1:
                    Console.WriteLine("Result: " + (a + b));
                    break;
                case 2:
                    Console.WriteLine("Result: " + (a - b));
                    break;
                case 3:
                    Console.WriteLine("Result: " + (a * b));
                    break;
                case 4:
                    Console.WriteLine("Result: " + (a / b));
                    break;
                case 5:
                    Console.WriteLine("Exiting program");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

        } while (option != 5);

        // ============================
        // 5. Methods
        // ============================

        // Email Validation
        ValidateEmail("vanshita@gmail.com");
        ValidateEmail("vanshita.ghh");

        // Palindrome Check
        Palindrome(121);
        Palindrome(123);

        // Factorial using recursion
        Console.WriteLine($"Factorial of 12 is {Factorial(12)}");

        // ============================
        // 6. Method Overloading
        // ============================
        Calculate bill = new Calculate();

        Console.WriteLine("Electricity Bill : " + bill.CalculateBill(250));
        Console.WriteLine("Mobile Bill : " + bill.CalculateBill(300, 1.5));
        Console.WriteLine("Internet Bill : " + bill.CalculateBill("Premium", 50));

        // ============================
        // 7. BankAccount Class Demo
        // ============================
        BankAccount acc = new BankAccount();

        acc.AccountNumber = "12345";
        acc.Balance = 1000;

        acc.Deposit(500);
        acc.Withdraw(300);

        Console.WriteLine("Final Balance: " + acc.Balance);

        // ============================
        // 8. Student Marks Analysis
        // ============================
        int[] marks = { 78, 45, 90, 33, 60, 25, 88, 40 };

        int highest = marks[0];
        int lowest = marks[0];
        int passCount = 0;

        foreach (int mark in marks)
        {
            if (mark > highest) highest = mark;
            if (mark < lowest) lowest = mark;
            if (mark >= 40) passCount++;
        }

        Console.WriteLine("Highest Marks : " + highest);
        Console.WriteLine("Lowest Marks : " + lowest);
        Console.WriteLine("Passed Count : " + passCount);

        // ============================
        // 9. Product Class Operations
        // ============================
        Product[] products =
        {
            new Product { ProductId = 101, ProductName = "Laptop", Price = 55000 },
            new Product { ProductId = 102, ProductName = "Mobile", Price = 25000 },
            new Product { ProductId = 103, ProductName = "Tablet", Price = 18000 },
            new Product { ProductId = 104, ProductName = "Keyboard", Price = 25000 }
        };

        // Search product by ID
        int searchId = 102;
        bool found = false;

        foreach (Product p in products)
        {
            if (p.ProductId == searchId)
            {
                Console.WriteLine("Product Found:");
                p.Display();
                found = true;
                break;
            }
        }

        if (!found)
            Console.WriteLine("Product not found");

        // Display products above given price
        double priceLimit = 20000;
        Console.WriteLine($"Products above price {priceLimit}:");

        foreach (Product p in products)
        {
            if (p.Price > priceLimit)
                p.Display();
        }

        // ============================
        // 10. List<Employee> Operations
        // ============================
        List<Employee> employees = new List<Employee>
        {
            new Employee { EmployeeId = 1, Name = "Amit", Salary = 30000 },
            new Employee { EmployeeId = 2, Name = "Neha", Salary = 40000 },
            new Employee { EmployeeId = 3, Name = "Rahul", Salary = 35000 }
        };

        // Update salary
        foreach (Employee emp in employees)
        {
            if (emp.EmployeeId == 2)
            {
                emp.Salary = 45000;
                break;
            }
        }

        // Delete employee
        employees.RemoveAll(e => e.EmployeeId == 1);

        // Display employees
        Console.WriteLine("Employee List:");
        foreach (Employee emp in employees)
            emp.Display();

        // ============================
        // 11. Dictionary Operations
        // ============================
        Dictionary<int, string> users = new Dictionary<int, string>
        {
            { 101, "Vanshita" },
            { 102, "Prisha" },
            { 103, "Neha" }
        };

        // Prevent duplicate key
        if (!users.ContainsKey(102))
            users.Add(102, "Rahul");
        else
            Console.WriteLine("Duplicate UserId not allowed");

        // Search using TryGetValue
        if (users.TryGetValue(103, out string userName))
            Console.WriteLine($"User Found → ID: 103, Name: {userName}");
        else
            Console.WriteLine("User not found");
    }
    

    // Email validation method
    static void ValidateEmail(string email)
    {
        if (email.Contains("@") && email.Contains(".") && email.IndexOf("@") < email.IndexOf("."))
            Console.WriteLine($"Email {email} is valid");
        else
            Console.WriteLine($"Email {email} is invalid");
    }

    // Palindrome number check
    static void Palindrome(int number)
    {
        int reverse = 0, temp = number;

        while (number > 0)
        {
            reverse = reverse * 10 + number % 10;
            number /= 10;
        }

        Console.WriteLine(temp == reverse
            ? $"{temp} is a palindrome"
            : $"{temp} is NOT a palindrome");
    }

    // Recursive factorial method
    static int Factorial(int number)
    {
        if (number == 0) return 1;
        return number * Factorial(number - 1);
    }
}
