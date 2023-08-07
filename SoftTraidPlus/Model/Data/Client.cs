using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string StatusName
        {
            get
            {
                if (Status == ClientStatus.OrdinaryClient)
                {
                    return "Обычный";
                }
                else return "Ключевой";
            }
        }
        public ClientStatus Status { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
    }
}
