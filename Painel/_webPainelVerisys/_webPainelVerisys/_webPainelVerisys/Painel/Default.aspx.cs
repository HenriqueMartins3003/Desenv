﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _webPainelVerisys.Painel
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNomeAplicacao.Text = ConfigurationManager.AppSettings["NameApplication"].ToString();
            lblNomeAplicacaoTitulo.Text = ConfigurationManager.AppSettings["NameApplication"].ToString();
        }
    }
}
