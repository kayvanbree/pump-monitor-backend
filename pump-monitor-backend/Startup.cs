using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pump_monitor_backend.Services;

namespace pump_monitor_backend
{
    public class Startup
    {
        private readonly string AllowClient = "AllowClient";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowClient,
                    builder => 
                        builder.WithOrigins(
                                "http://localhost:4200",
                                "https://pump-monitor-staging.herokuapp.com"
                            )
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });
            
            services.AddMemoryCache()
                .AddSingleton(new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(int.Parse(Configuration["BINANCE_CACHE_EXPIRATION"]))));
            services.AddTransient<ISystemService, SystemService>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["OKTA_OAUTH2_ISSUER"];
                    options.Audience = Configuration["OKTA_DEFAULT_AUDIENCE"];
                });

            // Use [AllowAnonymous] for going into an endpoint raw
            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter());
            });

            services.AddAuthorization();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();
            
            app.UseRouting();
            app.UseCors(AllowClient);
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(AllowClient);
            });
        }
    }
}