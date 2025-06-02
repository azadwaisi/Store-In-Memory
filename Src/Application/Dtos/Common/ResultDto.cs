using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Common
{
	// Enum برای انواع خطاها (قابل گسترش)
	public enum ResultStatusCode
	{
		// 2xx Success
		Success = 200,
		Created = 201,
		Accepted = 202,
		NoContent = 204,

		// 3xx Redirection
		NotModified = 304,

		// 4xx Client Errors
		BadRequest = 400,
		Unauthorized = 401,
		PaymentRequired = 402,
		Forbidden = 403,
		NotFound = 404,
		MethodNotAllowed = 405,
		NotAcceptable = 406,
		RequestTimeout = 408,
		Conflict = 409,
		Gone = 410,
		LengthRequired = 411,
		PreconditionFailed = 412,
		PayloadTooLarge = 413,
		UnsupportedMediaType = 415,
		UnprocessableEntity = 422,
		TooManyRequests = 429,

		// 5xx Server Errors
		ServerError = 500,
		NotImplemented = 501,
		BadGateway = 502,
		ServiceUnavailable = 503,
		GatewayTimeout = 504,
		HttpVersionNotSupported = 505
	}

	public class ResultDto
	{
		public bool IsSuccess { get; private set; } 
		public string Message { get; private set; }
		public ResultStatusCode StatusCode { get; private set; }
		public IEnumerable<string> Errors { get; private set; } 

		protected ResultDto(bool isSuccess, string message, ResultStatusCode statusCode)
		{
			IsSuccess = isSuccess;
			Message = message;
			StatusCode = statusCode;
		}

		public static ResultDto Success(string message = "Operation completed successfully.", ResultStatusCode statusCode = ResultStatusCode.Success)
		{
			return new ResultDto(true, message, statusCode);
		}

		public static ResultDto Failure(string message, ResultStatusCode statusCode = ResultStatusCode.BadRequest /*, IEnumerable<string> errors = null*/)
		{
			// return new ResultDto(false, message, statusCode) { Errors = errors };
			return new ResultDto(false, message, statusCode);
		}

		// متدهای کمکی برای خطاهای رایج
		public static ResultDto NotFound(string message = "Resource not found.") => Failure(message, ResultStatusCode.NotFound);
		public static ResultDto Unauthorized(string message = "Unauthorized access.") => Failure(message, ResultStatusCode.Unauthorized);

	}

	public class ResultDto<T> : ResultDto
	{
		public T Data { get; private set; }

		private ResultDto(bool isSuccess, string message, ResultStatusCode statusCode, T data)
			: base(isSuccess, message, statusCode)
		{
			Data = data;
		}

		public static ResultDto<T> Success(T data, string message = "Operation completed successfully.", ResultStatusCode statusCode = ResultStatusCode.Success)
		{
			return new ResultDto<T>(true, message, statusCode, data);
		}

		public new static ResultDto<T> Failure(string message, ResultStatusCode statusCode = ResultStatusCode.BadRequest /*, IEnumerable<string> errors = null*/)
		{
			// return new ResultDto<T>(false, message, statusCode, default(T)) { Errors = errors };
			return new ResultDto<T>(false, message, statusCode, default(T));
		}

		// متدهای کمکی برای خطاهای رایج در نسخه جنریک
		public new static ResultDto<T> NotFound(string message = "Resource not found.") => Failure(message, ResultStatusCode.NotFound);
		public new static ResultDto<T> Unauthorized(string message = "Unauthorized access.") => Failure(message, ResultStatusCode.Unauthorized);

	}
}
