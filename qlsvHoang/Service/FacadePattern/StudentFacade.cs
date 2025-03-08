using Microsoft.EntityFrameworkCore;
using qlsvHoang.Models;

namespace qlsvHoang.Service.FacadePattern
{
    public class StudentFacade
    {
        private readonly Se06303Nhom9Context _context;

        public StudentFacade(Se06303Nhom9Context context)
        {
            _context = context;
         
        }

        // Lấy danh sách sinh viên
        public List<Student> GetAllStudents()
        {
            return _context.Students.Include(s => s.Role).ToList();
        }

        // Lấy thông tin sinh viên theo ID
        public Student? GetStudentById(int id)
        {
            return _context.Students.Include(s => s.Role).FirstOrDefault(s => s.StudentId == id);
        }

        // Thêm sinh viên mới
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        // Cập nhật thông tin sinh viên
        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        // Xóa sinh viên
        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }

}
