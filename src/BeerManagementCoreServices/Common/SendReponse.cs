﻿using Microsoft.AspNetCore.Mvc;

namespace BeerManagement.Web.Common
{
    public class SendReponse
    {
        public static IActionResult ApiResponse(object result)
        {
            return new OkObjectResult(new ApiAttributes<object>
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            });
        }

        public static IActionResult ApiResponse(bool result, string message)
        {
            return new OkObjectResult(new ApiAttributes<object>
            {
                StatusCode = 200,
                Message = message
            });
        }

        public static IActionResult BadRequestObjectResult(bool result, string message)
        {
            return new BadRequestObjectResult(new ApiAttributes<object>
            {
                StatusCode = 400,
                Message = message
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