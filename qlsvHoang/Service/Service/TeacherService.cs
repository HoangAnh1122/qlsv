using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.Service
{
	public class TeacherService : ITeacherService
	{
		private readonly qlsvHoangContext context;

		public TeacherService(qlsvHoangContext context)
		{
			this.context = context;
		}
		public async Task<int> createTeacher(TeacherVM vm)
		{
			try
			{
				//use adapter maping vm to dto
				var Datado = Adapter.TeacherAdapter.toteacherDO(vm);
				var res = await context.Teachers.AddAsync(Datado);
				await context.SaveChangesAsync();
				if (res != null)
				{
					return 1;
				}
				return 0;

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<int> deleteTeacher(int id)
		{
			try
			{
				//check exit teacher
				var checkexit = await context.Teachers.FindAsync(id);
				if (checkexit == null)
				{
					return -1;
				}
				//delete teacher
				context.Teachers.Remove(checkexit);
				await context.SaveChangesAsync();
				return 1;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public async Task<Teacher> findTeacherById(int id)
        {
			var res = await context.Teachers.FirstOrDefaultAsync(x=>x.TeacherId == id);
			return res;
        }

        public async Task<List<Teacher>> getListTeachers()
		{
			try
			{
				var res = await context.Teachers.ToListAsync();

				return res;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<int> updateTeacher(UpdateTeacherVM teacher)
		{
			try
			{
				//check exit teacher
				var checkexit = await context.Teachers.FindAsync(teacher.TeacherId);
				if (checkexit == null)
				{
					return -1;
				}
				//map
			     checkexit.Name= teacher.Name;
				checkexit.PhoneNumber= teacher.PhoneNumber;
				checkexit.Address= teacher.Address;
				checkexit.Email= teacher.Email;

				//update
				context.Teachers.Update(checkexit);
				await context.SaveChangesAsync();
				return 1;

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
	}
}
