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
    }
}
