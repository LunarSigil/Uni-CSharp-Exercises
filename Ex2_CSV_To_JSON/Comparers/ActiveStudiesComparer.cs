namespace Ex2_CSV_To_JSON.Comparers
{
    class ActiveStudiesComparer : IEqualityComparer<ActiveStudies>
    {
        public bool Equals(ActiveStudies x, ActiveStudies y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.Name}", $"{y.Name}");
        }

        public int GetHashCode(ActiveStudies obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}