using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IryTech.AdmissionJankari.BO
{

    #region CollegeSpeech Class  Property

    [Serializable()]
  public  class CollegeSpeechProperty
    {
        public int CollegeSpeechId { get; set; }
        public string CollegeName { get; set; }
        public int CollegeBranhID { get; set; }
        public string CollegeSpeechPersonDesignation { get; set; }
        public string CollegeSpeechPersonName { get; set; }
        public string CollegeSpeechPersonImage { get; set; }
        public string CollegeSpeechDesc { get; set; }
        public string AboutKeyPerson { get; set; }
        public bool SpeechStatus { get; set; }


    }
    #endregion
}
