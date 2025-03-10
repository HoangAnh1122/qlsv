using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.IService
{
	public interface IAdminService
	{
		Task<Admin> CreateAdmin(Admin admin);
		Task<int> UpdateAdmin(UpdateAdminVM admin);
		Task<Admin> getAdminById(int id);
		Task<int> DeleteAdmin(int id);
		Task<List<Admin>> GetAllAdmins();

		Task<Admin> LoginAdmin(AdminLoginVM adminLogin);
	}
}
