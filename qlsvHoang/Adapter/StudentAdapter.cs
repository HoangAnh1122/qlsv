using qlsvHoang.Models;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Adapter
{
    public static class StudentAdapter
    {
        public static StudentVM toStudentVM(Student student)
        {
            return new StudentVM
            {
                Address = student.Address,
                ClassName = student.ClassName,
                DateOfBirth = student.DateOfBirth,
                Name = student.Name,
                Password = student.Password,
                PhoneNumber = student.PhoneNumber,
                Username = student.Username,
                RoleId = student.RoleId,
            };

        }
        public static Student toStudentDO(StudentVM student)
        {
            return new Student
            {
                Address = student.Address,
                ClassName = student.ClassName,
                DateOfBirth = student.DateOfBirth,
                Name = student.Name,
                Password = student.Password,
                PhoneNumber = student.PhoneNumber,
                Username = student.Username,
                RoleId = student.RoleId,
                
            };
        }
        public static Student toUpdateStudentoDO(EditStudentVM student)
        {
            return new Student
            { Name = student.Name,
            Address = student.Address,
            DateOfBirth= student.DateOfBirth,
            PhoneNumber= student.PhoneNumber,
            StudentId = student.StudentId,
            RoleId= student.RoleId,
            ClassName = student.ClassName,
            Username= student.Username,
            };
        }
        public static List<StudentVM>  listStudentVM(List<Student> students)
        { 
            return students.Select(toStudentVM).ToList();
        }
    }
}
