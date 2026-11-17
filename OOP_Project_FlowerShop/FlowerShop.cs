namespace FlowerShopApp
{
    public class FlowerShop
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Flower> Stock { get; private set; } = new List<Flower>();
        public List<Employee> Employees { get; private set; } = new List<Employee>();
        public List<Customer> Customers { get; private set; } = new List<Customer>();
        public List<Order> Orders { get; private set; } = new List<Order>();

        public FlowerShop(string name, string address)
        {
            throw new NotImplementedException();
        }

        public void AddToStock(Flower flower)
        {
            throw new NotImplementedException();
        }

        public void HireEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool FireEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(Customer customer, Bouquet bouquet,
                                 string deliveryAddress, DateTime deliveryDate)
        {
            throw new NotImplementedException();
        }

        public List<Flower> FindFlowers(FlowerType type)
        {
            throw new NotImplementedException();
        }

        public int RemoveStaleFlowers(DateTime today)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalRevenue()
        {
            throw new NotImplementedException();
        }
    }
}