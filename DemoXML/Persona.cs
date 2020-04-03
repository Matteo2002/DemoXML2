using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXML
{
    public class Persona
    {
        public string Nome { get; set; }

        public string Cognome { get; set; }

        public int Anni { get; set; }

        public string DataDiNascita { get; set; }

        public override string ToString()
        {
            return $"{Nome}";
        }
    }
}
