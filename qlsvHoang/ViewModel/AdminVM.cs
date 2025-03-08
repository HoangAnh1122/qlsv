using qlsvHoang.Models;

namespace qlsvHoang.ViewModel
{
    public class AdminVM
    {

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }
        public string Name { get; set; }


    }
    public class UpdateAdminVM
    {

        public string Username { get; set; } = null!;
        public int RoleId { get; set; }
        public string Name { get; set; }


    }
}
