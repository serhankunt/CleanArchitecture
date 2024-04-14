using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Options;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly,
                typeof(AppUser).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        var emailOptions = services.BuildServiceProvider().GetRequiredService<IOptions<EmailOptions>>().Value;    

        services
        .AddFluentEmail(emailOptions.Email)
        .AddSmtpSender(
            emailOptions.SMTP,
            emailOptions.PORT
            //emailOptions.Email,
            //emailOptions.Password
            );
            //builder.Configuration.GetSection("Email:Email").Value,
            //builder.Configuration.GetSection("Email:Password").Value);

        return services;
    }
}
