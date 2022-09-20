using IoTPlatform.Domain.Repositories;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Data.Interfaces;
using IoTPlatform.Infrastructure.Repositories;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services;
using IoTPlatform.Infrastructure.Services.Interfaces;

namespace IoTPlatform
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
            builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("MongoDB"));

            // Database
            builder.Services.AddSingleton<IDatabaseSetting, DatabaseSetting>();
            builder.Services.AddScoped<IMongoContext, MongoContext>();

            // Base
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(MongoRepository<>));

            // Device 
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();

            // Device Profile
            builder.Services.AddScoped<IDeviceProfileRepository, DeviceProfileRepository>();
            builder.Services.AddScoped<IDeviceProfileService, DeviceProfileService>();

            // Customer
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            // Rule Chain
            builder.Services.AddScoped<IRuleChainRepository, RuleChainRepository>();
            builder.Services.AddScoped<IRuleChainService, RuleChainService>();

            // Server Attribute
            builder.Services.AddScoped<IServerAttributeRepository, ServerAttributeRepository>();
            builder.Services.AddScoped<IServerAttributeService, ServerAttributeService>();

            // Client Attribute
            builder.Services.AddScoped<IClientAttributeRepository, ClientAttributeRepository>();
            builder.Services.AddScoped<IClientAttributeService, ClientAttributeService>();

            // Shared Attribute
            builder.Services.AddScoped<ISharedAttributeRepository, SharedAttributeRepository>();
            builder.Services.AddScoped<ISharedAttributeService, SharedAttributeService>();

            // Telemetry
            builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();
            builder.Services.AddScoped<ITelemetryService, TelemetryService>();

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

