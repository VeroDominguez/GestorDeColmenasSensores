using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClasses.Datos
{
    public static class DatosPreCargados
    {
        // Temperaturas específicas por sensor interno (idSensor -> lista de tuplas de temperaturas)
        // Estos IDs coinciden con los sensores en la base de datos
        public static Dictionary<int, List<(float temp1, float temp2, float temp3)>> TemperaturasPorSensorInterno = new()
        {
            // COLMENA 6 - Sensores 1, 2, 3
            { 1, new List<(float, float, float)>  // Sensor 1 - TempColmena (Cuadro 1)
                {
                    (34.5f, 35.1f, 34.8f),
                    (34.3f, 34.9f, 34.6f),
                    (34.7f, 35.3f, 35.0f),
                    (34.4f, 35.0f, 34.7f),
                    (34.6f, 35.2f, 34.9f)
                }
            },
            { 2, new List<(float, float, float)>  // Sensor 2 - TempColmena (Cuadro 2)
                {
                    (33.9f, 34.2f, 34.0f),
                    (33.7f, 34.0f, 33.8f),
                    (34.1f, 34.4f, 34.2f),
                    (33.8f, 34.1f, 33.9f),
                    (34.0f, 34.3f, 34.1f)
                }
            },
            { 3, new List<(float, float, float)>  // Sensor 3 - PesoColmena (Cuadro 3)
                {
                    (35.2f, 35.5f, 35.3f),
                    (35.0f, 35.3f, 35.1f),
                    (35.4f, 35.7f, 35.5f),
                    (35.1f, 35.4f, 35.2f),
                    (35.3f, 35.6f, 35.4f)
                }
            },

            // COLMENA 7 - Sensores 4, 5, 6
            { 4, new List<(float, float, float)>  // Sensor 4 - TempColmena (Cuadro 4)
                {
                    (33.5f, 34.0f, 33.8f),
                    (33.3f, 33.8f, 33.6f),
                    (33.7f, 34.2f, 34.0f),
                    (33.4f, 33.9f, 33.7f),
                    (33.6f, 34.1f, 33.9f)
                }
            },
            { 5, new List<(float, float, float)>  // Sensor 5 - TempColmena (Cuadro 5)
                {
                    (34.1f, 34.4f, 34.2f),
                    (33.9f, 34.2f, 34.0f),
                    (34.3f, 34.6f, 34.4f),
                    (34.0f, 34.3f, 34.1f),
                    (34.2f, 34.5f, 34.3f)
                }
            },
            { 6, new List<(float, float, float)>  // Sensor 6 - PesoColmena (Cuadro 6)
                {
                    (34.8f, 35.0f, 34.9f),
                    (34.6f, 34.8f, 34.7f),
                    (35.0f, 35.2f, 35.1f),
                    (34.7f, 34.9f, 34.8f),
                    (34.9f, 35.1f, 35.0f)
                }
            },

            // COLMENA 8 - Sensores 7, 8
            { 7, new List<(float, float, float)>  // Sensor 7 - TempColmena (Cuadro 7)
                {
                    (32.5f, 33.0f, 32.8f),
                    (32.3f, 32.8f, 32.6f),
                    (32.7f, 33.2f, 33.0f),
                    (32.4f, 32.9f, 32.7f),
                    (32.6f, 33.1f, 32.9f)
                }
            },
            { 8, new List<(float, float, float)>  // Sensor 8 - PesoColmena (Cuadro 8)
                {
                    (33.2f, 33.6f, 33.4f),
                    (33.0f, 33.4f, 33.2f),
                    (33.4f, 33.8f, 33.6f),
                    (33.1f, 33.5f, 33.3f),
                    (33.3f, 33.7f, 33.5f)
                }
            },

            // COLMENA 9 - Sensores 9, 10
            { 9, new List<(float, float, float)>  // Sensor 9 - TempColmena (Cuadro 9)
                {
                    (36.0f, 36.5f, 36.2f),
                    (35.8f, 36.3f, 36.0f),
                    (36.2f, 36.7f, 36.4f),
                    (35.9f, 36.4f, 36.1f),
                    (36.1f, 36.6f, 36.3f)
                }
            },
            { 10, new List<(float, float, float)> // Sensor 10 - PesoColmena (Cuadro 10)
                {
                    (35.5f, 35.9f, 35.7f),
                    (35.3f, 35.7f, 35.5f),
                    (35.7f, 36.1f, 35.9f),
                    (35.4f, 35.8f, 35.6f),
                    (35.6f, 36.0f, 35.8f)
                }
            }
        };

        // Datos externos (temperatura externa y peso) que se combinarán con los sensores
        // Estos valores son para toda la colmena, no por sensor individual
        public static List<(float temp, float peso)> ExternaYPeso = new()
        {
            (28.5f, 42.3f),   // Ambiente cálido, peso normal
            (27.8f, 39.1f),   // Ambiente templado, peso más bajo
            (29.0f, 45.0f),   // Ambiente más cálido, peso alto
            (26.5f, 38.2f),   // Ambiente fresco, peso bajo
            (27.0f, 38.9f),   // Ambiente templado, peso medio-bajo
            (28.2f, 39.8f),   // Ambiente templado-cálido, peso medio
            (28.7f, 44.5f),   // Ambiente cálido, peso alto
            (30.0f, 42.0f)    // Ambiente muy cálido, peso normal
        };

        // Definición de tipos de sensores según la base de datos
        public static Dictionary<int, string> TiposPorSensor = new()
        {
            { 1, "TempColmena" },   // Colmena 6
            { 2, "TempColmena" },
            { 3, "PesoColmena" },
            { 4, "TempColmena" },   // Colmena 7
            { 5, "TempColmena" },
            { 6, "PesoColmena" },
            { 7, "TempColmena" },   // Colmena 8
            { 8, "PesoColmena" },
            { 9, "TempColmena" },   // Colmena 9
            { 10, "PesoColmena" }
        };

        // DATOS LEGACY - Mantenidos por compatibilidad, pero no se usan en el nuevo código
        public static List<(float, float, float)> TemperaturasInternas = new()
        {
            (32.1f, 32.4f, 32.8f),
            (33.0f, 33.2f, 33.5f),
            (34.0f, 34.2f, 34.4f),
            (35.0f, 35.1f, 35.3f)
        };

        public static List<(int idSensor, string tipoSensor)> SensoresInterno = new()
        {
            (1, "TempColmena"),
            (2, "TempColmena"),
            (3, "PesoColmena"),
            (4, "TempColmena"),
            (5, "TempColmena"),
            (6, "PesoColmena"),
            (7, "TempColmena"),
            (8, "PesoColmena"),
            (9, "TempColmena"),
            (10, "PesoColmena")
        };

        public static List<(int idSensor, string tipoSensor)> SensoresExterno = new()
        {
            (11, "TempExterna"),
            (12, "PesoExterno")
        };
    }
}