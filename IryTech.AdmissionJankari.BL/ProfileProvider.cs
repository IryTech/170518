using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class ProfileProvider : IProfile
    {
        public static ProfileProvider Instance
        {
            get { return new Profile(); }

        }
        public abstract List<ExamAppearedProperty> GetAllExamAppearedList(int userId);
        public abstract List<CollegePrefered> GetStudentCollegePreference(int userId);
        public abstract List<CoursePreffered> GetAllCoursePreferList(int userId);

        public abstract List<CourseStreamPreffered> GetCourseStreamListPreferedByStudent(int userId);


        public abstract List<CityPrefferedProperty> GetCityPreferedByStudent(int userId);
        public abstract int StudentInsertExamAppeared(string examName,string rank,int userId);

        public abstract List<StudentQueryProperty> GetStudentQuery(int userId);

        public abstract List<SchoolBoardproperty> GetBoardDetails();

        public abstract int InsertStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty, int userId, out string errMsg);

        public abstract int UpdateStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty,
                                                           int userId, out string errmsg);


        public abstract int InsertStudentInterSchoolDetails(StudentInterSchoolProperty objStudentHighSchoolProperty, int userId,
                                              out string errMsg);

        public abstract int UpdateStudentInterSchoolDetails(StudentInterSchoolProperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);


        public abstract int InsertStudentDiplomaDetails(StudentDiplomaProperty objStudentHighSchoolProperty, int userId,
                                              out string errMsg);

        public abstract int UpdateStudentDiplomaDetails(StudentDiplomaProperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);


        public abstract int InsertStudentGraduationDetails(StudentGraduationproperty objStudentHighSchoolProperty, int userId,
                                            out string errMsg);

        public abstract int UpdateStudentGraduationDetails(StudentGraduationproperty objStudentHighSchoolProperty, int userId,
                                           out string errmsg);
    }
}
