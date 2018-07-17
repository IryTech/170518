

using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{
   public interface  IStream
    {
       int InsertCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int createdBy, out string errmsg);
       int UpdateCourseStreamDetails(CourseStreamProperty objCourseStreamProperty, int modifiedBy, out string errmsg);
       List<CourseStreamProperty> GetAllStreamList();
       List<CourseStreamProperty> GetStreamListById(int streamId);
       List<CourseStreamProperty> GetStreamListByCourse(int courseId);
       List<CourseStreamProperty> GetStreamListByStreamNameCourseId(int courseId, string streamName);
       List<CourseStreamProperty> GetStreamListByStreamName(string streamName);

                                    
    }
}
