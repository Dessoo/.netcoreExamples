using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.XmlProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace DependencyIoC
{
    public static class InjectDataAccess
    {
        public static void Init(IServiceCollection services)
        {
            services.AddDbContext<TestContext>(options => options
                .UseSqlServer(services.BuildServiceProvider().
                                 GetService<IConfiguration>().
                                 GetConnectionString("TestDatabase")));

            var serviceResolver = services.BuildServiceProvider();

            //start inject repos/uoW??
            services.AddScoped<IUserRepository>(s => new UserRepository(serviceResolver.GetService<TestContext>()));
            services.AddScoped<IEventLogRepository>(s => new EventLogRepository(serviceResolver.GetService<TestContext>()));

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            services.AddSingleton<IXmlDataProvider<EventLog>>(s => new XmlDataProvider<EventLog>(Path.GetDirectoryName(path), $"{nameof(EventLog)}", "Logs"));
        }
    }
}
