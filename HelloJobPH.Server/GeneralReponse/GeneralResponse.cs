namespace HelloJobPH.Server.GeneralReponse
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static GeneralResponse<T> Ok(string message, T? data = default)
            => new() { Success = true, Message = message, Data = data };

        public static GeneralResponse<T> Fail(string message)
            => new() { Success = false, Message = message };
    }

}
