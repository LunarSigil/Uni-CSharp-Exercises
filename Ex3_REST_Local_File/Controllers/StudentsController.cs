using Ex3_REST_Local_File.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ex3_REST_Local_File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private static readonly string filePath = "data.csv";
        private static List<Student> students;

        public StudentsController()
        {
            students = new List<Student>();

            FileInfo fileInfo = new(filePath);
            using StreamReader reader = new(fileInfo.OpenRead());
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] studentData = line.Split(',');

                students.Add(CreateStudent(studentData));
            }
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            Student foundStudent = students.Find(student => student.Index == studentId);
            if (foundStudent == null)
            {
                return NotFound($"Student with id {studentId} not found");
            }

            return Ok(foundStudent);
        }

        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, Student updatedStudent)
        {
            if (!IsStudentDataValid(updatedStudent))
            {
                return BadRequest("Student information must be complete");
            }

            Student foundStudent = students.Find(student => student.Index == studentId);

            if (foundStudent == null)
            {
                return NotFound($"Student with id {studentId} not found");
            }
            if (foundStudent.Index != updatedStudent.Index)
            {
                return BadRequest("Student id must remain the same");
            }

            foundStudent = updatedStudent;
            return Ok(foundStudent);
        }

        [HttpPost]
        public IActionResult AddStudent(Student newStudent)
        {
            if (!IsStudentDataValid(newStudent))
            {
                return BadRequest("Student information must be complete");
            }
            if (students.Any(student => student.Index == newStudent.Index))
            {
                return BadRequest("Student with that id already exists");
            }

            students.Add(newStudent);
            return Ok(newStudent);
        }

        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            Student foundStudent = students.Find(student => student.Index == studentId);
            if (foundStudent == null)
            {
                return NotFound($"Student with id {studentId} not found");
            }

            students.Remove(foundStudent);
            return Ok($"Student with id {studentId} deleted");
        }

        private Student CreateStudent(string[] studentData)
        {
            return new Student()
            {
                FirstName = studentData[0],
                LastName = studentData[1],
                Studies = new()
                {
                    Name = studentData[2],
                    Mode = studentData[3]
                },
                Index = int.Parse(studentData[4]),
                Birthdate = DateTime.Parse(studentData[5]),
                Email = studentData[6],
                MotherName = studentData[7],
                FatherName = studentData[8]
            };
        }

        private bool IsStudentDataValid(Student student)
        {
            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName)
                || string.IsNullOrEmpty(student.Studies.Name) || string.IsNullOrEmpty(student.Studies.Mode)
                || string.IsNullOrEmpty(student.Email)
                || string.IsNullOrEmpty(student.MotherName) || string.IsNullOrEmpty(student.FatherName))
            {
                return false;
            }
            return true;
        }

    }
}
