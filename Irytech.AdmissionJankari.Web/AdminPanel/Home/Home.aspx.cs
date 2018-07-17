using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Home
{
    public partial class Home : SecurePage
    {
        DataTable dtMenuItems = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)return;
            if(new SecurePage().IsLoggedInUSer)
            {
                UserMenu(new SecurePage().LoggedInUserId.ToString());
            }
        }


        // Method to Bind The User Menu

        protected void UserMenu(string userId)
        {
            var objCommon = new Common();
            DataView view = null;
            try
            {
                dtMenuItems = objCommon.GetHeader(userId);
                view = new DataView(dtMenuItems) { RowFilter = "AjParentId IS NULL " };
                if (view.Count>0)
                {
                    ddlUserMenu.DataSource = view;
                    ddlUserMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing UserMenu in Home.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void ddlUserMenu_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptsubMenu = e.Item.FindControl("rptuserSubMenu") as Repeater;
                int currentItemID = int.Parse(this.ddlUserMenu.DataKeys[e.Item.ItemIndex].ToString());

                DataView view = GetSubMenu(currentItemID);
                if (view.Count > 0)
                {
                    rptsubMenu.DataSource = view;
                    rptsubMenu.DataBind();
                }
            }
        }

        // Method to  find the SUb menu of the menu
        private DataView GetSubMenu(int paraentId)
        {
            DataView view = null;
            try
            {
                view = new DataView(dtMenuItems) { RowFilter = "AjParentId=" + paraentId };
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing GetSubMenu in Home.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return view;
        }
        
    }
}