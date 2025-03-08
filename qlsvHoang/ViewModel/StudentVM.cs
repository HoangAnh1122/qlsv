using qlsvHoang.Models;

namespace qlsvHoang.ViewModel
{
    public class StudentVM
    {
        public string Username { get; set; } = null!;
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public string Password { get; set; } = null!;

        public string ClassName { get; set; } = null!;

        public int RoleId { get; set; }

    }

    public class EditStudentVM

    {
        public int StudentId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; } = 3;

        public string ClassName { get; set; } = null!;



    }
    public class GetStudentVM
    {
        public string StudentId { get; set; }
		public string Username { get; set; } = null!;
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }


		public string Password { get; set; } = null!;

		public string ClassName { get; set; } = null!;

		public int RoleId { get; set; }
	}
}
