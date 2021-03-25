namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        public Result(string message,bool success)
        {
            Message = message;
            Success = success;
        }

        public Result(bool success)
        {
            Success = success;
        }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}