using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class IndicatorControl : System.Web.UI.UserControl
    {
        private bool warning;
        private double percent;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IndicatorLabel.Width = new Unit(percent * IndicatorPanel.Width.Value, UnitType.Pixel);
                IndicatorLabel.ToolTip = percent.ToString("p0");
            }
            catch (Exception)
            {

            }
        }
        [Browsable(true)]
        [DefaultValue(false)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public bool Warning
        {
            get
            {
                return warning;
            }
            set
            {
                warning = value;
            }
        }

        [Browsable(true)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public double Percent
        {
            get
            {
                return percent;
            }
            set
            {
                percent = value;

                if (percent < 0)
                    percent = 0;

                if (percent > 1)
                    percent = 1;


                IndicatorLabel.Width = new Unit(percent * IndicatorPanel.Width.Value, UnitType.Pixel);
                IndicatorLabel.ToolTip = percent.ToString("p0");

                if (warning)
                {
                    if (percent > 0 && percent < .50)
                    {
                        IndicatorLabel.BackColor = Color.Green;
                    }
                    else if (percent >= .50 && percent < .75)
                    {
                        IndicatorLabel.BackColor = Color.Gold;
                    }
                    else if (percent >= .75 && percent < .90)
                    {
                        IndicatorLabel.BackColor = Color.Orange;
                    }
                    else if (percent >= .90)
                    {
                        IndicatorLabel.BackColor = Color.Red;
                    }
                }
            }
        }

        [Browsable(true)]
        [DefaultValue(20)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public double Width
        {
            get
            {
                return IndicatorPanel.Width.Value;
            }
            set
            {
                IndicatorPanel.Width = new Unit(value, UnitType.Pixel);
            }
        }

        [Browsable(true)]
        [DefaultValue(20)]
        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public double Height
        {
            get
            {
                return IndicatorPanel.Height.Value;
            }
            set
            {
                IndicatorPanel.Height = new Unit(value, UnitType.Pixel);
                IndicatorLabel.Height = new Unit(value, UnitType.Pixel);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected override object SaveControlState()
        {
            return percent;
        }

        protected override void LoadControlState(object savedState)
        {
            percent = Convert.ToDouble(savedState);
        }
    }
}