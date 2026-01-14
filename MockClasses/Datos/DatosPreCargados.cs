using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClasses.Datos
{
    public static class DatosPreCargados
    {
        //aqui se cargan los valores de todos los posibles casos de prueba
        public static List<(float, float, float)> TemperaturasInternas = new()
        {
            (32.1f, 32.4f, 32.8f),
            (33.0f, 33.2f, 33.5f),
            (34.0f, 34.2f, 34.4f),
            (35.0f, 35.1f, 35.3f)
        };

        public static List<(float temp, float peso)> ExternaYPeso = new()
        {
            (18.5f, 42.0f),
            (19.0f, 42.4f),
            (20.2f, 43.0f),
            (21.0f, 43.8f)
        };

        public static List<(int idSensor, string tipoSensor)> SensoresInterno = new()
        {
            (1, "TempColmena"),
            (2, "PesoColmena"),
            (3, "TempColmena"),
            (4, "TempColmena"),
            (5, "TempColmena")
        };

        public static List<(int idSensor, string tipoSensor)> SensoresExterno = new()
        {
            (6, "TempCuadro"),
            (7, "TempCuadro"),
            (8, "TempCuadro"),
            (9, "TempCuadro")
        };

        // Temperaturas específicas por sensor interno (idSensor -> lista de tuplas de temperaturas)
        public static Dictionary<int, List<(float temp1, float temp2, float temp3)>> TemperaturasPorSensorInterno = new()
        {
            { 1, new List<(float, float, float)>
                {
                    (32.1f, 32.4f, 32.8f),
                    (33.0f, 33.2f, 33.5f),
                    (34.0f, 34.2f, 34.4f),
                    (35.0f, 35.1f, 35.3f)
                }
            },
            { 2, new List<(float, float, float)>
                {
                    (31.5f, 31.8f, 32.2f),
                    (32.5f, 32.8f, 33.1f),
                    (33.5f, 33.8f, 34.1f),
                    (34.5f, 34.8f, 35.1f)
                }
            },
            { 3, new List<(float, float, float)>
                {
                    (33.5f, 33.8f, 34.2f),
                    (34.5f, 34.8f, 35.1f),
                    (35.5f, 35.8f, 36.1f),
                    (36.5f, 36.8f, 37.1f)
                }
            },
            { 4, new List<(float, float, float)>
                {
                    (30.0f, 30.3f, 30.7f),
                    (31.0f, 31.3f, 31.7f),
                    (32.0f, 32.3f, 32.7f),
                    (33.0f, 33.3f, 33.7f)
                }
            },
            { 5, new List<(float, float, float)>
                {
                    (36.0f, 36.3f, 36.7f),
                    (37.0f, 37.3f, 37.7f),
                    (38.0f, 38.3f, 38.7f),
                    (39.0f, 39.3f, 39.7f)
                }
            }
        };
    }
}
