namespace SharedHome.Infrastructure.EF.Initializers
{
    public class InitializerOptions
    {
        public const string InitializerOptionsName = "Initializer";

        public string AdminPassword { get; set; } = default!;

        public string CharlesPassword { get; set; } = default!;

        public string FrancPassword { get; set; } = default!;
    }
}
