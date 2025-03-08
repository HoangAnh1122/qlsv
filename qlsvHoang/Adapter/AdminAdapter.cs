using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Adapter
{
    public class AdminAdapter
    {
        public static AdminVM toAdminVM(Admin admin)
        {
            return new AdminVM
            {
                Password = admin.Password,
                RoleId = admin.RoleId,
                Username = admin.Username,
                Name=admin.Name,
        
            };

        }
        public static Admin toAdminDO(AdminVM student)
        {
            return new Admin
            {
                Username = student.Username,
                RoleId = student.RoleId,
                Password = student.Password,
                Name = student.Name,
                
            };
        }
        public static Admin UpdateAdminToDO(UpdateAdminVM updateAdminVM)
        {
            return new Admin
            {
                Username=updateAdminVM.Username,
                RoleId=updateAdminVM.RoleId,
                Name=updateAdminVM.Name,
                
            };
        }
   
        public static List<AdminVM> listStudentVM(List<Admin> admins)
        {
            return admins.Select(toAdminVM).ToList();
        }
    }
}
