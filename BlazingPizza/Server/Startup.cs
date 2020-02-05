using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazingPizza.Server.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BlazingPizza.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddDbContext<PizzaStoreContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme =
               CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey =
                "U9DbAaVcDPYO3RVFlDo4w";
                twitterOptions.ConsumerSecret =
                "l6HWZa8F5MJmbBkGSzL6gMjgZMererT5KROxAzws9o";
                twitterOptions.Events.OnRemoteFailure = (context) =>
                {
                    context.HandleResponse();
                    return context.Response.WriteAsync(
                     "<script>window.close();</script>");
                };
            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
