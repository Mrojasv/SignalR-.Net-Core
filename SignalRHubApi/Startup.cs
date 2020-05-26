using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRHubApi.Hubs;

namespace SignalRHubApi
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
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(options =>
            //{
            //    // Identity made Cookie authentication the default.
            //    // However, we want JWT Bearer Auth to be the default.
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(options =>
            //    {
            //        // Configure the Authority to the expected value for your authentication provider
            //        // This ensures the token is appropriately validated
            //        options.Authority = "https://localhost:44391";

            //        // We have to hook the OnMessageReceived event in order to
            //        // allow the JWT authentication handler to read the access
            //        // token from the query string when a WebSocket or 
            //        // Server-Sent Events request comes in.

            //        // Sending the access token in the query string is required due to
            //        // a limitation in Browser APIs. We restrict it to only calls to the
            //        // SignalR hub in this code.
            //        // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
            //        // for more information about security considerations when using
            //        // the query string to transmit the access token.
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                var accessToken = context.Request.Query["access_token"];

            //                // If the request is for our hub...
            //                var path = context.HttpContext.Request.Path;
            //                if (!string.IsNullOrEmpty(accessToken) &&
            //                    (path.StartsWithSegments("/ChatHub")))
            //                {
            //                    // Read the token out of the query string
            //                    context.Token = accessToken;
            //                }
            //                return Task.CompletedTask;
            //            }
            //        };
            //    });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .WithOrigins("http://localhost:61085", "http://localhost:62027")
                           .AllowCredentials();
                }));

            services.AddControllers();
            services.AddSignalR();

            // Change to use Name as the user identifier for SignalR
            // WARNING: This requires that the source of your JWT token 
            // ensures that the Name claim is unique!
            // If the Name claim isn't unique, users could receive messages 
            // intended for a different user!
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddHostedService<Worker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
