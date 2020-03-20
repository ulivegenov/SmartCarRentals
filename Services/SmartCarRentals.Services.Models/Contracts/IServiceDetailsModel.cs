namespace SmartCarRentals.Services.Models.Contracts
{
    public interface IServiceDetailsModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
