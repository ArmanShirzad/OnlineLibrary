namespace OnlineLibrary.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty");

            if (!value.Contains("@"))
                throw new ArgumentException("Invalid email format");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var email = (Email)obj;
            return Value == email.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
