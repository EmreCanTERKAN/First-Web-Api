// asp.net.core uygulamasının yapılandırmak için bir builder tanımlandı.
var builder = WebApplication.CreateBuilder(args);


// api uygulamalarını otomatik olarak tanımlasını ve geliştirilmesini sağlar.
builder.Services.AddEndpointsApiExplorer();
//swager dökümantasyonu oluşturmak için eklenir..
builder.Services.AddSwaggerGen();

// web application nesnesi oluşturuyoruz.
var app = builder.Build();

// uygulamalar eğer ki geliştirme ortamında çalıştırılırsa etkinleştirilecek middleware'leri gösteriyor.
if (app.Environment.IsDevelopment())
{
    // json endpoitini etkinleştirir.
    app.UseSwagger();
    // api'nin internette test edilmesini sağlayan arayüzü sağlar.
    app.UseSwaggerUI();
}

// hhtps'e gelen requetleri otomatik olaran yönlendirmeye yarıyor.
app.UseHttpsRedirection();

// Yukarıdakiler artık bir minimal api olarak çalıştığını gösteriyor.

// burada havanın durumlarını belirtecek bir dizi tanımlanmıştır.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Ardından bir get isteği alması için endpoint tanımlaması yapılmıştır.
app.MapGet("/weatherforecast", () =>
{
    // 5 günlük rastgele havadurumu listesi oluşturan bir kod var içerisinde.
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
// Endpointin adına GetWeatherForecast adı veriliyor.
.WithName("GetWeatherForecast")
// Swager desteği tanımlanıyor.
.WithOpenApi();

// web uygulaması başlatılıyor.
app.Run();


// Burada tanımlanan record yapılanması End pointte tanımlanacak bir model olarak tanımlanıyor.
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Bu kod basit bir mini api bir hava durumu tahmin uygulaması sunmaktadır. Swager sayesinde API'ı kullanıcıya test edilip ve dökümantasyona ulaşabiliyoruz.
