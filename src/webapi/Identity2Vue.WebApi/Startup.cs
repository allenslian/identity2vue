using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Identity2Vue.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity2Vue.WebApi", Version = "v1" });
      });

      services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
            options.Authority = Configuration["JwtToken:Authority"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateAudience = false
            };
          });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("platform.scopes", policy =>
        {
          policy.RequireAuthenticatedUser();
          policy.RequireAssertion(ctx =>
          {
            return null != ctx.User.Claims.FirstOrDefault(c => c.Type == "scope" && c.Value == "platform.api");
          });
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity2Vue.WebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers()
          //.RequireAuthorization("platform.scopes")
          ;
      });
    }
  }
}
