using GenAiApp.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IOpenAiService, OpenAiServises>();
builder.Services.AddScoped<IWhisperService, WhisperService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("Angular");

app.UseAuthorization();

app.MapControllers();

app.Run();