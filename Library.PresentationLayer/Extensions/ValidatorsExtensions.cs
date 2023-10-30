using Library.BusinessLayer.Validation;

namespace Library.PresentationLayer.Extensions;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<BookValidator>();
        services.AddScoped<IdValidator>();
        services.AddScoped<IsbnValidator>();
        services.AddScoped<UserValidator>();
        services.AddScoped<AuthorValidator>();

        return services;
    }
}