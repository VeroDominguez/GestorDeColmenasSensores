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
        private readonly HttpClient _httpClient;

        public EnvioDatosService()
        {
            _httpClient = new HttpClient();
        }

        public async Task EnviarAsync(DataArduinoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(
                "https://localhost:5001/api/registros", //aca va la url del endpoint para recibir los datos,      CAMBIAR antes de usar
                content
            );
        }
    }
}
