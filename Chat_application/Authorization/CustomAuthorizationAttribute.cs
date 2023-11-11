using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string _correctCode = "123";
        public CustomAuthorizationAttribute(string correctCode)
        {
            _correctCode = correctCode;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string enteredCode = context.HttpContext.Request.Headers["AuthorizationCode"].ToString();

            if (enteredCode == _correctCode)
            {
                // Код совпадает, аутентификация прошла успешно
            }
            else
            {
                // Код не совпадает, возвращаем ошибку
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
