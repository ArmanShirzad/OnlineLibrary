namespace OnlineLibrary.Core.ValueObjects
{
    public class ISBN
    {
        public string Value { get; }

        public ISBN(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ISBN cannot be empty");

            if (value.Length != 13)
                throw new ArgumentException("ISBN must be 13 characters long");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var isbn = (ISBN)obj;
            return Value == isbn.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
