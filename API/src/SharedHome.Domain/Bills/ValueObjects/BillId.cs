namespace SharedHome.Domain.Bills.ValueObjects
{
    public record BillId
    {
        public Guid Value { get; }

        public BillId() : this(Guid.NewGuid())
        {

        }

        public BillId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(BillId billId) => billId.Value;

        public static implicit operator BillId(Guid value) => new(value);
    }
}
