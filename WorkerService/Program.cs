using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services
// Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Server=localhost\SQLEXPRESS;Initial Catalog=HangfireDB;Integrated Security=True;Pooling=False"));
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
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

app.UseHangfireDashboard();

app.UseAuthorization();
app.MapControllers();

app.Run();
