namespace Ex2_CSV_To_JSON.Comparers
{
    class StudiesComparer : IEqualityComparer<Studies>
    {
        public bool Equals(Studies x, Studies y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals (x.Name, y.Name);
        }

        public int GetHashCode(Studies obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}