using Ex2_CSV_To_JSON;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSV_To_JSON
{
    class Program
    {
        private static readonly string WorkingDir = Directory.GetCurrentDirectory();
        private static readonly string ProjectDir = Directory.GetParent(WorkingDir).Parent.Parent.FullName;
        private static readonly int NumOfColumns = 9;

        static async Task Main(string[] args)
        {

            await using StreamWriter logStreamWriter = new StreamWriter(Path.Combine(ProjectDir, "log.txt"));
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

            HashSet<Student> students = new();

            using StreamReader streamReader = new StreamReader(dataFile);
            string line;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                string[] studentData = line.Split(',');

                Student student = new()
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

                students.Add(student);
            }

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            JsonResult jsonResult = new()
            {
                CreatedAt = DateTime.Now,
                Author = "LunarSigil",
                Students = students
            };

            string stringJsonResult = JsonSerializer.Serialize(jsonResult, jsonSerializerOptions);
            Console.WriteLine(stringJsonResult);

        }

        private static async Task WriteToFile(StreamWriter streamWriter, string text)
        {
            await streamWriter.WriteLineAsync(text);
        }
    }
}