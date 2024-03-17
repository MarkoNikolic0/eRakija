var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProizvodContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("eRakija"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:4200",
                           "http://localhost:4200"
                           );
            
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


