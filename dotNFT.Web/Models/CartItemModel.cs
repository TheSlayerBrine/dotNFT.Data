namespace dotNFT.Models
{
    public class CartItem
    {
        public int NftId { get; set; }
        public string NftName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Amount * Price; }
        }
    }
}
