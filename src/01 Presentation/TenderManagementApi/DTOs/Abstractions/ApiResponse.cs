using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TenderManagementApi.DTOs.Abstractions
{
    public class ApiResponse<TResponse>
    {
        public List<Error> Errors { get; set; }

        public TResponse? Data { get; set; }

        public PaginationMetaData? PaginationMetaData { get; set; }

        public bool Succeeded { get; set; }

        public ApiResponse() => this.Errors = new List<Error>();

        public static ApiResponse<TResponse> Failed(params string[] errors)
        {
            ApiResponse<TResponse> apiResponse = new ApiResponse<TResponse>()
            {
                Succeeded = false
            };
            foreach (string error in errors)
                apiResponse.Errors.Add(new Error()
                {
                    ErrorMessage = error,
                    Code = new short?()
                });
            return apiResponse;
        }

        public static ApiResponse<TResponse> Succeed(TResponse data)
        {
            return new ApiResponse<TResponse>()
            {
                Succeeded = true,
                Data = data
            };
        }

    }
}
