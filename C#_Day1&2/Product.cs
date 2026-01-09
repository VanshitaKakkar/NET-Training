namespace Assignment_1;

using System;

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }

    public void Display()
    {
        Console.WriteLine($"ID: {ProductId}, Name: {ProductName}, Price: {Price}");
    }
    
}
