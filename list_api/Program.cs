using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using System.Text;
using FluentValidation;
using list_api.Data;
using list_api.Repository.Interface;
using list_api.Repository;
using list_api.Middlewares;
using System.Reflection;
using list_api.Services;
using list_api.Security;
namespace list_api {
	public class Program { // Constructing.
		public static void Main(string[] args) {
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			ConfigurationManager configuration = builder.Configuration;
			builder.Services.AddControllers();
			builder.Services.AddScoped<IAuthRepository, AuthRepository>();
			builder.Services.AddScoped<IBrandRepository, BrandRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IClientRepository, ClientRepository>();
			builder.Services.AddScoped<IListRepository, ListRepository>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IRoleRepository, RoleRepository>();
			builder.Services.AddScoped<IStatusRepository, StatusRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IListApiDbContext>(provider => provider.GetService<ListApiDbContext>()!);
			builder.Services.AddDbContext<ListApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
			builder.Services.AddStackExchangeRedisCache(options => options.Configuration = configuration["RedisCacheUrl"]);
			builder.Services.Configure<RabbitMQConfiguration>(rabbitmq_configuration => configuration.GetSection(nameof(RabbitMQConfiguration)).Bind(rabbitmq_configuration));
			builder.Services.AddScoped<IMessageService, RabbitMQService>();
			builder.Services.AddScoped<IEncryptor, SHA256Encryptor>();
			builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
			//builder.Services.AddFluentValidationAutoValidation();
			//builder.Services.AddFluentValidationClientsideAdapters();
			//builder.Services.AddValidatorsFromAssemblyContaining<Program>();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options => {
				options.SwaggerDoc("v1", new OpenApiInfo() { Title = "list_app_auth", Version = "v1" });
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() { Name = "Authorization", Description = "Please enter a valid token:", Type = SecuritySchemeType.ApiKey, Scheme = "bearer", BearerFormat = "JWT", In = ParameterLocation.Header });
				options.AddSecurityRequirement(new OpenApiSecurityRequirement() { { new OpenApiSecurityScheme { Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
			});
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false, ValidateIssuer = false, ValidateLifetime = true, ValidateIssuerSigningKey = true, ValidIssuer = configuration["Token:Issuer"], ValidAudience = configuration["Token:Audience"], IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!)), ClockSkew = TimeSpan.Zero });
			var app = builder.Build();
			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<LoggingMiddleware>();
			app.MapControllers();
			app.Run();
		}
	}
}