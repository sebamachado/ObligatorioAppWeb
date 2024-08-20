using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using program;
using sharedEntities;


public partial class home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Obtén la hora del último despliegue
        string lastDeployTime = ParametersActions.GetParameter("ultimoDeploy");
        // Actualiza el texto del Label con la hora del último despliegue
        lblLastDeploy.Text = "Último despliegue: " + lastDeployTime;

    }
} 