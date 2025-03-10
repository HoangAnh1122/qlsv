using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.Service
{
	public class CourseService : ICourseService
	{
		private readonly qlsvHoangContext context;

		public CourseService(qlsvHoangContext context)
        {
			this.context = context;
		}
        public async Task<Course> createCourse(CourseVM course)
		{
			//
			try
			{
				//map data to do
				var mapdata=Adapter.CourseAdapter.toCourse(course);
				context.Courses.Add(mapdata);
				 await	context.SaveChangesAsync();

				return mapdata;

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<int> deleteCourse(int id)
		{
			//check exit;
			try
			{
				var res=await findCourseById(id);
				if (res != null)
				{
					context.Courses.Remove(res);
					await context.SaveChangesAsync();
					return 1;
				}
				return -1;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Course> findCourseById(int id)
		{
			try
			{

			var res=await context.Courses.AsNoTracking().FirstOrDefaultAsync(x=>x.CourseId==id);
			return res;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Course>> getListCourse()
		{
			try
			{
			var res=await context.Courses.ToListAsync();

			return res;
			}
			catch( Exception ex)
			{
				throw new Exception(ex.Message);	

			}
		}

		public async Task<int> updateCourse(Course course)
		{
			try
			{
				//check exit
				var checkdata=await findCourseById(course.CourseId);
				if (checkdata != null)
				{
					context.Courses.Update(course);
					await context.SaveChangesAsync();
					return 1;
				}
				return -1;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
