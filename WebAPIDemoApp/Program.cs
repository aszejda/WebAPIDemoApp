using DataAccess.DbAccess;
using WebAPIDemoApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess,  SqlDataAccess>();
builder.Services.AddSingleton<IProductInfoData, ProductInfoData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

app.UseHttpsRedirection();
app.ConfigureApi();
app.Run();

