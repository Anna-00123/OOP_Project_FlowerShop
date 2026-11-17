namespace FlowerShopApp
{
    public class Bouquet : IPriceable, IPrintable
    {
        public string Name { get; set; }
        public WrappingType Wrapping { get; set; }
        public List<BouquetItem> Items { get; private set; } = new List<BouquetItem>();

        public const int MaxFlowers = 101;

        public Bouquet(string name, WrappingType wrapping)
        {
            throw new NotImplementedException();
        }

        public void AddFlower(Flower flower, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFlower(int flowerId)
        {
            throw new NotImplementedException();
        }

        public int GetFlowerCount()
        {
            throw new NotImplementedException();
        }

        public static decimal GetWrappingPrice(WrappingType wrapping)
        {
            throw new NotImplementedException();
        }

        public decimal GetPrice()
        {
            throw new NotImplementedException();
        }

        public string PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}