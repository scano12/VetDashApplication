using Microsoft.Extensions.Configuration;
using VetDashBoard;
using VetDashBoard.Controllers;
using VetDashBoard.Services;
using VetDashBoard.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<IVetAppointmentsServices, VetAppointmentsservices>();
builder.Services.AddScoped<IVetAppointmentsServices, VetAppointmentsservices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddCors(o => 
{
	o.AddPolicy("AllowFromSpecificOrigin", policy => 
	{
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
		policy.WithOrigins("*");
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowFromSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
