using CandidateAPI.JWTDataBase;
using CandidateAPI.Services.AuthService;
using CandidateAPI.Services.CandidateService;
using CandidateAPI.Services.Empresas;
using CandidateAPI.Services.Habilidades;
using CandidateAPI.Services.Ofertas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IOfertaService, OfertaService>();
builder.Services.AddScoped<JWTDbContext>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IHabilidadesService, HabilidadService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"))
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JWTDbContext>(options =>
options.UseInMemoryDatabase("JWTDataBase"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); // Habilitar la pol?tica de CORS

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<JWTDbContext>();
    context.Database.EnsureCreated(); // fuerza a que se creen las tablas y se inserten los datos HasData
}

app.Run();
