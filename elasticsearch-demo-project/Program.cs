using Elastic.Clients.Elasticsearch;
using elasticsearch_demo_project.Services;
using elasticsearch_demo_project.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("Elasticsearch"));
builder.Services.AddSingleton(s =>
{
    var settings = s.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
    var clientSettings = new ElasticsearchClientSettings(new Uri(settings.Uri))
        .DefaultIndex(settings.IndexName);
    return new ElasticsearchClient(clientSettings);
});

builder.Services.AddSingleton<ElasticsearchService>();

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
