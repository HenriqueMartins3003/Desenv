﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

using _webPainelVerisys.BLL;
using _webPainelVerisys.DTO;

namespace _webPainelVerisys.Mailing
{
    public partial class SegmentacaoHistorico : System.Web.UI.Page
    {
        dtoUsers objUsers = null;
        UsersProfiles objUsersProfiles = new UsersProfiles();
        Campaigns objCampaign = new Campaigns();
        Mailings objMailings = new Mailings();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ObjUsers"] != null)
            {
                objUsers = (dtoUsers)Session["ObjUsers"];
            }
            else
            {
                Response.Redirect("../login.aspx");
            }

            if (!IsPostBack)
            {
                AccessSecurity();
                PageConfig();
                loadCampaign();
            }
        }

        private void AccessSecurity()
        {
            if (objUsersProfiles.AccessProfile(this.Form.ID, dtoUsersProfiles.AccessType.CONSULTA, objUsers.IdProfile) == 0)
            {
                Response.Redirect("../Painel/Default.aspx");
            }

            objUsersProfiles.AcessProfileButton(buttonImageFilter, "~/img/btn-filtrar.gif", this.Form.ID, dtoUsersProfiles.AccessType.CONSULTA, Convert.ToInt32(objUsers.IdProfile));
        }

        private void PageConfig()
        {
            dtoPageConfig objdtoPageConfig = null;
            bllPageConfig objPageConfig = new bllPageConfig();

            String strApplication = Session["ObjApplication"].ToString();
            objdtoPageConfig = objPageConfig.listPageConfig(this.Form.ID, strApplication);

            lblTitulo.Text = objdtoPageConfig.Descricao;
        }

        protected void loadCampaign()
        {
            ddlCampaign.DataSource = objCampaign.listCampaignAssociated(objUsers);
            ddlCampaign.DataTextField = "NomeCampanha";
            ddlCampaign.DataValueField = "Campaign";
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Selecione a campanha...", "0"));
        }

        public void loadMailingHandler()
        {
            if (ddlCampaign.SelectedValue == "0")
            {
                lblResponse.Text = "<div class='RNOK'> A <b>Campanha</b> deve ser selecionada !! </div>";
                divResponse.Visible = true;
                return;
            }

            dtoMailing objAuxMailing = new dtoMailing();
            objAuxMailing.Campaign = ddlCampaign.SelectedValue.ToString();

            /* processing period */
            switch (ddlPeriodo.SelectedValue)
            {
                case "1":
                    objAuxMailing.DateStart = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 00:00:00");
                    objAuxMailing.DateFinish = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    break;

                case "2":
                    objAuxMailing.DateStart = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00");
                    objAuxMailing.DateFinish = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    break;

                case "3":
                    objAuxMailing.DateStart = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd 00:00:00");
                    objAuxMailing.DateFinish = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    break;

                case "4":
                    objAuxMailing.DateStart = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd 00:00:00");
                    objAuxMailing.DateFinish = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    break;

                case "5":
                    objAuxMailing.DateStart = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd 00:00:00");
                    objAuxMailing.DateFinish = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                    break;
            }

            gridListaHistorico(objAuxMailing);
        }

        protected void gridListaHistorico(dtoMailing objAux)
        {
            gdRegistros.AutoGenerateColumns = false;
            gdRegistros.Columns.Clear();

            BoundField bfCampaign = new BoundField();
            bfCampaign.DataField = "Campaign";
            bfCampaign.HeaderText = "Canpanha";
            bfCampaign.HeaderStyle.CssClass = "gridViewHeader1";
            bfCampaign.ItemStyle.CssClass = "gridViewValues3";
            bfCampaign.ItemStyle.Width = 50;
            bfCampaign.SortExpression = "Campaign";
            gdRegistros.Columns.Add(bfCampaign);

            BoundField bfLot = new BoundField();
            bfLot.DataField = "Lot";
            bfLot.HeaderText = "Lote";
            bfLot.HeaderStyle.CssClass = "gridViewHeader1";
            bfLot.ItemStyle.CssClass = "gridViewValues3";
            bfLot.ItemStyle.Width = 50;
            bfLot.SortExpression = "Lot";
            gdRegistros.Columns.Add(bfLot);

            BoundField bfIDSegmentacao = new BoundField();
            bfIDSegmentacao.DataField = "IDSegmentacao";
            bfIDSegmentacao.HeaderText = "Segmentação";
            bfIDSegmentacao.HeaderStyle.CssClass = "gridViewHeader1";
            bfIDSegmentacao.ItemStyle.CssClass = "gridViewValues3";
            bfIDSegmentacao.ItemStyle.Width = 50;
            bfIDSegmentacao.SortExpression = "IDSegmentacao";
            gdRegistros.Columns.Add(bfIDSegmentacao);

            BoundField bfRecords = new BoundField();
            bfRecords.DataField = "Records";
            bfRecords.HeaderText = "Registros";
            bfRecords.HeaderStyle.CssClass = "gridViewHeader1";
            bfRecords.ItemStyle.CssClass = "gridViewValues3";
            bfRecords.ItemStyle.Width = 50;
            bfRecords.SortExpression = "Records";
            gdRegistros.Columns.Add(bfRecords);

            BoundField bfDateTimeMH = new BoundField();
            bfDateTimeMH.DataField = "DateTimeMH";
            bfDateTimeMH.HeaderText = "Data / Hora";
            bfDateTimeMH.HeaderStyle.CssClass = "gridViewHeader1";
            bfDateTimeMH.ItemStyle.CssClass = "gridViewValues3";
            bfDateTimeMH.ItemStyle.Width = 100;
            bfDateTimeMH.SortExpression = "DateTimeMH";
            gdRegistros.Columns.Add(bfDateTimeMH);

            BoundField bfUser = new BoundField();
            bfUser.DataField = "User";
            bfUser.HeaderText = "Usuario";
            bfUser.HeaderStyle.CssClass = "gridViewHeader1";
            bfUser.ItemStyle.CssClass = "gridViewValues3";
            bfUser.ItemStyle.Width = 100;
            bfUser.SortExpression = "User";
            gdRegistros.Columns.Add(bfUser);

            gdRegistros.PagerStyle.HorizontalAlign = HorizontalAlign.Center;
            gdRegistros.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#E1E1E1");
            gdRegistros.FooterStyle.CssClass = "gridViewHeader";

            //gdRegistros.DataSource = objMailings.listSegmentacaoLog(objAux);
            //gdRegistros.DataBind();

            List<dtoMailing> AuxDto = objMailings.listSegmentacaoLog(objAux);
            if (AuxDto != null)
            {
                divResponse.Visible = false;
                gdRegistros.DataSource = AuxDto;
                gdRegistros.DataBind();
            }
            else
            {
                lblResponse.Text = "<div class='RNOK'>Essa Campanha não possui modulo de Segmentação !! </div>";
                divResponse.Visible = true;
                return;
            }
        }

        protected void gdRegistros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Color = "#E8F3FF";

                if (((e.Row.RowIndex + 1) % 2) == 0)
                {
                    e.Row.CssClass = "gridViewValues1";
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(Color);
                }
            }
        }

        protected void buttonImageFilter_Click(object sender, ImageClickEventArgs e)
        {
            loadMailingHandler();
        }
    }
}
