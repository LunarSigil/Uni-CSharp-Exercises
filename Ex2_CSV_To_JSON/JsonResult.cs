namespace Ex2_CSV_To_JSON
{
    class JsonResult
    {
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public HashSet<Student> Students { get; set; }
        public HashSet<ActiveStudies> ActiveStudies { get; set; }
    }
}
