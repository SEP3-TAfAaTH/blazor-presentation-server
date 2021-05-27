using System;
using System.Security.Claims;
using BlazorPresentationServer.Authentication;
using BlazorPresentationServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;

namespace BlazorPresentationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient<IAccountService, AccountService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });
            services.AddHttpClient<ILoginAccountService, LoginAccountService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });
            services.AddHttpClient<IStockService, StockService>(client =>
            {
                client.BaseAddress = new Uri("https://api.twelvedata.com");
            });
            services.AddHttpClient<ITransactionService, TransactionService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });
            services.AddSingleton<ICachedAccount, CachedAccount>();
            
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("LoggedIn", a => a.RequireAuthenticatedUser().RequireClaim("Login","true"));
            });
            
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}