using TimerTtriggerGoSocket.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using TimerTtriggerGoSocket.DataAccess.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using TimerTtriggerGoSocket.DataAccess.DAO;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(TimerTriggerGoSocket.StartUp))]

namespace TimerTriggerGoSocket
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=GoSocket;Persist Security Info=True;User ID=avega;Password=f3l1p3v3g4;";
            builder.Services.AddDbContext<GoSocketDbContext>(
              options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            builder.Services.AddScoped<IXmlFilesDAO, XmlFilesDAO>();
            builder.Services.AddScoped<IReadXmlService, ReadXmlService>();

        }
    }
}