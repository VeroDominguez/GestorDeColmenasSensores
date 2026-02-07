using MockClasses.Sensores;
using MockClasses.Datos;
using MockClasses.DTOs;
using MockConsola.Servicios;

int intervaloSegundos = 10;
int intervaloEntreEnviosMs = 2000; // 2 segundos entre cada envío

Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
Console.WriteLine("║     Mock de Sensores de Colmena - Sistema de Apiarios         ║");
Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("Configuración de Sensores según Base de Datos:");
Console.WriteLine("- Colmena 6: Sensores 1, 2, 3");
Console.WriteLine("- Colmena 7: Sensores 4, 5, 6");
Console.WriteLine("- Colmena 8: Sensores 7, 8");
Console.WriteLine("- Colmena 9: Sensores 9, 10");
Console.WriteLine();
Console.WriteLine("¿Qué colmena deseas simular?");
Console.WriteLine("1 - Colmena 6 (Sensores 1-3)");
Console.WriteLine("2 - Colmena 7 (Sensores 4-6)");
Console.WriteLine("3 - Colmena 8 (Sensores 7-8)");
Console.WriteLine("4 - Colmena 9 (Sensores 9-10)");
Console.WriteLine("5 - Todas las colmenas (10 sensores)");
Console.Write("\nSelecciona una opción (1-5): ");

string? opcion = Console.ReadLine();

List<int> sensoresASimular = opcion switch
{
    "1" => new List<int> { 1, 2, 3 },       // Colmena 6
    "2" => new List<int> { 4, 5, 6 },       // Colmena 7
    "3" => new List<int> { 7, 8 },          // Colmena 8
    "4" => new List<int> { 9, 10 },         // Colmena 9
    "5" => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, // Todas
    _ => new List<int> { 1, 2, 3 }          // Default: Colmena 6
};

// Inicialización de sensores internos basados en la selección
var sensoresInternos = new List<SensorInterno>();

foreach (var sensorId in sensoresASimular)
{
    if (DatosPreCargados.TiposPorSensor.ContainsKey(sensorId))
    {
        sensoresInternos.Add(new SensorInterno
        {
            idSensor = sensorId,
            TipoSensor = DatosPreCargados.TiposPorSensor[sensorId]
        });
    }
}

// Inicialización sensor externo (compartido por todos)
var sensorExterno = new SensorExterno
{
    Id = 100 // ID genérico para el sensor externo
};

// Servicio de envío
var envioService = new EnvioDatosService();

// Índices para recorrer listas precargadas
int indiceInterno = 0;
int indiceExterno = 0;

Console.WriteLine();
Console.WriteLine($"Iniciando simulación con {sensoresInternos.Count} sensores...");
Console.WriteLine("Presiona Ctrl+C para detener");
Console.WriteLine("════════════════════════════════════════════════════════════════");
await Task.Delay(2000);

while (true)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║              DATOS ACTUALES DE LOS SENSORES                    ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
    Console.WriteLine();

    // Actualizar sensores internos (cada uno obtiene sus propias temperaturas)
    for (int i = 0; i < sensoresInternos.Count; i++)
    {
        var sensorId = sensoresInternos[i].idSensor;

        if (DatosPreCargados.TemperaturasPorSensorInterno.ContainsKey(sensorId))
        {
            var listaTemps = DatosPreCargados.TemperaturasPorSensorInterno[sensorId];
            var valores = listaTemps[indiceInterno % listaTemps.Count];

            sensoresInternos[i].TempInterna1 = valores.temp1;
            sensoresInternos[i].TempInterna2 = valores.temp2;
            sensoresInternos[i].TempInterna3 = valores.temp3;
        }
    }

    // Actualizar sensor externo
    var externo = DatosPreCargados.ExternaYPeso[indiceExterno % DatosPreCargados.ExternaYPeso.Count];
    sensorExterno.TempExterna = externo.temp;
    sensorExterno.Peso = externo.peso;

    // Mostrar valores por consola
    Console.WriteLine("┌─ SENSORES INTERNOS ────────────────────────────────────────────┐");
    for (int i = 0; i < sensoresInternos.Count; i++)
    {
        sensoresInternos[i].MostrarEnConsola();
        if (i < sensoresInternos.Count - 1)
        {
            Console.WriteLine("├────────────────────────────────────────────────────────────────┤");
        }
    }
    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
    Console.WriteLine();

    Console.WriteLine("┌─ SENSOR EXTERNO (COMPARTIDO) ─────────────────────────────────┐");
    sensorExterno.MostrarEnConsola();
    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");

    // Determinar qué sensor enviará datos externos
    // Por defecto, el primer sensor de peso en la lista, o el primero si no hay sensores de peso
    int sensorConDatosExternos = sensoresInternos
        .FirstOrDefault(s => s.TipoSensor == "PesoColmena")?.idSensor
        ?? sensoresInternos.First().idSensor;

    Console.WriteLine();
    Console.WriteLine("════════════════════════════════════════════════════════════════");
    Console.WriteLine($"📡 Enviando datos al backend...");
    Console.WriteLine($"   (Sensor {sensorConDatosExternos} incluirá datos externos)");
    Console.WriteLine("════════════════════════════════════════════════════════════════");

    int sensorActual = 0;
    int exitosos = 0;
    int fallidos = 0;

    foreach (var sensorInterno in sensoresInternos)
    {
        var registro = new DataArduinoDto
        {
            // Identifica el sensor interno que envía los datos
            idSensor = sensorInterno.idSensor,
            TipoSensor = sensorInterno.TipoSensor,

            // Datos del sensor interno - Temperaturas internas
            TempInterna1 = sensorInterno.TempInterna1,
            TempInterna2 = sensorInterno.TempInterna2,
            TempInterna3 = sensorInterno.TempInterna3,

            // Datos del sensor externo - Solo el sensor designado los envía
            TempExterna = sensorInterno.idSensor == sensorConDatosExternos
                ? sensorExterno.TempExterna
                : (float?)null,
            Peso = sensorInterno.idSensor == sensorConDatosExternos
                ? sensorExterno.Peso
                : (float?)null
        };

        try
        {
            // Enviar datos
            await envioService.EnviarAsync(registro);
            Console.WriteLine($"  ✓ Sensor {sensorInterno.idSensor} ({sensorInterno.TipoSensor}) enviado correctamente");
            exitosos++;
        }
        catch (Exception e)
        {
            Console.WriteLine($"  ✗ Error en Sensor {sensorInterno.idSensor}: {e.Message}");
            fallidos++;
        }

        // Esperar entre envíos para evitar problemas de concurrencia en el backend
        sensorActual++;
        if (sensorActual < sensoresInternos.Count)
        {
            await Task.Delay(intervaloEntreEnviosMs);
        }
    }

    Console.WriteLine("════════════════════════════════════════════════════════════════");
    Console.WriteLine($"📊 Resumen del envío:");
    Console.WriteLine($"   ✓ Exitosos: {exitosos}");
    if (fallidos > 0)
    {
        Console.WriteLine($"   ✗ Fallidos: {fallidos}");
    }
    Console.WriteLine($"   ⏱  Próximo envío en {intervaloSegundos} segundos");
    Console.WriteLine("════════════════════════════════════════════════════════════════");

    // Avanzar índices (cíclico)
    indiceInterno = (indiceInterno + 1) % DatosPreCargados.TemperaturasPorSensorInterno.Values.Max(list => list.Count);
    indiceExterno = (indiceExterno + 1) % DatosPreCargados.ExternaYPeso.Count;

    await Task.Delay(TimeSpan.FromSeconds(intervaloSegundos));
}