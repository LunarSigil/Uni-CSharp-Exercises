namespace Ex3_REST_Local_File.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Studies Studies { get; set; }
        public int Index { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
    }
}