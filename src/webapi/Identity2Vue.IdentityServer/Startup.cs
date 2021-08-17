// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Identity2Vue.IdentityServer
{
  public class Startup
  {
    public IWebHostEnvironment Environment { get; }

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      // uncomment, if you want to add an MVC-based UI
      services.AddControllersWithViews();

      var builder = services.AddIdentityServer(options =>
      {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseSuccessEvents = true;

        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
        options.EmitStaticAudienceClaim = true;
      })
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddCredential(Configuration["Credentials:RsaPrivateKey"])
      ;

      // services.AddAuthentication("Cookie")
      //   .AddCookie("Cookie", options =>
      //   {
          
      //   });
    }

    public void Configure(IApplicationBuilder app)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseRouting();

      app.UseIdentityServer();

      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
