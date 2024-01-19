var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
    IConfiguration _configuration = configurationBuilder.Build();
    var rdsDbString = _configuration.GetConnectionString("LocalDatabase");

// Add services to the container.
builder.Services.AddDbContext<EveryBudgetDbContext>(opt =>
                   opt.UseNpgsql(rdsDbString));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
