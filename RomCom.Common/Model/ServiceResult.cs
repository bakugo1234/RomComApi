namespace RomCom.Common.Model
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ServiceResult<T> Success(T data, string message = "Success")
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult<T> Failure(string message = "An error occurred")
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default(T)
            };
        }
    }
}
