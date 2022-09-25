using api.contact.Entities;
using api.contact.Services;
using common.MongoDb;
using common.Options;

var builder = WebApplication.CreateBuilder(args);

var mongoDbOptions = builder.Configuration.GetSection(nameof(MongoDbOptions)).Get<MongoDbOptions>();

builder.Services
    .AddMongoDb(mongoDbOptions)
    .AddMongoDbRepository<Contact>()
    .AddMongoDbRepository<Communication>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICommunicationService, CommunicationService>();

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