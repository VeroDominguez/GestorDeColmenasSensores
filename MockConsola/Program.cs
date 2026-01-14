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
        idSensor = 1,
        TipoSensor = "TempColmena"
    },
    new SensorInterno
    {
        idSensor = 2,
        TipoSensor = "PesoColmena"
    },
    new SensorInterno
    {
        idSensor = 3,
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
    // Actualizar sensores internos (cada uno obtiene sus propias temperaturas)
    for (int i = 0; i < sensoresInternos.Count; i++)
    {
        var sensorId = sensoresInternos[i].idSensor;

        if (DatosPreCargados.TemperaturasPorSensorInterno.ContainsKey(sensorId))
        {
            var valores = DatosPreCargados.TemperaturasPorSensorInterno[sensorId][indiceInterno];

            sensoresInternos[i].TempInterna1 = valores.temp1;
            sensoresInternos[i].TempInterna2 = valores.temp2;
            sensoresInternos[i].TempInterna3 = valores.temp3;
        }
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

    // Armar DTOs (se envían 3 JSON a la vez, uno por cada sensor interno)
    const int SENSOR_INTERNO_A_ENVIAR = 1; // Enviar siempre el primero (puede cambiarse para enviar otro)
    
    foreach (var sensorInterno in sensoresInternos)
    {
        var registro = new DataArduinoDto
        {
            //Identifica el sensor interno que envía los datos
            idSensor = sensorInterno.idSensor,
            TipoSensor = sensorInterno.TipoSensor,

            //Datos del sensor interno- Temperaturas internas
            TempInterna1 = sensorInterno.TempInterna1,
            TempInterna2 = sensorInterno.TempInterna2,
            TempInterna3 = sensorInterno.TempInterna3,

            //Datos del sensor externo- Temperatura externa y peso
            TempExterna = sensorInterno.idSensor == SENSOR_INTERNO_A_ENVIAR
                ? sensorExterno.TempExterna
                : (float?)null,
            Peso = sensorInterno.idSensor == SENSOR_INTERNO_A_ENVIAR
                ? sensorExterno.Peso
                : (float?)null
        };

        try
        {
            // Enviar datos
            await envioService.EnviarAsync(registro);
        }
        catch( Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
    

    Console.WriteLine();
    Console.WriteLine("Datos enviados al backend");
    Console.WriteLine($"Próximo envío en {intervaloSegundos} segundos");

    // Avanzar índices (cíclico)
    indiceInterno = (indiceInterno + 1) % DatosPreCargados.TemperaturasInternas.Count;
    indiceExterno = (indiceExterno + 1) % DatosPreCargados.ExternaYPeso.Count;

    await Task.Delay(TimeSpan.FromSeconds(intervaloSegundos));
}
