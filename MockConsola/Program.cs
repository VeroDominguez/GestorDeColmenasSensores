using MockClasses.Sensores;
using MockClasses.Datos;
using MockClasses.DTOs;
using MockConsola.Servicios;

int intervaloSegundos = 300;

// Inicialización de sensores internos (placas internas)
var sensoresInternos = new List<SensorInterno>
{
    new SensorInterno
    {
        Id = 1,
        TipoSensor = "TempColmena"
    },
    new SensorInterno
    {
        Id = 2,
        TipoSensor = "PesoColmena"
    },
    new SensorInterno
    {
        Id = 3,
        TipoSensor = "TempColmena"
    }
};

// Inicialización sensor externo
var sensorExterno = new SensorExterno
{
    Id = 10
};

// Servicio de envío
var envioService = new EnvioDatosService();

// Índices para recorrer listas precargadas
int indiceInterno = 0;
int indiceExterno = 0;

Console.WriteLine("Mock de sensores de colmena iniciado...");
Console.WriteLine("----------------------------------------");

while (true)
{
    Console.Clear();

    // Actualizar sensores internos (toman el mismo set de valores)
    for (int i = 0; i < sensoresInternos.Count; i++)
    {
        var valores = DatosPreCargados.TemperaturasInternas[indiceInterno];

        sensoresInternos[i].TempInterna1 = valores.Item1;
        sensoresInternos[i].TempInterna2 = valores.Item2;
        sensoresInternos[i].TempInterna3 = valores.Item3;
    }

    // Actualizar sensor externo
    var externo = DatosPreCargados.ExternaYPeso[indiceExterno];
    sensorExterno.TempExterna = externo.temp;
    sensorExterno.Peso = externo.peso;

    // Mostrar valores por consola
    for (int i = 0; i < sensoresInternos.Count; i++)
    {
        sensoresInternos[i].MostrarEnConsola();
    }

    sensorExterno.MostrarEnConsola();

    // Armar DTO (se envía una placa interna + externo)--- esto hay que modificarlo dado que ahora hay 3 placas internas + 1 externa
    var registro = new DataArduinoDto
    {
        idSensor = sensoresInternos[0].Id,
        TipoSensor = sensoresInternos[0].TipoSensor,
        TempInterna1 = sensoresInternos[0].TempInterna1,
        TempInterna2 = sensoresInternos[0].TempInterna2,
        TempInterna3 = sensoresInternos[0].TempInterna3,
        TempExterna = sensorExterno.TempExterna,
        Peso = sensorExterno.Peso
    };

    // Enviar datos
    await envioService.EnviarAsync(registro);

    Console.WriteLine();
    Console.WriteLine("Datos enviados al backend");
    Console.WriteLine($"Próximo envío en {intervaloSegundos} segundos");

    // Avanzar índices (cíclico)
    indiceInterno = (indiceInterno + 1) % DatosPreCargados.TemperaturasInternas.Count;
    indiceExterno = (indiceExterno + 1) % DatosPreCargados.ExternaYPeso.Count;

    await Task.Delay(TimeSpan.FromSeconds(intervaloSegundos));
}
