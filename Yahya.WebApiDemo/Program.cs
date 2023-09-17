using Yahya.WebApiDemo.CustomMiddlewares;
using Yahya.WebApiDemo.DataAcces;
using Yahya.WebApiDemo.Formatters;

namespace Yahya.WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IProductDal,EfProductDal>();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new VCardOutputFormatter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<AuthenticationMiddleware>();
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