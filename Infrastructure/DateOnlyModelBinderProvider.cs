using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryBuddy.Infrastructure.ModelBinding;

public class DateOnlyModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DateOnly) ||
            context.Metadata.ModelType == typeof(DateOnly?))
        {
            return new DateOnlyModelBinder();
        }

        return null;
    }
}
