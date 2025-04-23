namespace Ex2_CSV_To_JSON
{
    class Student
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public HashSet<Studies> Studies { get; set; }
        public int Index { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
    }
}