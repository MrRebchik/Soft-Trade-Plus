namespace SoftTradePlus.Model.Data
{
    public enum ClientStatus
    {
        OrdinaryClient = 0,
        CrucialClient = 1
    }
    public class Client
    {
        public int Id { get; set; }
        public ClientStatus Status { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
    }
}
