using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharedEntities
{
    public abstract class Terminal
    {
        //atributos
        private string id;
        private string cityName;

        //propiedades
        public string Id
        {
            get { return id; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("El codigo no puede ser vacio");
                }
                if (value.Length != 6) {
                    throw new Exception("El codigo debe de contener 6 caracteres");
                }
                id = value;
            }
        }
        public string CityName
        {
            get { return cityName; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("El nombre de la ciudad no puede ser vacio");
                }
                cityName = value;
            }
        }

        //constructor (completo)
        public Terminal(string id, string cityName)
        {
            Id = id;
            CityName = cityName;
        }

        //metodo toString
        public override string ToString()
        {
            //return base.ToString()+"\t";
            return "Codigo: " + Id +"\nCiudad: "+CityName;
        }
    }


}
