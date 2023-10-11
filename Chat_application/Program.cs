
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.EntityDB;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Context(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IParticipantsService, ParticipantsService>();
builder.Services.AddScoped<IUserService, UserService>();
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
