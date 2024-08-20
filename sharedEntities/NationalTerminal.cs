using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharedEntities
{
    public class NationalTerminal: Terminal
    {
        //atributos
        private bool taxiService;

        //propiedades
        public bool TaxiService
        {
            get { return taxiService; }
            set { taxiService = value; }
        }
        public string TaxiServiceText
        {
            get { return TaxiService ? "SI" : "NO"; }
        }

        //constructor (completo)
        public NationalTerminal (string id, string cityName, bool taxiService):base(id,cityName) {
            TaxiService = taxiService;
        }

        //metodo toString
        public override string ToString()
        {
            string hasTaxiService = "NO";
            if (TaxiService)
                hasTaxiService = "SI";
            //return base.ToString()+"\t";
            return "Codigo: " + Id +"\nCiudad: "+CityName+"\nServicio de Taxi: "+hasTaxiService;;
        }
    }
}
