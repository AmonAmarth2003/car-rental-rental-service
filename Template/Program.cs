using Microsoft.EntityFrameworkCore;
using Retail.API.Servicos;
using Template.Data;
using Template.Infra;
using Template.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddHttpClient<IClientServiceCaller, ClientServiceCaller>(client =>
    client.BaseAddress = new Uri(builder.Configuration["Services:ClientService"] ?? "http://localhost:5090"));

builder.Services.AddHttpClient<IVehicleServiceCaller, VehicleServiceCaller>(client =>
    client.BaseAddress = new Uri(builder.Configuration["Services:VehicleService"] ?? "http://localhost:5090"));

// Register rental service implementation
builder.Services.AddScoped<IRentalService, RentalService>();

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
