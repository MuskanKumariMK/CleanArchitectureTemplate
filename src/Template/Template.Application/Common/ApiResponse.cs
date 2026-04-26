namespace Template.Applications.Common
{
     /// <summary>
     /// Represents a standardized API response structure for returning data and status messages.
     /// </summary>
     /// <typeparam name="T"></typeparam>
     public class ApiResponse<T>
     {
          /// <summary>
          /// Indicates whether the API call was successful or not.
          /// </summary>
          public bool Success { get; set; }
          /// <summary>
          /// Message providing additional information about the API call result.
          /// </summary>
          public string Message { get; set; }
          /// <summary>
          /// Contains the data returned by the API call, if any.
          /// </summary>
          public T? Data { get; set; }
          /// <summary>
          /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with success status and data.
          /// </summary>
          /// <param name="data"></param>
          /// <param name="message"></param>
          public ApiResponse(T data, string message = "Success")
          {
               // Success of the API call
               Success = true;
               // Message providing additional information
               Message = message;
               // Data returned by the API call
               Data = data;
          }
          /// <summary>
          /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with failure status and a message.
          /// </summary>
          /// <param name="message"></param>
          public ApiResponse(string message)
          {
               Success = false;
               Message = message;
               Data = default;
          }
          /// <summary>
          /// Creates a successful API response with the provided data and an optional message.
          /// </summary>
          /// <param name="data"></param>
          /// <param name="message"></param>
          /// <returns></returns>
          public static ApiResponse<T> SuccessResponse(T data, string message = "Success") => new(data, message);
          /// <summary>
          /// Creates a failed API response with the provided message.
          /// </summary>
          /// <param name="message"></param>
          /// <returns></returns>
          public static ApiResponse<T> FailResponse(string message) => new(message);
     }
}
