using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new[] { new CultureInfo("en-US") };
    options.SupportedUICultures = new[] { new CultureInfo("en-US") };
});

//Se crea la cadena e conexion que se utilizara.
//Si se necesitan hacer prueba en el projecto antes de crearlo como Microservicio se necesitara la cadena de conexion en el appsettings.json
//Aunque para crear el microservicio la cadena de conexion se creara en el Dockerfile y por orden de gerarquia se va a utilizar la cadena que tengas en el archivo Dockerfile antes que la del appsettings.json.
//Sin embargo donde sea que lo agregues, se debe hacer la configuracion de la cadena de conexion
//En este caso se pasa la cdena de conexion del Dockerfile que tendra el nombre DefaultConnection
builder.Services.AddDbContext<ProductContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Se agregara el uso de controladores y como es una Web API, no tiene el uso de controladores 
builder.Services.AddControllers();

var app = builder.Build();
app.UseRequestLocalization();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Si se utiliza autorizacion de coloca 
app.UseAuthorization();
//Se agrega el mapeo de controladores
app.MapControllers();
app.Run();
