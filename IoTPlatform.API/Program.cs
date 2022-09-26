using IoTPlatform.Domain.Repositories;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Data.Interfaces;
using IoTPlatform.Infrastructure.Repositories;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IoTPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
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

            // AuditLog
            builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            builder.Services.AddScoped<IAuditLogService, AuditLogService>();

            // User
            builder.Services.AddScoped<IUserService, UserService>();

            // HTTP Context Accessor
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example:\"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2020_09_07$vinhnq";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //what to validate
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    //setup validate data
                    ValidIssuer = "becanatomy",
                    ValidAudience = "user",
                    IssuerSigningKey = symmetricSecurityKey
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}

