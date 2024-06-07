// using storage_api.Models;

using storage_api.Models;
using storage_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DatasetService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<ImagesService>();
builder.Services.AddSingleton<LabelsService>();
builder.Services.AddSingleton<LabelsService>();
builder.Services.AddSingleton<StorageContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle | reiks istrint
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<StorageContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
