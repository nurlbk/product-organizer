using Microsoft.AspNetCore.Hosting;
using proj1;

namespace TimetableApi {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, configuration) => {
                //
            })
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });

    }
}