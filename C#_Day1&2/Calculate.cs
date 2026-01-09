namespace Assignment_1;

public class Calculate
{
    public double CalculateBill(int units)
    {
        double amount;

        if (units <= 100)
            amount = units * 2;
        else if (units <= 200)
            amount = (100 * 2) + ((units - 100) * 3);
        else
            amount = (100 * 2) + (100 * 3) + ((units - 200) * 5);

        return amount;
    }

    public double CalculateBill(double minutesUsed, double ratePerMinute)
    {
        return minutesUsed * ratePerMinute;
    }

    public double CalculateBill(string planType, double dataUsed)
    {
        double amount = 0;

        if (planType == "Basic")
            amount = 500 + (dataUsed * 10);
        else if (planType == "Premium")
            amount = 1000 + (dataUsed * 5);

        return amount;
    }
}