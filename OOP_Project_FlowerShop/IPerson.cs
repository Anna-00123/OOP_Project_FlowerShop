namespace FlowerShopApp
{
    public interface IPerson
    {
        string Name { get; set; }
        string Phone { get; set; }

        string GetContactInfo();
    }
}