namespace UnitTestingMinimalApi.Models
{
    public class Player
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool ContractExpired { get; set; } = false;
    }
}
