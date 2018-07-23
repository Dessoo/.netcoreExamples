using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;
using SignalRSelfHost.IoC;
using System.Threading;

namespace SignalRSelfHost
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
            services.AddResponseCompression();
            services.AddSignalR();         
            services.AddSingleton(Configuration);
          
            DependencyIoC.InjectDataAccess.Init(services);
            DependencyIoC.InjectBusinessLayer.Init(services);
            IoCSettings.InitIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();
            app.UseSignalR(routes =>
            {
                routes.MapHub<TestHub>($"/{nameof(TestHub)}");
            });
        }
    }
}
