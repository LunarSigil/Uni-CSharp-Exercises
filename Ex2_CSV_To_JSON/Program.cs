using Ex2_CSV_To_JSON;
using Ex2_CSV_To_JSON.Comparers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CSV_To_JSON
{
    class Program
    {
        private static readonly string WorkingDir = Directory.GetCurrentDirectory();
        private static readonly string ProjectDir = Directory.GetParent(WorkingDir).Parent.Parent.FullName;
        private static int _expectedNumOfInfoColumns = 9;

        static async Task Main(string[] args)
        {
            await using StreamWriter logStreamWriter = new($@"{ProjectDir}\Logs\log.txt");
            string errorMessage;

            if (args.Length != 2)
            {
                errorMessage = "Two arguments required: CSV file path and destination file path";
                await WriteToFile(logStreamWriter, errorMessage);
                throw new ArgumentException(errorMessage);
            }

            string dataFile = args[0];
            string outputFile = args[1];

            if (!File.Exists(dataFile))
            {
                errorMessage = "Given CSV file path is incorrect";
                await WriteToFile(logStreamWriter, errorMessage);
                throw new ArgumentException(errorMessage);
            }
            
            if (!File.Exists(outputFile))
            {
                errorMessage = "Output file does not exist in given file path";
                await WriteToFile(logStreamWriter, errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            StudentComparer studentComparer = new();
            HashSet<Student> students = new(studentComparer);

            using StreamReader streamReader = new StreamReader(dataFile);
            string line;
            int counter = 0;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                counter++;
                string[] studentData = line.Split(',');

                if (studentData.Length != _expectedNumOfInfoColumns || !IsStudentDataValid(studentData))
                {
                    await WriteToFile(logStreamWriter, $"Line {counter}: Student skipped - data missing");
                    continue;
                }

                Student student = CreateStudent(studentData);

                if (!students.Add(student))
                {
                    Student foundStudent;
                    students.TryGetValue(student, out foundStudent);

                    bool studiesResult = false;

                    StudiesComparer studiesComparer = new();
                    HashSet<Studies> studiesSet = new(foundStudent.Studies, studiesComparer);

                    foreach (Studies studies in student.Studies)
                    {
                        if (studiesSet.Contains(studies))
                        {
                            await WriteToFile(logStreamWriter, $"Line {counter}: Student skipped - duplicate");
                            continue;
                        }
                        studiesResult = foundStudent.Studies.Add(studies);
                    }
                }
            }

            ActiveStudiesComparer activeStudiesComparer = new();
            HashSet<ActiveStudies> activeStudiesSet = new(activeStudiesComparer);

            foreach (Student student in students)
            {
                foreach (Studies studies in student.Studies )
                {
                    ActiveStudies activeStudies = CreateActiveStudies(studies.Name);

                    if (!activeStudiesSet.Add(activeStudies))
                    {
                        ActiveStudies foundActiveStudies;
                        activeStudiesSet.TryGetValue(activeStudies, out foundActiveStudies);
                        foundActiveStudies.NumberOfStudents++;
                    }
                }
            }

            string jsonResult = ParseToJson(students, activeStudiesSet);
            await using StreamWriter outputStreamWriter = new(outputFile);
            await WriteToFile(outputStreamWriter, jsonResult);
        }

        private static string ParseToJson(HashSet<Student> students, HashSet<ActiveStudies> activeStudiesSet)
        {
            JsonResult jsonResult = new()
            {
                CreatedAt = DateTime.Now,
                Author = "LunarSigil",
                Students = students,
                ActiveStudies = activeStudiesSet
            };

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(jsonResult, jsonSerializerOptions);
        }

        private static ActiveStudies CreateActiveStudies(string name)
        {
            return new ActiveStudies()
            {
                Name = name,
                NumberOfStudents = 1
            };
        }

        private static Student CreateStudent(string[] data)
        {
            HashSet<Studies> studies = new();
            studies.Add(new Studies()
            {
                Name = data[2],
                Mode = data[3]
            });

            return new Student()
            {
                FirstName = data[0],
                LastName = data[1],
                Studies = studies,
                Index = int.Parse(data[4]),
                Birthdate = DateTime.Parse(data[5]),
                Email = data[6],
                MotherName = data[7],
                FatherName = data[8]
            };
        }

        private static bool IsStudentDataValid(string[] data)
        {
            foreach (string dataItem in data)
            {
                if (string.IsNullOrWhiteSpace(dataItem))
                {
                    return false;
                }
            }
            return true;
        }

        private static async Task WriteToFile(StreamWriter streamWriter, string text)
        {
            await streamWriter.WriteLineAsync(text);
        }
    }
}