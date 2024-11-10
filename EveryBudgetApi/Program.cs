using EveryBudgetApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
    IConfiguration _configuration = configurationBuilder.Build();
    var rdsDbString = _configuration.GetConnectionString("LocalDatabase");

// Add services to the container.
builder.Services.AddDbContext<EveryBudgetDbContext>(opt =>
                   opt.UseNpgsql(rdsDbString));
/*
* Create a CORS policy to allow any origin, any header, and any method.
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DevelopmentPolicy",
        policy =>
        {
            // NOTE : Specific origins must be specified for CORS to work in production
            policy
            //.SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyOrigin()
                //.WithOrigins(
                //    "http://localhost:3000"
                //)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers(); // options => { options.AllowEmptyInputInBodyModelBinding = true; }
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DevelopmentPolicy");

// app.UseAuthorization();

app.MapControllers();

app.Run();
