using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PruebaAPI.DTO;
namespace PruebaAPI.Filters
{
    public class ValidaterFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        //Valida que el modelo de entrada sea valido en caso de no ser asi devuelve
        //un badRequets pero con con el dto de responseHTTP en lugar de la estructura personalizada de .NET
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //validar modelo
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                //instanciar objeto de respuesta
                var response = new ResponseHTTP<object>
                {
                    status = false,
                    Content = errors,
                    message = "Validation errors occurred.",

                };
                //devolver respuesta con el objecto cargado
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
