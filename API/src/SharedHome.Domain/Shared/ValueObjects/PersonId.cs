namespace SharedHome.Domain.Shared.ValueObjects
{
    public record PersonId
    {
        public Guid Value { get;}

        private PersonId() : this(Guid.NewGuid())
        {

        }

        public PersonId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(PersonId personId) => personId.Value;

        public static implicit operator PersonId(Guid value) => new(value);
    }
}
