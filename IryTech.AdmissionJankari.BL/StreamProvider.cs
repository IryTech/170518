using System.Collections.Generic;
using IryTech.BaseClassLibraray;


namespace IryTech.AdmissionJankari.BL
{
    public abstract class StreamProvider :IStream 
    {

        public static StreamProvider Instance
        {
            get
            {
                return new Stream();
            }

        }

        public abstract int InsertCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int createdBy, out string errmsg);
        public abstract int UpdateCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int modifiedBy, out string errmsg);
        public abstract List<CourseStreamProperty> GetAllStreamList();
        public abstract List<CourseStreamProperty> GetStreamListById(int streamId);
        public abstract List<CourseStreamProperty> GetStreamListByCourse(int courseId);
        public abstract List<CourseStreamProperty> GetStreamListByStreamNameCourseId(int courseId, string streamName);
        public abstract List<CourseStreamProperty> GetStreamListByStreamName(string streamName);
        
      
    }
}
