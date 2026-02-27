using System;

namespace Automotores.Kiosco.Servicios;

public class DatabookService
{


    private readonly HttpClient _http;

    public DatabookService (HttpClient http)
    {
        _http = http;
    }

    public async Task<object?> ObtenerDatosAsync()
    {
        var response = await _http.GetAsync("https://rickandmortyapi.com/api/character");

        if(!response.IsSuccessStatusCode)
            return null;
        var data = response.Content.ReadFromJsonAsync<object>();
        return data;
    }

}
