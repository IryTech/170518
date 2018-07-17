using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IryTech.BaseClassLibraray;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class CollegeSpeechProvider : ICollegeSpeech
    {
        public static CollegeSpeechProvider Instance
        {
            get
            {                    

                return new  CollegeSpeech();
            }
        }

        #region ICollege Speech  Members
        public abstract int InsertCollegeSpeechDetails(CollegeSpeechProperty objCollegeSpeechProperty, int createdBy, out string errmsg);
        public abstract int UpdateCollegeSpeechDetails(CollegeSpeechProperty objCollegeSpeechProperty, int modifiedBy, out string errmsg);
        public abstract List<CollegeSpeechProperty> GetAllCollegeSpeechList();
        public abstract List<CollegeSpeechProperty> GetCollegeSpeechById(int CollegeSpeechID);
        public abstract List<CollegeSpeechProperty> GetCollegeSpeechByCourseId(int courseId);
        #endregion




    }
}
