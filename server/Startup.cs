using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using CryptobotUi.Data;

using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;

namespace CryptobotUi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAny",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            services.AddOData();
            services.AddODataQueryFilter();
            

            services.AddDbContext<CryptobotUi.Data.CryptodbContext>(options =>
            {
              options.UseNpgsql(Configuration.GetConnectionString("cryptodbConnection"));
            });

            services.AddRazorPages();
            services.AddLocalization();

            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("en-AU"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-AU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            OnConfiguring(app, env);

            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("en-AU"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-AU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.Count().Filter().OrderBy().Expand().Select().MaxTop(null).SetTimeZoneInfo(TimeZoneInfo.Utc);

                var oDataBuilder = new ODataConventionModelBuilder(provider);

                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Config>("Configs");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Exchange>("Exchanges");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.ExchangeOrder>("ExchangeOrders");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.MarketEvent>("MarketEvents");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Pnl>("Pnls");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Position>("Positions");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Signal>("Signals");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.SignalCommand>("SignalCommands");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Strategy>("Strategies");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.StrategyCondition>("StrategyConditions");
                oDataBuilder.EntitySet<CryptobotUi.Models.Cryptodb.Symbol>("Symbols");

                this.OnConfigureOData(oDataBuilder);

                var model = oDataBuilder.GetEdmModel();

                endpoints.MapODataRoute("odata", "odata/cryptodb", model);

            });

            OnConfigure(app, env);
        }
    }


}
