using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Service.FacadePattern
{
    public class StudentFacade
    {
        private readonly qlsvHoangContext _context;

        public StudentFacade(qlsvHoangContext context)
        {
            _context = context;
         
        }

        // Lấy danh sách sinh viên
        public List<Student> GetAllStudents()
        {
            
            return _context.Students.Include(s => s.Role).ToList();
        }

        // Lấy thông tin sinh viên theo ID
        public async Task<Student?> GetStudentByUserName(string username)
        {
            return await _context.Students.Include(s => s.Role).FirstOrDefaultAsync(s => s.Username == username);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == id);

        }
        // Thêm sinh viên mới
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        // Cập nhật thông tin sinh viên
        public async Task<int> UpdateStudent(Student student)
        {
            try
            {
                //check exit student
             

             var res=   _context.Students.Update(student);
             _context.SaveChanges();
                if(res!=null)
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

        // Xóa sinh viên
        public async Task<int> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return 1;
            }
            return -1;
        }

        public virtual async Task<Student > loginStudent(LoginStudentVM loginStudentVM)
        {
            var studentcheck = await _context.Students.FirstOrDefaultAsync(x => x.Username == loginStudentVM.Username && x.Password == Common.Security.Hash(loginStudentVM.Password));
            return studentcheck;
        }
    }

}
