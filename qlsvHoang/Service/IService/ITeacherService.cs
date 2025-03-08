using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.IService
{
	public interface ITeacherService 
	{
		Task<int> createTeacher(TeacherVM vm);
		Task<List<Teacher>> getListTeachers();
		Task<int> updateTeacher(UpdateTeacherVM teacher);
		Task<int>  deleteTeacher(int id);
		Task<Teacher> findTeacherById(int id);
	}
}
