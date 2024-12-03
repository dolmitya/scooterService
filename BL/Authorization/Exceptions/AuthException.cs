namespace BL.Authorization.Exceptions;

public class AuthException : Exception
{
    public ResultCode? Code { get; set; }
    public AuthException(string message) : base(message)
    {
    }
    public AuthException(ResultCode code) : base(code.ToString())
    {
        Code = code;
    }
}