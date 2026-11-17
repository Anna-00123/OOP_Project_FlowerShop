namespace FlowerShopApp
{
    public class BouquetItem
    {
        public Flower Flower { get; private set; }
        public int Quantity { get; set; }

        public BouquetItem(Flower flower, int quantity)
        {
            throw new NotImplementedException();
        }

        public decimal GetPrice()
        {
            throw new NotImplementedException();
        }
    }
}