namespace NorthwindWithDapper.ExceptionHandler;

public class CustomerNotFoundException : Exception
{
    public static string ErrorMessage { get; set; }

    public CustomerNotFoundException(string input)
    {
        ErrorMessage = input;
    }
}