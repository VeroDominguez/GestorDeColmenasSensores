using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClasses.Sensores
{
    public class SensorExterno
    {
        public int Id { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }

        public void MostrarEnConsola()
        {
            Console.WriteLine($"Sensor Externo ID:{Id}");
            Console.WriteLine($"Temperatura Externa: {TempExterna}°C");
            Console.WriteLine($"Peso: {Peso}kg");
        }
    }
}
