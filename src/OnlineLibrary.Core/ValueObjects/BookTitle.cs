namespace OnlineLibrary.Core.ValueObjects
{
    public class BookTitle
    {
        public string Value { get; }

        public BookTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Book title cannot be empty");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var title = (BookTitle)obj;
            return Value == title.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
