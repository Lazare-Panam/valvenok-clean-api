using Microsoft.AspNetCore.Mvc;
using Valvenok.Core.Common.Errors;

namespace Valvenok.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result) =>
            result.IsSuccess
                ? new OkResult()
                : Problem(result.Error!);

        public static IActionResult ToActionResult<TValue>(this Result<TValue> result) =>
            result.IsSuccess
                ? new OkObjectResult(result.Value)
                : Problem(result.Error!);

        private static IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return new ObjectResult(new { code = error.Code, message = error.Description })
            {
                StatusCode = statusCode
            };
        }
    }
}
