namespace EComerce.Api.Costumers.Profiles
{
    public class CustomerProfile: AutoMapper.Profile
    {
        public CustomerProfile() {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
