using Library.Business.Validation;

namespace Library.API.Extensions;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddSingleton<BookValidator>();
        services.AddSingleton<IdValidator>();
        services.AddSingleton<IsbnValidator>();
        services.AddSingleton<AccountValidator>();
        services.AddSingleton<AuthorValidator>();

        return services;
    }
}