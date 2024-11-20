using elasticsearch_demo_project.Contexts;
using elasticsearch_demo_project.Interfaces;
using elasticsearch_demo_project.Repositories;
using elasticsearch_demo_project.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

#region Elasticsearch
//builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("Elasticsearch"));
//builder.Services.AddSingleton(s =>
//{
//    var settings = s.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
//    var clientSettings = new ElasticsearchClientSettings(new Uri(settings.Uri))
//        .DefaultIndex(settings.IndexName);
//    return new ElasticsearchClient(clientSettings);
//});
builder.Services.AddSingleton<ElasticsearchService>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
