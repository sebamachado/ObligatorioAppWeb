using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharedEntities
{
    public class InternationalTerminal : Terminal
    {
        //atributos
        private string country;

        //propiedades
        public string Country
        {
            get { return country; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("El nombre del pais no puede ser vacio");
                }
                country = value;
            }
        }

        //constructor (completo)
        public InternationalTerminal(string id, string cityName, string country): base(id, cityName)
        {
            Country = country;
        }

        //metodo toString
        public override string ToString()
        {
            return base.ToString() + "\t";
        }
    }
}
