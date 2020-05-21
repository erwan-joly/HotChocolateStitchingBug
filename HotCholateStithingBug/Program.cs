using System;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Stitching;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HotCholateStithingBug
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder2(args).Build().RunAsync();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5050")
                .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder2(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5051")
                .UseStartup<Startup2>();
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataLoaderRegistry();

            services.AddGraphQLSubscriptions();

            services.AddHttpClient("stitched", (sp, client) =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5051");
            });

            services.AddStitchedSchema(builder => builder
                .AddSchemaFromHttp("stitched"));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL();
        }
    }

    public class Startup2
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataLoaderRegistry();

            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<QueryType2>();
            }));

            services.AddGraphQLSubscriptions();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL();
        }
    }
}
