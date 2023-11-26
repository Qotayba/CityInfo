using CityInfo.API.DbContexts;
using CityInfo.API.Middelware;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().
                MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/cityInfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            // builder.Logging.AddConsole();

            // Add services to the container.
            builder.Host.UseSerilog();
            builder.Services.AddControllers(
                options => options.ReturnHttpNotAcceptable = true
                ).AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
          

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //this statment allows us to ingect a file extension contant type provider in the other parts of the code 
            builder.Services.AddDbContext<CityInfoContext>();

           



            #if DEBUG
            builder.Services.AddTransient<IMailService, LocalMailService >();
            #else
            builder.Services.AddTransient<IMailService, CloudMailService >();
#endif
            builder.Services.AddScoped<ICityInfoRepositry,CityInfoRepositry>();
            builder.Services.AddSingleton<CitiesDataStore>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.Use(async (context, next) =>
            //{
            //    var start = DateTime.UtcNow;
            //    await next.Invoke(context);
            //    app.Logger.LogInformation($"Reqest {context.Request.Path} :{(DateTime.UtcNow - start).TotalMilliseconds} ms");
            //});
            app.UseMiddleware<TimingMiddleWare>();
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>{
                endpoints.MapControllers();
                    }
            );
           



            app.Run();
        }
    }
}