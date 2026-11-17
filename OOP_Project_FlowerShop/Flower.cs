namespace FlowerShopApp
{
    public class Flower : IPriceable
    {
        public int Id { get; set; }
        public FlowerType Type { get; set; }
        public FlowerColor Color { get; set; }
        public decimal Price { get; set; }
        public int StemLengthCm { get; set; }
        public DateTime ArrivalDate { get; set; }

        public const int FreshDays = 7;

        public Flower(int id, FlowerType type, FlowerColor color, decimal price,
                      int stemLengthCm, DateTime arrivalDate)
        {
            throw new NotImplementedException();
        }

        public decimal GetPrice()
        {
            throw new NotImplementedException();
        }

        public bool IsFresh(DateTime today)
        {
            throw new NotImplementedException();
        }

        public void ApplyDiscount(int percent)
        {
            throw new NotImplementedException();
        }
    }
}