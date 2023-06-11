using StudentWebAPIExample.Models;

namespace StudentWebAPIExample.Services
{
    public interface IClassroomService
    {
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        List<Student> GetStudentsByMajor(string major);
        List<StudentsByAgeDTO> GroupStudentsByAge();
        IEnumerable<Student> SortStudentsByName();
        void AddStudent(Student student);
        void UpdateStudent(int id, Student student);
        void DeleteStudent(int id);
    }
}
