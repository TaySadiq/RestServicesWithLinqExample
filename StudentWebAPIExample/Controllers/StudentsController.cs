using Microsoft.AspNetCore.Mvc;
using StudentWebAPIExample.Models;
using StudentWebAPIExample.Services;

namespace StudentWebAPIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IClassroomService classroomService;

        public StudentsController(IClassroomService classroomService)
        {
            this.classroomService = classroomService;
        }

        // GET api/students
        /// <summary>
        /// Call the classroom service to Get all the students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Student> Get()
        {
            //TODO:  Replace the new List<Student>() code below and call the classroom service to Get all the students
            var students = new List<Student>();
            return students;
        }

        //GET api/major/{major}
        [HttpGet("major/{major}")]
        public ActionResult<IEnumerable<IGrouping<int, Student>>> GetStudentsByMajor(string major)
        {
            //TODO: Replace the new List<Student>() code below and call the classroom service to Get all the students by their major
            var students = new List<Student>();

            if (!students.Any())
                return NotFound();

            return Ok(students);
        }

        //GET api/age-group
        [HttpGet("age-group")]
        public ActionResult<List<StudentsByAgeDTO>> GetStudentsByAge()
        {
            //TODO:  Replace the new List<Student>() code below and call the classroom service to Get all the students and group by age
            var students = new List<Student>(); 

            if (!students.Any())
                return NotFound();

            return Ok(students);
        }

        //GET api/sorted-by-name
        [HttpGet("sorted-by-name")]
        public ActionResult<IEnumerable<Student>> GetStudentsSortedByName()
        {
            var sortedStudents = classroomService.SortStudentsByName();
            if (!sortedStudents.Any())
                return NotFound();

            return Ok(sortedStudents);
        }

        // POST api/students
        [HttpPost]
        public ActionResult<Student> Post(Student student)
        {
            classroomService.AddStudent(student);
            var returnedStudent = classroomService.GetStudentById(student.Id);
            if (returnedStudent == null)
            {
                return BadRequest("Failed to save new student");
            }
            return Ok(returnedStudent);
        }

        // GET api/students/{id}
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = classroomService.GetStudentById(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // PUT api/students/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)
        {
            classroomService.UpdateStudent(id, student);
            return Ok();
        }

        // DELETE api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            classroomService.DeleteStudent(id);
            return Ok();
        }
    }
}
