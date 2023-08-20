using ITPLibrary.Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

configurationBuilder.AddEnvironmentVariables();

var Configuration = configurationBuilder.Build();

var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Configuration[Constants.DatabaseConnectionString]));

services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

services.AddScoped<AppDbContext>();


services.AddScoped<IBookService, BookService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IShoppingCartService, ShoppingCartService>();
services.AddScoped<IOrderService, OrderService>();


using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
bool useRepositoryPattern = configuration.GetValue<bool>(Constants.UseRepositoryPattern);

if (useRepositoryPattern)
{
    services.AddScoped<IUserManagementDataProvider, UserManagementDataProvider>();
    services.AddScoped<IBookDataProvider, BookRepository>();
    services.AddScoped<IShoppingCartDataProvider, ShoppingCartRepository>();
    services.AddScoped<IOrderDataProvider, OrderRepository>();
}
else
{
    services.AddScoped<IUserManagementDataProvider, UserManagementDataProvider>();
    services.AddScoped<IBookDataProvider, BookDataProvider>();
    services.AddScoped<IShoppingCartDataProvider, ShoppingCartDataProvider>();
    services.AddScoped<IOrderDataProvider, OrderDataProvider>();
}

services.AddAuthentication(options =>
         {
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
         })
        .AddJwtBearer(options =>
         {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidAudience = Configuration[Constants.JwtAudience],
                 ValidIssuer = Configuration[Constants.JwtIssuer],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JwtKey]))
             };
         });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITPLibrary API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
