// asp.net.core uygulamasýnýn yapýlandýrmak için bir builder tanýmlandý.
var builder = WebApplication.CreateBuilder(args);


// api uygulamalarýný otomatik olarak tanýmlasýný ve geliþtirilmesini saðlar.
builder.Services.AddEndpointsApiExplorer();
//swager dökümantasyonu oluþturmak için eklenir..
builder.Services.AddSwaggerGen();

// web application nesnesi oluþturuyoruz.
var app = builder.Build();

// uygulamalar eðer ki geliþtirme ortamýnda çalýþtýrýlýrsa etkinleþtirilecek middleware'leri gösteriyor.
if (app.Environment.IsDevelopment())
{
    // json endpoitini etkinleþtirir.
    app.UseSwagger();
    // api'nin internette test edilmesini saðlayan arayüzü saðlar.
    app.UseSwaggerUI();
}

// hhtps'e gelen requetleri otomatik olaran yönlendirmeye yarýyor.
app.UseHttpsRedirection();

// Yukarýdakiler artýk bir minimal api olarak çalýþtýðýný gösteriyor.

// burada havanýn durumlarýný belirtecek bir dizi tanýmlanmýþtýr.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Ardýndan bir get isteði almasý için endpoint tanýmlamasý yapýlmýþtýr.
app.MapGet("/weatherforecast", () =>
{
    // 5 günlük rastgele havadurumu listesi oluþturan bir kod var içerisinde.
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
// Endpointin adýna GetWeatherForecast adý veriliyor.
.WithName("GetWeatherForecast")
// Swager desteði tanýmlanýyor.
.WithOpenApi();

// web uygulamasý baþlatýlýyor.
app.Run();


// Burada tanýmlanan record yapýlanmasý End pointte tanýmlanacak bir model olarak tanýmlanýyor.
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Bu kod basit bir mini api bir hava durumu tahmin uygulamasý sunmaktadýr. Swager sayesinde API'ý kullanýcýya test edilip ve dökümantasyona ulaþabiliyoruz.
