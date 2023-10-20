namespace API.Helpers;
public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    public ApiResponse()
    {
    }

    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "You have made a bad request",
            401 => "Unauthorized user",
            404 => "The resource you have tried to request does not exist.",
            405 => "This HTTP method is not allowed on the server.",
            500 => "Server error.",
            _ => throw new NotImplementedException()
        };
    }
}