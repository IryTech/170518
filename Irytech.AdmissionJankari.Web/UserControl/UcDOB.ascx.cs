using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcDOB : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string url = Request.RawUrl;
                //if (url.Contains('?'))
                //{
                //    int id = Convert.ToInt32(url.Substring(url.IndexOf("?") + 1));
                //    BindPersonalInfo(id);
                //}

                if (this.SelectedDate == DateTime.MinValue)
                {
                    this.PopulateYear(DateTime.Now.Year.ToString());
                    this.PopulateMonth(DateTime.Now.Month.ToString());
                    this.PopulateDay();


                }
            }
        }
        private int Day
        {
            get
            {
                if (Request.Form[ddlDay.UniqueID] != null)
                {
                    return int.Parse(Request.Form[ddlDay.UniqueID]);
                }
                else
                {
                    return int.Parse(ddlDay.SelectedItem.Value);
                }
            }
            set
            {
                this.PopulateDay();
                ddlDay.ClearSelection();
                ddlDay.Items.FindByValue(value.ToString()).Selected = true;
            }
        }
        private int Month
        {
            get
            {
                return int.Parse(ddlMonth.SelectedItem.Value);
            }
            set
            {
                this.PopulateMonth(DateTime.Now.Month.ToString());
                ddlMonth.ClearSelection();
                ddlMonth.Items.FindByValue(value.ToString()).Selected = true;
            }
        }
        private int Year
        {
            get
            {
                return int.Parse(ddlYear.SelectedItem.Value);
            }
            set
            {
                this.PopulateYear(DateTime.Now.Year.ToString());
                ddlYear.ClearSelection();
                ddlYear.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.Month + "/" + this.Day + "/" + this.Year);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                {
                    this.Year = value.Year;
                    this.Month = value.Month;
                    this.Day = value.Day;
                }
            }
        }
        private void PopulateDay()
        {
            ddlDay.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "Day";
            lt.Value = "0";
            ddlDay.Items.Add(lt);
            int days = DateTime.DaysInMonth(this.Year, this.Month);
            for (int i = 1; i <= days; i++)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                ddlDay.Items.Add(lt);
            }
            //ddlDay.Items.FindByValue(DateTime.Now.Day.ToString()).Selected = true;
        }

        private void PopulateMonth(string Month)
        {
            ddlMonth.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "Month";
            lt.Value = "0";
            ddlMonth.Items.Add(lt);
            for (int i = 1; i <= 12; i++)
            {
                lt = new ListItem();
                lt.Text = Convert.ToDateTime(i.ToString() + "/1/1900").ToString("MMM");
                lt.Value = i.ToString();
                ddlMonth.Items.Add(lt);
            }
          //ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
            ddlMonth.Items.FindByValue(Month).Selected = true;
        }

        private void PopulateYear(string Year)
        {
            ddlYear.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "Year";
            lt.Value = "0";
            ddlYear.Items.Add(lt);
            for (int i = DateTime.Now.Year; i >= 1950; i--)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                ddlYear.Items.Add(lt);
            }
            //ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
            ddlYear.Items.FindByValue(Year).Selected = true;
        }

        public string ClientValidation
        {
            set { Validator.ValidationGroup = value; }
        }
     
    }
}