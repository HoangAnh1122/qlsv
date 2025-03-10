namespace qlsvHoang.ViewModel
{
	public class CourseVM
	{
		public string CourseName { get; set; } = null!;

		public string? Description { get; set; }

	}

	public class UpdateCourseVM
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; } = null!;

		public string? Description { get; set; }


	}
}
