namespace SmartCarRentals.Web.ViewModels.Administration.Contracts
{
    public interface IDetailsViewModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
