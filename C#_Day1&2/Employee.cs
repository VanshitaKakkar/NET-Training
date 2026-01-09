namespace Assignment_1;


class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }

    public void Display()
    {
        Console.WriteLine($"ID: {EmployeeId}, Name: {Name}, Salary: {Salary}");
    }
}
