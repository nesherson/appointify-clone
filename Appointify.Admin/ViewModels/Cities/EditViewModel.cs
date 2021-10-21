using Appointify.Data.Entities;

namespace Appointify.Admin.ViewModels.Cities
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }

    }
}
