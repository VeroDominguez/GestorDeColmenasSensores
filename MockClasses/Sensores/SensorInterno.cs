using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClasses.Sensores
{
    public class SensorInterno
    {
        public int idSensor { get; set; }
        public string TipoSensor { get; set; } = string.Empty; //esto es para que igual se inicialice como cadena vacía y no nulo, se lo peude sacar en caso de que sea requisito que sea nulo
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }

        public void MostrarEnConsola()
        {
            Console.WriteLine($"Sensor Interno ID:{idSensor}");
            Console.WriteLine($"Tipo de Sensor: {TipoSensor}");
            Console.WriteLine($"Temperatura Interna 1: {TempInterna1}");
            Console.WriteLine($"Temperatura Interna 2: {TempInterna2}");
            Console.WriteLine($"Temperatura Interna 3: {TempInterna3}");
        }
    }
}
