namespace EmployeeManagementApi.DTOs
{
    public class ResponseObj<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

        public ResponseObj(T data, string message, int totalCount = 0)
        {
            Data = data;
            Message = message;
            Success = true;
            TotalCount = totalCount;
        }

        public ResponseObj(T data, string message, bool success, int totalCount = 0)
        {
            Data = data;
            Message = message;
            Success = success;
            TotalCount = totalCount;
        }

        public ResponseObj(string message)
        {
            Data = default;
            Message = message;
            Success = true;
        }
    }
}