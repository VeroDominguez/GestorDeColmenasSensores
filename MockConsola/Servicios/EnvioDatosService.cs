using MockClasses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MockConsola.Servicios
{
    public class EnvioDatosService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task EnviarAsync(DataArduinoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(
                "http://localhost:5083/MedicionSensores",
                //"https://gestordeapiarios-b7dhf6e4amdfh3e0.centralus-01.azurewebsites.net/MedicionSensores",
                content
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
