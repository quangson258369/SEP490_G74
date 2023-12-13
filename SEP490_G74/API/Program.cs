using API;
using AutoMapper;
using DataAccess.Entity;
using HcsBE.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HcsContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MyCnn")
));
builder.Services.AddScoped<HcsContext>();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new PatientMapper());
    mc.AddProfile(new DoctorMapper());
    mc.AddProfile(new ServiceMapper());
    mc.AddProfile(new MedicalRCMapper());
    mc.AddProfile(new ExaminationResultMapper());
    mc.AddProfile(new PrescriptionMapper());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSession();
builder.Services.AddSingleton(mapper);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7115", builder =>
    {
        builder.WithOrigins("https://localhost:7115")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
startup.ConfigureServices(builder.Services);
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Define a security requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowLocalhost7115");
app.UseHttpsRedirection();

app.UseRouting();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
