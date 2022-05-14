namespace SharedHome.Infrastructure.EF.Initializers
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
