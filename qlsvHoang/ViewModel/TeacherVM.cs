using qlsvHoang.Models;

namespace qlsvHoang.ViewModel
{
	public class TeacherVM
	{


		public string Username { get; set; } = null!;

		public string Password { get; set; } = null!;
		public string? Name { get; set; } 

		public int RoleId { get; set; }

		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
	}

    public class UpdateTeacherVM
    {
		public int TeacherId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }

	public class LoginTeacherVM
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
