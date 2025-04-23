namespace Ex2_CSV_To_JSON.Comparers
{
    class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.FirstName} {x.LastName} {x.Index}", $"{y.FirstName} {y.LastName} {y.Index}");
        }

        public int GetHashCode(Student obj)
        {
            return obj.Index.GetHashCode();
        }
    }
}
