
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Controls
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        private SecurePage _objSecurePage = new SecurePage();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            if (_objSecurePage.IsLoggedInUSer)
                lblUserName.Text = _objSecurePage.LoggedInUserName;
            DataTable menuData = null;
            try
            {
                menuData = new DataTable();
                menuData = GetMenuData();
                AddTopMenuItems(menuData);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                menuData = null;
            }
        }

        #region Function for getting data for menu

        /// <summary>
        /// Get's the data from database for menu
        /// </summary>
        /// <returns>Datatable of menu items</returns>
        private DataTable GetMenuData()
        {
            if (new SecurePage().IsLoggedInUSer)
            {
                var objCommon = new Common();
                try
                {
                    var dtMenuItems = objCommon.GetHeader(Convert.ToString(new SecurePage().LoggedInUserId));
                    return dtMenuItems;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            return null;
        }

        #endregion

        #region Function for adding top menu items

        /// <summary>
        /// Adds the top/parent menu items for the menu
        /// </summary>
        /// <param name="menuData"></param>
        private void AddTopMenuItems(DataTable menuData)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData) { RowFilter = "AjParentId IS NULL " };
                foreach (DataRowView row in view)
                {
                    //Adding the menu item
                    var newMenuItem = new MenuItem(row["AjMenuName"].ToString(), row["AjMenuId"].ToString())
                    {
                        NavigateUrl = row["AjMenuUrl"].ToString()
                    };
                    menuBar.Items.Add(newMenuItem);
                    AddChildMenuItems(menuData, newMenuItem);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                view = null;
            }
        }

        #endregion

        #region Function for adding child menu items from database

        /// <summary>
        /// This code is used to recursively add child menu items by filtering by ParentID
        /// </summary>
        /// <param name="menuData"></param>
        /// <param name="parentMenuItem"></param>
        private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData) { RowFilter = "AjParentId=" + parentMenuItem.Value };
                foreach (DataRowView row in view)
                {
                    var newMenuItem = new MenuItem(row["AjMenuName"].ToString(), row["AjMenuId"].ToString())
                                          {
                                              NavigateUrl
                                                  =
                                                  row
                                                  [
                                                      "AjMenuUrl"
                                                  ]
                                                  .ToString
                                                  ()
                                          };

                    
                    if (Request.Url.AbsolutePath == newMenuItem.NavigateUrl)
                        parentMenuItem.Selected = true;

                    parentMenuItem.ChildItems.Add(newMenuItem);

                    AddChildMenuItems(menuData, newMenuItem);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                view = null;
            }
        }

      



    }

        #endregion
 

}