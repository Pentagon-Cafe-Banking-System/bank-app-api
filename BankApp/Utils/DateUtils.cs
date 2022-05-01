namespace BankApp.Utils;

public static class DateUtils
{
    public static int GetYearsBetween(DateTime start, DateTime end)
    {
        var years = end.Year - start.Year;
        if (start.AddYears(years) > end)
        {
            years--;
        }

        return years;
    }
}