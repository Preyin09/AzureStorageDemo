using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Files.Shares;
using AzureStorageDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Azure Storage connection string
var connectionString = builder.Configuration.GetConnectionString("AzureStorage");

// Register Azure clients
builder.Services.AddSingleton(new TableServiceClient(connectionString));
builder.Services.AddSingleton(new BlobServiceClient(connectionString));
builder.Services.AddSingleton(new QueueServiceClient(connectionString));
builder.Services.AddSingleton(new ShareServiceClient(connectionString));

// Register custom services
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IBlobServices, BlobServices>();
builder.Services.AddScoped<IQueueServices, QueueServices>();
builder.Services.AddScoped<IFileShareServices, FileShareServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
