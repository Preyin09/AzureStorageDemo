using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Azure.Data.Tables;
using AzureStorageDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Azure configuration from appsettings.json
string blobConn = builder.Configuration["Azure:BlobStorageConnectionString"];
string queueConn = builder.Configuration["Azure:QueueStorageConnectionString"];
string tableConn = builder.Configuration["Azure:TableStorageConnectionString"];
string fileConn = builder.Configuration["Azure:FileShareConnectionString"];

// Register services
builder.Services.AddScoped<IBlobServices, BlobServices>();
builder.Services.AddScoped<IQueueServices, QueueServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IFileShareServices, FileShareServices>();

// Azure clients
builder.Services.AddSingleton(new BlobServiceClient(blobConn));
builder.Services.AddSingleton(new QueueServiceClient(queueConn));
builder.Services.AddSingleton(new TableServiceClient(tableConn));
builder.Services.AddSingleton(new ShareServiceClient(fileConn));

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
