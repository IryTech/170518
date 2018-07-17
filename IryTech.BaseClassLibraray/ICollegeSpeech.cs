using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IryTech.AdmissionJankari.BO;
namespace IryTech.BaseClassLibraray
{
    public interface ICollegeSpeech
    {

        int InsertCollegeSpeechDetails(CollegeSpeechProperty objCollegeGroupProperty, int createdBy, out string errmsg);
        int UpdateCollegeSpeechDetails(CollegeSpeechProperty objCollegeGroupProperty, int modifiedBy, out string errmsg);
    }
}
