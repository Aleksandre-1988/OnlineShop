using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class AddressResponse
    {
        public List<Address?> Addresses { get; set; }
        public Address? Address { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}
