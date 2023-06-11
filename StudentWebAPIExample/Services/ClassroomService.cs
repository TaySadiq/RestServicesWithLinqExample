using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentWebAPIExample.Models;

namespace StudentWebAPIExample.Services
{
    public class ClassroomService : IClassroomService
    {
        //What could you update this to to not allow duplicate students? 
        private static List<Student> students = new List<Student>();
        public ClassroomService()
        {
            students.Add(new Student() { Id = 1, Name = "Alice", Age = 20, Major = "Computer Science" });
            students.Add(new Student() { Id = 2, Name = "Bob", Age = 21, Major = "Physics" });
            students.Add(new Student() { Id = 3, Name = "Charlie", Age = 19, Major = "Chemistry" });
            students.Add(new Student() { Id = 4, Name = "Dave", Age = 22, Major = "Mathematics" });
            students.Add(new Student() { Id = 5, Name = "Eve", Age = 20, Major = "Computer Science" });
        }

        public List<Student> GetAllStudents()
        {
            if (students.Count == 0)
            {
                students.Add(new Student() { Id = 1, Name = "Alice", Age = 20, Major = "Computer Science" });
                students.Add(new Student() { Id = 2, Name = "Bob", Age = 21, Major = "Physics" });
                students.Add(new Student() { Id = 3, Name = "Charlie", Age = 19, Major = "Chemistry" });
                students.Add(new Student() { Id = 4, Name = "Dave", Age = 22, Major = "Mathematics" });
                students.Add(new Student() { Id = 5, Name = "Eve", Age = 20, Major = "Computer Science" });
            }
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id);
            #region WithLinq
            //return students.Where(s => s.Id == id).FirstOrDefault();
            #endregion WithLinq
        }

        public List<Student> GetStudentsByMajor(string major)
        {
            //throw new NotImplementedException();
            #region WithLINQ
            return students.Where(s => s.Major.Equals(major, StringComparison.OrdinalIgnoreCase)).ToList();
            //var students = _students.Where(s => s.Major.Equals(major, StringComparison.OrdinalIgnoreCase));
            //if (!students.Any())
            //    return NotFound();

            //return Ok(students);
            #endregion WithLINQ
        }

        public List<StudentsByAgeDTO> GroupStudentsByAge()
        {
            //throw new NotImplementedException();
            #region WithLINQ 
            var ageGroupedStudents = from s in students
                                     group s by s.Age into g
                                     orderby g.Count() descending, g.Key
                                     where g.Count() >= 2
                                     select new
                                     {
                                         Age = g.Key,
                                         Count = g.Count()
                                     };

            var groupedStudents = students
                                    .GroupBy(s => s.Age)
                                    //.Where(g => g.Count() >= 2)
                                    .OrderByDescending(g => g.Count())
                                    .ThenBy(g => g.Key)
                                    .Select(g => new StudentsByAgeDTO
                                    {
                                        Age = g.Key,
                                        Count = g.Count()
                                    });

            return groupedStudents.ToList();
            #endregion WithLINQ
        }

        public IEnumerable<Student> SortStudentsByName()
        {
            throw new NotImplementedException();
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void UpdateStudent(int id, Student student)
        {
            var existingStudent = students.Find(s => s.Id == id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.GradeLevel = student.GradeLevel;
                existingStudent.Major = student.Major;
                existingStudent.Age = student.Age;
            }
        }

        public void DeleteStudent(int id)
        {
            var student = students.Find(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
        }
    }
}
