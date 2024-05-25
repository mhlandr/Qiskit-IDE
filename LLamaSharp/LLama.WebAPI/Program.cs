using LLama.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<StatefulChatService>();
builder.Services.AddScoped<StatelessChatService>();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7136") // Adjust this to your frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();
