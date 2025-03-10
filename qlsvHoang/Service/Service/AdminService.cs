using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.Service
{
    public class AdminService : IAdminService
    {
        private readonly qlsvHoangContext dbContext;

        public AdminService(qlsvHoangContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Admin> CreateAdmin(Admin admin)
        {
            try
            {
                var res = dbContext.Add(admin);
                dbContext.SaveChanges();
                return admin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteAdmin(int id)
        {
            try
            {
                //check exit
                var checkexit = await dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(x => x.AdminId == id);
                if (checkexit != null)
                {
                    var delete = dbContext.Admins.Remove(checkexit);
                    await dbContext.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public async Task<Admin> getAdminById(int id)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(x => x.AdminId == id);
            return admin;
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            var listAdmin = await dbContext.Admins.ToListAsync();
            return listAdmin;
        }

        public async Task<Admin> LoginAdmin(AdminLoginVM adminLogin)
        {
            var admim = await dbContext.Admins.FirstOrDefaultAsync(x => x.Username == adminLogin.Username && x.Password == Common.Security.Hash(adminLogin.Password));
            return admim;
        }

        public async Task<int> UpdateAdmin(UpdateAdminVM admin)
        {
            var res = await dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(x => x.Username == admin.Username);

            if (res != null)
            {
                //map do
                var mapDo = Adapter.AdminAdapter.UpdateAdminToDO(admin);
                mapDo.Password = res.Password;
                mapDo.AdminId = res.AdminId;

                dbContext.Admins.Update(mapDo);
                await dbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
