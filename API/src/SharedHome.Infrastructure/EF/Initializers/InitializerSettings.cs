namespace SharedHome.Infrastructure.EF.Initializers
{
    public class InitializerSettings
    {
        public const string SectionName = "Initializer";

        public string AdminPassword { get; set; } = default!;

        public string CharlesPassword { get; set; } = default!;

        public string FrancPassword { get; set; } = default!;
    }
}
