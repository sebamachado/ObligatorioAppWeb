using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sharedEntities;
using dataPersistence;

namespace program 
{
    public class CompanyAction
    {
        public static void Create(Company pCompany)
        {
            CompanyPersistence.CreateCompany(pCompany); 
        }

        public static void Update(Company pCompany)
        {
            CompanyPersistence.UpdateCompany(pCompany);
        }

        public static void Delete(Company pCompany)
        {
            CompanyPersistence.DeleteCompany(pCompany);
        }

        public static Company Read(string pNameComp)
        {
            return CompanyPersistence.findCompany(pNameComp);
        }
        public static List<Company> ListCompanies()
        {
            return CompanyPersistence.ListAllCompanies();

        }
    }
}
