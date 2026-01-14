using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClasses.DTOs
{
    public class DataArduinoDto
    {
        public int idSensor { get; set; }
        public string TipoSensor { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public float? TempExterna { get; set; }// se agrega ? para que pueda ser nulo
        public float? Peso { get; set; }
    }
}
