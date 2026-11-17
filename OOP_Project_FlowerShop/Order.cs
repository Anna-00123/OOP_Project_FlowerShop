namespace FlowerShopApp
{
    public class Order : IPrintable
    {
        public int Number { get; private set; }
        public Customer Customer { get; private set; }
        public Bouquet Bouquet { get; private set; }
        public Employee Florist { get; private set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderStatus Status { get; private set; }

        public const decimal DeliveryPrice = 150m;
        public const decimal FreeDeliveryFrom = 2000m;

        public Order(int number, Customer customer, Bouquet bouquet,
                     string deliveryAddress, DateTime deliveryDate)
        {
            throw new NotImplementedException();
        }

        public void AssignFlorist(Employee florist)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotal()
        {
            throw new NotImplementedException();
        }

        public void Pay()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public string PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}