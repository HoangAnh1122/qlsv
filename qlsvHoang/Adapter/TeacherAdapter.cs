using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Adapter
{
	public class TeacherAdapter
	{
		public static TeacherVM toTeacherVM(Teacher teacher)
		{
			return new TeacherVM
			{
	          	Email=teacher.Email,
				Password = teacher.Password,
				PhoneNumber = teacher.PhoneNumber,
				Username = teacher.Username,
				RoleId = teacher.RoleId,
				Address = teacher.Address,
				Name=teacher.Name,
				
			};

		}
		public static Teacher toteacherDO(TeacherVM teacher)
		{
			return new Teacher
			{
				Email = teacher.Email,
				Password = teacher.Password,
				PhoneNumber = teacher.PhoneNumber,
				Username = teacher.Username,
				RoleId = teacher.RoleId,
                Address = teacher.Address,
                Name = teacher.Name,

            };
		}

        public static Teacher toUpdateteacherDO(UpdateTeacherVM teacher)
        {
            return new Teacher
            {
                Email = teacher.Email,
                    PhoneNumber= teacher.PhoneNumber,
                Address = teacher.Address,
                Name = teacher.Name,

            };
        }

        public static List<TeacherVM> listTeacherVM(List<Teacher> teachers)
		{
			return teachers.Select(toTeacherVM).ToList();
		}
	}
}
