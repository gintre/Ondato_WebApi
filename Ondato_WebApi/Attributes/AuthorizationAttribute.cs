using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Ondato_WebApi.Utils.Constants;
using System;

namespace Ondato_WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Method)
    ]
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        public AuthorizationAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting
               (ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue
             (ConfigurationConstants.AuthorizationApiKey, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            var apiKey = _configuration.GetValue<string>("Authorization:ApiKey");

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }
        }
    }
}
