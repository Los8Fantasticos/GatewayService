namespace Api.CrossCutting.Helpers
{
    public class ApiError
    {
        //Constructor
        public ApiError(string message, int code)
        {
            Message = message;
            ErrorCode = code;
        }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}
