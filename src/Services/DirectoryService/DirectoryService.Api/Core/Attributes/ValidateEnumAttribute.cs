using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Api.Core.Attributes
{
    public class ValidateEnumAttribute : ActionFilterAttribute
    {
        private readonly Type _enumType;

        public ValidateEnumAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                var propertyName = actionArgument.Key;
                var propertyValue = actionArgument.Value;
                var properties = propertyValue.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        var enumValues = Enum.GetValues(property.PropertyType);
                        var enumValue = property.GetValue(propertyValue);

                        if (!enumValues.Cast<object>().Contains(enumValue))
                        {
                            context.Result = new BadRequestObjectResult($"Invalid value for {propertyName}.{property.Name}.");
                        }
                    }
                }
            }
        }
    }
}
