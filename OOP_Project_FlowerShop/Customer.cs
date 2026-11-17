namespace FlowerShopApp
{
    public class Customer : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int BonusPoints { get; private set; }

        public Customer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }

        public string GetContactInfo()
        {
            throw new NotImplementedException();
        }

        public void AddBonus(decimal orderSum)
        {
            throw new NotImplementedException();
        }

        public bool UseBonus(int points)
        {
            throw new NotImplementedException();
        }
    }
}