using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Application.Queries;
using WebApiCleanWithMediatr.Domain.DTO;
using WebApiCleanWithMediatr.GlobalExceptionHandler;
using WebApiCleanWithMediatr.Identity;
using WebApiCleanWithMediatr.Identity.Models;
using WebApiCleanWithMediatr.Infrastructure;
using WebApiCleanWithMediatr.Infrastructure.Repository;
using WebApiCleanWithMediatr.Infrastructure.Services;
using WebApiCleanWithMediatr.Utitlity;
using static WebApiCleanWithMediatr.Application.Commands.CreateCategorysCommand;
using static WebApiCleanWithMediatr.Application.Commands.CreateOrderCommand;
using static WebApiCleanWithMediatr.Application.Queries.GetCategoryByIdQuery;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

var connectionString1 = builder.Configuration.GetConnectionString("default1");
var connectionString2 = builder.Configuration.GetConnectionString("default2");

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString1));

builder.Services.AddDbContext<IdentityDbContextNew>(options =>
        options.UseSqlServer(connectionString2));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<IdentityDbContextNew>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddTransient<IRequestHandler<CreateProductCommand, ProductDto>, CreateProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQueryHandler>();
builder.Services.AddTransient<IProductService, ProductService>();


// Registering Category services and handlers
builder.Services.AddTransient<IRequestHandler<CreateCategorysCommand, CategoryDto>, CreateCategoryCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryByIdQuery, CategoryDto>, GetCategoryByIdQueryHandler>();
builder.Services.AddTransient<ICategorysService, CategoryService>();

// Registering Order services and handlers
builder.Services.AddTransient<IRequestHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddControllers();


builder.Services.AddScoped(typeof(IGenericRepository<Student>), typeof(AnotherGenericRepository<Student>));
builder.Services.AddScoped<IDBSeeding, DBSeeding>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
//builder.Services.AddScoped<IDBSeeding, DBSeeding>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbseeding = scope.ServiceProvider.GetRequiredService<IDBSeeding>();
    dbseeding.Initialize();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
