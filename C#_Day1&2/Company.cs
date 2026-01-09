namespace Assignment_1;

public class Company
{
    private const string CompanyName = "XYZ";

    private readonly string _registrationNumber;
    
    private static int _employeeCount = 0;
    
    private string _employeeName;
    
    private double _salary;


    public Company(string registrationNumber,string employeeName,double salary)
    {
        this._registrationNumber =  registrationNumber;
        this._employeeName = employeeName;
        this._salary = salary;
        _employeeCount++;
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Company Name: {CompanyName}");
        Console.WriteLine($"Registration Number: {_registrationNumber}");
        Console.WriteLine($"Employee Name: {_employeeName}");
        Console.WriteLine($"Salary: {_salary}");
        
    }

    public void UpdateSalary(ref double newSalary, out double bonus)
    {
        _salary = newSalary;
        bonus = 7000;
    }
}