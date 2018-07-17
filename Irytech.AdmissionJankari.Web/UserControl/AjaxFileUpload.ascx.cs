using System;
using System.Web.UI;
using IryTech.AdmissionJankari.BL;
using AjaxControlToolkit;
using System.Drawing;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    [Serializable]
    public partial class AjaxFileUpload : System.Web.UI.UserControl
    {
        #region  "DataMemeber"

        Common _objCommon;
        string strImageName = Common.NoImageSubstitute;


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session["AsyncUpldItemImage"] = null;

        }



        protected void AsyncUpldItemImage_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            // System.Threading.Thread.Sleep(1000);
            Session["AsyncUpldItemImage"] = null;
            if (Session["AsyncUpldItemImage"] == null && AsyncUpldItemImage.HasFile)
            {
                Session["AsyncUpldItemImage"] = AsyncUpldItemImage;
            }

            else if (Session["AsyncUpldItemImage"] != null && (!AsyncUpldItemImage.HasFile))
            {
                AsyncUpldItemImage = (AsyncFileUpload)Session["AsyncUpldItemImage"];
            }

            else if (AsyncUpldItemImage.HasFile)
            {
                Session["AsyncUpldItemImage"] = AsyncUpldItemImage;
            }
            if (((AsyncFileUpload)Session["AsyncUpldItemImage"]).HasFile)
            {
                string ImageName = ((AsyncFileUpload)Session["AsyncUpldItemImage"]).FileName;
                string directory = Server.MapPath(uploadToDirectory + ImageName);
                Bitmap originalBMP = new Bitmap(((AsyncFileUpload)Session["AsyncUpldItemImage"]).FileContent);
                originalBMP.Save(directory);
                originalBMP.Dispose();
                //hfImageName.Value = ImageName;

              // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "image", "top.$get(ImgPrev).src ='" + uploadToDirectory + ImageName + "';", true);


                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "image", "top.$get(\"" + PrevImage.ClientID + "\").src = '" + uploadToDirectory + ImageName + "';", true);



            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetImageName", "document.getElementById(\"" + hfImageName.ClientID + "\").value='" + ImageName + "';", true);
               
            }


            //UploadedImageName = strImageName;

        }

        string strDirectoryPath;
        public string uploadToDirectory
        {
            set { strDirectoryPath = value; }
            get { return strDirectoryPath; }

        }


        public string UploadedImageName
        {

            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(Session["AsyncUpldItemImage"].ToString()))
                        if (((AsyncFileUpload)Session["AsyncUpldItemImage"]).HasFile)
                            strImageName = ((AsyncFileUpload)Session["AsyncUpldItemImage"]).FileName;
                        else
                            strImageName = Common.NoImageSubstitute;
                }
                catch { }
                return strImageName;
            }

        }



       public string SetImgUrl
         {
           set { PrevImage.ImageUrl = value; }

            get { return PrevImage.ImageUrl; }
       }


    }
}