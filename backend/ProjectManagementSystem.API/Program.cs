using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductManagementSystem.BLL.Services.Auth;
using ProductManagementSystem.BLL.Services.Categories;
using ProductManagementSystem.BLL.Services.Products;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;
using ProductManagementSystem.DAL.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using ProductManagementSystem.BLL.Services.Roles;
using ProductManagementSystem.BLL.Mapping;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.BLL.Interfaces.Services.Categories;
using ProductManagementSystem.BLL.Interfaces.Services.Auth;
using ProductManagementSystem.BLL.Interfaces.Services.Roles;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequireDigit = false;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireLowercase = false;
});

builder.Services
    .AddAuthentication(options =>
    {
        // use JWT Bearer for both authenticating and challenging
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),

            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagementSystem API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT as: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer" }
            },
            new string[] { }
        }
    });
});
builder.Services.AddCors(o => o.AddPolicy("ReactDev", policy =>
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
));

builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly, typeof(CategoryMappingProfile).Assembly);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

builder.Services.AddScoped<ICreateProductService, CreateProductService>();
builder.Services.AddScoped<IGetProductService, GetProductService>();
builder.Services.AddScoped<IGetAllProductDetailsService, GetAllProductDetailsService>();
builder.Services.AddScoped<IUpdateProductService, UpdateProductService>();
builder.Services.AddScoped<IPatchProductService, PatchProductService>();
builder.Services.AddScoped<IDeleteProductService, DeleteProductService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICreateCategoryService, CreateCategoryService>();
builder.Services.AddScoped<IGetCategoryService, GetCategoryService>();
builder.Services.AddScoped<IListCategoriesService, ListCategoriesService>();
builder.Services.AddScoped<IUpdateCategoryService, UpdateCategoryService>();
builder.Services.AddScoped<IPatchCategoryService, PatchCategoryService>();
builder.Services.AddScoped<IDeleteCategoryService, DeleteCategoryService>();

builder.Services.AddScoped<IListRoles, ListRoles>();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "wwwroot", "uploads")
    ),
    RequestPath = "/uploads"
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("ReactDev");

app.MapControllers();

app.Run();