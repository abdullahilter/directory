using entity.common;
using entity.report;
using service.common;
using service.report;

var builder = WebApplication.CreateBuilder(args);

var mongoDbOptions = builder.Configuration.GetSection(nameof(MongoDbOptions)).Get<MongoDbOptions>();
var rabbitMqOptions = builder.Configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();

builder.Services
    .AddMongoDb(mongoDbOptions)
    .AddMongoDbRepository<Report>()
    .AddMassTransitWithRabbitMq(rabbitMqOptions)
    .AddRefitWithRetryPolicy<IContactApi>(builder.Configuration[nameof(IContactApi)]);

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();