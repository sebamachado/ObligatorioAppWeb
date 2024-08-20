using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharedEntities
{
    public class Company
    {

        //atributos
        private string name;
        private string matrizAddress;
        private string contactPhone;

        //propiedades
        public string Name
            {
                get { return name; }
                set{
                    if (value == string.Empty) {
                        throw new Exception("El nombre no puede ser vacio");}
                      name = value; }
            }
        public string MatrizAddress
           {
            get { return matrizAddress; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("La direccion no puede ser vacia");
                }
                matrizAddress = value;
            }
            }
        public string ContactPhone
        {
            get { return contactPhone; }
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("El numero de telefono no puede ser vacio");
                }
                contactPhone = value;
            }
        }

        //constructor (completo)
        public Company(string name, string matrizAddress, string contactPhone)
        {
            Name = name;
            MatrizAddress = matrizAddress;
            ContactPhone = contactPhone;
        }

        //metodo toString
        public override string ToString()
        {
            return "\nNombre: " + Name + "\nDireccion Matriz: : " + MatrizAddress + "\nTelefono de Contacto: " + ContactPhone;
        }
        //metodo getName
        public string GetName()
        {
            return Name;
        }
    }
}
