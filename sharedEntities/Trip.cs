using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharedEntities
{
    public class Trip
    {
        //atributos
        private static int lastId = 0; // Variable estática para mantener el último valor asignado
        private int id;
        private DateTime departureDate;
        private DateTime estimatedArrivalDate;
        private int maxPassengers;
        private Double ticketPrice;
        private int platformNumber;
        private Terminal arrivalTerminal;
        private Company companyTrip;

        //propiedades
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        public DateTime DepartureDate
        {
            get { return departureDate; }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new Exception("La fecha de salida no puede ser anterior a la fecha actual");
                }
                departureDate = value;
            }
        }
        public DateTime EstimatedArrivalDate
        {
            get { return estimatedArrivalDate; }
            set
            {
                if (value < DepartureDate)
                {
                    throw new Exception("La fecha de llegada no puede ser anterior a la fecha de salida");
                }
                estimatedArrivalDate = value;
            }
        }
        public int MaxPassengers
        {
            get { return maxPassengers; }
            set
            {
                if (value > 50)
                {
                    throw new Exception("EL maximo de pasajeros es 50");
                }
                maxPassengers = value;
            }
        }
        public Double TicketPrice
        {
            get { return ticketPrice; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("El precio no puede ser $0");
                }
                ticketPrice = value;
            }
        }
        public int PlatformNumber
        {
            get { return platformNumber; }
            set
            {
                if (value >35 || value<1)
                {
                    throw new Exception("Los andenes deben ser del 1 al 35");
                }
                platformNumber = value;
            }
        }
        public Terminal ArrivalTerminal
        {
            get { return arrivalTerminal; }
            set
            { arrivalTerminal = value; }
        }
        public Company CompanyTrip
        {
            get { return companyTrip; }
            set{companyTrip = value;}
        }

        //constructor (completo)
        public Trip(DateTime departureDate, DateTime estimatedArrivalDate, int maxPassengers, double ticketPrice, int platformNumber, Terminal arrivalTerminal, Company companyTrip)
        {
            id = ++lastId;
            DepartureDate = departureDate;
            EstimatedArrivalDate = estimatedArrivalDate;
            MaxPassengers = maxPassengers;
            TicketPrice = ticketPrice;
            PlatformNumber = platformNumber;
            ArrivalTerminal = arrivalTerminal;
            CompanyTrip = companyTrip;
        }
        
        //metodo toString
        public override string ToString()
        {
            return "Id: " + Id + "\nFecha y hora de salida: " + departureDate + "\nFecha y hora aproximada de llegada: " + departureDate + "\nPasajeros: " + maxPassengers + "\nPrecio pasaje: " + ticketPrice + "\nNro anden: " + platformNumber + "\nTerminal destino: " + arrivalTerminal.CityName + "\nCompania: " + companyTrip.Name;
        }
    }

}
