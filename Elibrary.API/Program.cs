using ELibrary.Data;
using ELibrary.Data.Contract;
using ELibrary.Data.Implementation;
using ELibrary.Service.Contract;
using ELibrary.Service.Implementation;
using ELibrary.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
//CORS
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Service Manager DI
builder.Services.AddScoped<IRepositoryServiceManager, RepositoryServiceManager>();

builder.Services.AddScoped(serviceType: typeof(IUnitOfWork), implementationType: typeof(UnitOfWork));

//repos
builder.Services.AddScoped(serviceType: typeof(ICoreRepo<>), implementationType: typeof(CoreRepo<>));

// auto mapper configuration
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

//CORS
app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true)
                 .AllowCredentials());

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
