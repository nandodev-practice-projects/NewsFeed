using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsFeed.Models.Data;
using NewsFeed.Models.Identity;
using System;
using System.IO;

namespace NewsFeed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder
                            .UseKestrel()
                            .UseIIS()
                            .UseStartup<Startup>();
                    }).Build();

                //host.Run();
                //var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                        IdentityDbInitializer.Initialize(context, userManager, roleManager);
                        DbInitializer.Initialize(context);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
