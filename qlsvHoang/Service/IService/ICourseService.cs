using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.IService
{
	public interface ICourseService
	{
		Task<Course> createCourse(CourseVM course);
		Task<List<Course>> getListCourse();
		Task<int> updateCourse(Course course);
		Task<int> deleteCourse(int id);
		Task<Course> findCourseById(int id);
	}
}
