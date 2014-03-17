using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using DataAccess;

namespace nTier_Chapman
{
    public partial class NtierDemo : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["nTierChapmanConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            daSQLapp objDemo = new daSQLapp();

            DataTable dtJobList = objDemo.GetJobList(connectionString);

            gvJobList.DataSource = dtJobList;
            gvJobList.DataBind();

            ddlJobList.DataSource = dtJobList;
            ddlJobList.DataValueField = "JobID";
            ddlJobList.DataTextField = "JobLocation";

            ddlJobList.DataBind();

            lbJobList.DataSource = dtJobList;
            lbJobList.DataValueField = "JobID";
            lbJobList.DataTextField = "JobLocation";
            lbJobList.DataBind();
                   
        }
    }
}