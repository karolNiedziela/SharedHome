namespace SharedHome.Domain.Invitations.ValueObjects
{
    public record InvitationId
    {
        public Guid Value { get; }

        public InvitationId() : this(Guid.NewGuid())
        {

        }

        public InvitationId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(InvitationId invitationId) => invitationId.Value;

        public static implicit operator InvitationId(Guid value) => new(value);
    }
}
