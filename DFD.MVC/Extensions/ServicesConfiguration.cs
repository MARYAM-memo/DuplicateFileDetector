using DFD.Application.Validations.Folders;
using DFD.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace DFD.MVC.Extensions;

public static class ServicesConfiguration
{
      public static void DatabaseConnection(this IServiceCollection services, WebApplicationBuilder builder)
      {
            var databaseCs = builder.Configuration.GetConnectionString("DatabaseCS");
            services.AddDbContext<DatabaseContext>(op => op.UseNpgsql(databaseCs));
      }

      public static void AddFluentValidatore(this IServiceCollection services)
      {
            services.AddFluentValidationAutoValidation(config =>
            {
                  config.DisableDataAnnotationsValidation = true;
            });
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<FolderCreateValidator>();

      }
}
