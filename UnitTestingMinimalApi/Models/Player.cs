namespace UnitTestingMinimalApi.Models
{
    public class Player
    {
        public Guid ID { get; private set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public int Age { get; set; }
        public bool ContractExpired { get; set; } = false;
    }
}
