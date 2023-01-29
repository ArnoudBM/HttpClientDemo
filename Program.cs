using HttpClientDemo.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IJokesClient, JokesClient>();
builder.Services.AddHttpClient("dadjokesapi", client =>
{
    var apiKey = GetApiKey(builder.Configuration);
    var baseAddress = new Uri("https://dad-jokes.p.rapidapi.com");

    client.BaseAddress = baseAddress;
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", baseAddress.Host);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static string GetApiKey(IConfiguration configuration)
{
    var apiKey = configuration["DadJokes:ApiKey"];

    if (apiKey is null)
        throw new InvalidOperationException("The user secret DadJokes:ApiKey is required.\r\nUse 'dotnet user-secrets init' and 'dotnet user-secrets set \"DadJokes:ApiKey\" \"<your API Key>\"'.");

    return apiKey;
}