using Microsoft.EntityFrameworkCore;
using StockManagementProject.Repositories.Abstract;
using StockManagementProject.Repositories.Context;
using StockManagementProject.Service.Abstract;
using StockManagementProject.Service.Concreate;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StockManagementProject.Repositories.Concreate;

namespace StockManagementProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StockManagementProjectContext>(option =>
            {
                option.UseSqlServer("Server = DESKTOP-BODOH2U\\SA; Database = StockManagementDB; uid = sa; pwd = 1234");
            });
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}