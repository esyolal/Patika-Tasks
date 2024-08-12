namespace BookStore.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool IsSuccess { get; set; }

        // Başarılı yanıt için constructor
        public ApiResponse(T data)
        {
            Data = data;
            IsSuccess = true;
            Error = string.Empty;
        }

        // Hata mesajı ile yanıt için constructor
        public ApiResponse(string errorMessage)
        {
            Data = default;
            IsSuccess = false;
            Error = errorMessage;
        }

        public ApiResponse()
        {
        }
    }
}
