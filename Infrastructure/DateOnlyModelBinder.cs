using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace LibraryBuddy.Infrastructure.ModelBinding;

public class DateOnlyModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            bindingContext.Result = ModelBindingResult.Success(date);
            return Task.CompletedTask;
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid date format.");
        return Task.CompletedTask;
    }
}
