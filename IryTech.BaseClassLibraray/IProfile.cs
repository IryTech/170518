using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{

    public interface IProfile
    {


        List<ExamAppearedProperty> GetAllExamAppearedList(int userId);
        List<CoursePreffered> GetAllCoursePreferList(int userId);
     List<CollegePrefered> GetStudentCollegePreference(int userId);
     List<CourseStreamPreffered> GetCourseStreamListPreferedByStudent(int userId);
     List<CityPrefferedProperty> GetCityPreferedByStudent(int userId);
     int StudentInsertExamAppeared(string examName, string rank, int userId);

     List<StudentQueryProperty> GetStudentQuery(int userId);
     List<SchoolBoardproperty> GetBoardDetails();

        int InsertStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty, int userId,
                                                 out string errMsg);

        int UpdateStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);

        int InsertStudentInterSchoolDetails(StudentInterSchoolProperty objStudentHighSchoolProperty, int userId,
                                               out string errMsg);

        int UpdateStudentInterSchoolDetails(StudentInterSchoolProperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);


        int InsertStudentDiplomaDetails(StudentDiplomaProperty objStudentHighSchoolProperty, int userId,
                                               out string errMsg);

        int UpdateStudentDiplomaDetails(StudentDiplomaProperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);

        int InsertStudentGraduationDetails(StudentGraduationproperty objStudentHighSchoolProperty, int userId,
                                             out string errMsg);

        int UpdateStudentGraduationDetails(StudentGraduationproperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);
    }
}
