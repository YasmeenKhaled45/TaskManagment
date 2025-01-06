using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.BuisnessLogic.DataSantization
{
    public class InputSanitizationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
           
            foreach (var parameter in context.ActionArguments)
            {
                var value = parameter.Value;

                if (value is not null && value.GetType().IsClass)
                {
                    foreach (var prop in value.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            var propValue = (string?)prop.GetValue(value);
                            if (propValue != null)
                            {
                                var sanitizedValue = Sanitizer.GetSafeHtmlFragment(propValue);
                                prop.SetValue(value, sanitizedValue); 
                            }
                        }
                    }
                }
                else if (value is string stringValue) 
                {
                    context.ActionArguments[parameter.Key] = Sanitizer.GetSafeHtmlFragment(stringValue);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }

}
