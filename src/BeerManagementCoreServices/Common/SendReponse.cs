using Microsoft.AspNetCore.Mvc;
namespace BeerManagement.Web.Common
{
    public class SendReponse
    {
        public static IActionResult ReturnResponse(object result)
        {
            if (result == null)
            {
                return NoContentFound();
            }
            else
            {
                return new OkObjectResult(new ApiAttributes<object>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Result = result
                });
            }
        }

        public static IActionResult ReturnResponseByBooleanValue(bool result, string message)
        {
            if (!result)
            {
                return new BadRequestObjectResult(new ApiAttributes<object>
                {
                    StatusCode = 400,
                    Message = message
                });
            }
            else
            {
                return new OkObjectResult(new ApiAttributes<object>
                {
                    StatusCode = 200,
                    Message = message
                });
            }
        }
        public static IActionResult ApiResponse(object result)
        {
            return new OkObjectResult(new ApiAttributes<object>
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            });
        }
        public static IActionResult ApiResponse(bool result)
        {
            return new OkObjectResult(new ApiAttributes<object>
            {
                StatusCode = 200,
                Message = "Success"
            });
        }
        public static IActionResult BadRequestObjectResult(string input)
        {
            return new BadRequestObjectResult(new ApiAttributes<object>
            {
                StatusCode = 400,
                Message = $"Provided {input} input is invalid."
            });
        }
        public static IActionResult InternalServerError()
        {
            return new ObjectResult(new ApiAttributes<object>
            {
                StatusCode = 500,
                Message = "Unable to complete the operation due to invalid id or due to internal error."
            });
        }
        public static IActionResult BadRequest()
        {
            return new BadRequestResult();
        }
        public static IActionResult NoContentFound()
        {
            return new NotFoundResult();
        }
    }
}