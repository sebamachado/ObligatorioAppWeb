using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sharedEntities;
using dataPersistence;

namespace program
{
    public class ParametersActions
    {
        public static string GetParameter(string pName)
        {
            string pValue=ParametersPersistence.GetParameter(pName);
            return pValue;
        }

    }
}
