// asp.net.core uygulamas�n�n yap�land�rmak i�in bir builder tan�mland�.
var builder = WebApplication.CreateBuilder(args);


// api uygulamalar�n� otomatik olarak tan�mlas�n� ve geli�tirilmesini sa�lar.
builder.Services.AddEndpointsApiExplorer();
//swager d�k�mantasyonu olu�turmak i�in eklenir..
builder.Services.AddSwaggerGen();

// web application nesnesi olu�turuyoruz.
var app = builder.Build();

// uygulamalar e�er ki geli�tirme ortam�nda �al��t�r�l�rsa etkinle�tirilecek middleware'leri g�steriyor.
if (app.Environment.IsDevelopment())
{
    // json endpoitini etkinle�tirir.
    app.UseSwagger();
    // api'nin internette test edilmesini sa�layan aray�z� sa�lar.
    app.UseSwaggerUI();
}

// hhtps'e gelen requetleri otomatik olaran y�nlendirmeye yar�yor.
app.UseHttpsRedirection();

// Yukar�dakiler art�k bir minimal api olarak �al��t���n� g�steriyor.

// burada havan�n durumlar�n� belirtecek bir dizi tan�mlanm��t�r.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Ard�ndan bir get iste�i almas� i�in endpoint tan�mlamas� yap�lm��t�r.
app.MapGet("/weatherforecast", () =>
{
    // 5 g�nl�k rastgele havadurumu listesi olu�turan bir kod var i�erisinde.
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
// Endpointin ad�na GetWeatherForecast ad� veriliyor.
.WithName("GetWeatherForecast")
// Swager deste�i tan�mlan�yor.
.WithOpenApi();

// web uygulamas� ba�lat�l�yor.
app.Run();


// Burada tan�mlanan record yap�lanmas� End pointte tan�mlanacak bir model olarak tan�mlan�yor.
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Bu kod basit bir mini api bir hava durumu tahmin uygulamas� sunmaktad�r. Swager sayesinde API'� kullan�c�ya test edilip ve d�k�mantasyona ula�abiliyoruz.
