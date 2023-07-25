using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(typeof(CreateCarWorkshopCommand));

            services.AddScoped(provider => new MapperConfiguration(conf =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                conf.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()   );

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                   .AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters();
            
        }
    }
}
