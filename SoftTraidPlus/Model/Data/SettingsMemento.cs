using System.Runtime.Serialization;

namespace SoftTradePlus.Model.Data
{
    [DataContract]
    public class SettingsMemento
    {
        [DataMember(Name = "isAuthorized")]
        public bool IsAuthorized { get; set; }
        [DataMember(Name = "currentUser")]
        public Manager? CurrentUser { get; set; }
        public SettingsMemento()
        {
            IsAuthorized = false;
        }
    }
}
