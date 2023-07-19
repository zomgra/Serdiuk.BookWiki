using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.Persistance;
using Serdiuk.Persistance.Data;
using Serdiuk.Persistance.Mapper;
using Serdiuk.Services.Interfaces;
using Serdiuk.Services.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var jwtConfig = new JwtConfig(builder.Configuration);

builder.Services.AddAutoMapper(typeof(ApplicationMapper).Assembly);

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(b =>
    {
        b.AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader()
        .WithOrigins("https://localhost:7288");
    });
});

builder.Services.AddResponseCaching();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(c =>
{
    c.SignIn.RequireConfirmedPhoneNumber = false;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredLength = 5;
    c.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
     opt.UseSqlite(builder.Configuration.GetConnectionString("SQLite"), b=>b.MigrationsAssembly("Serdiuk.API"));
    //opt.UseInMemoryDatabase("Test DB");
});

builder.Services.AddAuthorization(options =>
      options.AddPolicy("Admin", b => b.RequireAssertion(context =>
      {
          return context.User.FindFirstValue(ClaimTypes.Role).Contains(AppData.Administrator);
      })
      ));

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(c =>
    {
        c.SaveToken = true;

        c.TokenValidationParameters = jwtConfig.GetTokenValidationParameters();
    });

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();
await DataInitializer.Intialize(app.Services.CreateScope().ServiceProvider);

app.Run();
