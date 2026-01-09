namespace Assignment_1;

public class BankAccount
{
    private double _balance;
    private string _accountNumber;

    public double Balance
    {
        get => _balance;
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Incorrect balance");
            }
            else
            {
                _balance = value;
            }
        }
    }

    public string AccountNumber
    {
        get => _accountNumber;
        set
        {
            if (value.Length < 3)
            {
                Console.WriteLine("Incorrect account number");
            }
            else
            {
                _accountNumber = value;
            }
        }
    }

    public void Deposit(double amount)
       {
           if (amount < 0)
           {
               Console.WriteLine("Incorrect amount");
           }
           else
           {
               _balance += amount;
           }
       }

    public void Withdraw(double amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Incorrect amount");
        }
        else
        {
            _balance -= amount;
        }
    }
    
}