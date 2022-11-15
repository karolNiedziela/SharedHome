namespace SharedHome.Identity.Entities
{
    public class UserImage
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string PublicId { get; set; } = default!;

        public int Version { get; set; }

        public string Signature { get; set; } = default!;

        public int Width { get; set; } 

        public int Height { get; set; }

        public string Format { get; set; } = default!;

        public string ResourceType { get; set; } = default!;

        public int Bytes { get; set; }

        public string Type { get; set; } = default!;

        public string Url { get; set; } = default!;

        public string SecureUrl { get; set; } = default!;

        public string Path { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;

        public string UserId { get; set; } = default!;
    }
}
