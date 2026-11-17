namespace FlowerShopApp
{
    public class Employee : IPerson
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, string phone, EmployeePosition position, decimal salary)
        {
            throw new NotImplementedException();
        }

        public string GetContactInfo()
        {
            throw new NotImplementedException();
        }

        public void RaiseSalary(int percent)
        {
            throw new NotImplementedException();
        }
    }
}