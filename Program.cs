using Microsoft.OpenApi.Models;
using BankAccountAPI.Data;
using Microsoft.EntityFrameworkCore;
using BankAccountAPI.Repository;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BankAccountDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Account", new OpenApiInfo
    {
        Title = "Bank API",
        Version = "1.0",
        Description = "Uma web API para gerenciamento de contas bancárias."
    });  
});

builder.Services.AddControllers();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddHostedService<SavingsYieldServices>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/Account/swagger.json", "Bank Account API v1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
