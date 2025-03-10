using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Adapter
{
	public static class CourseAdapter
	{
		public static CourseVM toCourseVM(Course course)
		{
			return new CourseVM
			{
				CourseName = course.CourseName,
				Description= course.Description,

			};

		}
		public static Course toCourse(CourseVM course)
		{
			return new Course
			{
				CourseName = course.CourseName,
				Description = course.Description,
			};

		}



		public static List<CourseVM> listTeacherVM(List<Course> teachers)
		{
			return teachers.Select(toCourseVM).ToList();
		}
	}
}
