using System.Runtime.Serialization;

namespace SoftTradePlus.Model
{
    public class SettingsMemento
    {
        [DataMember (Name = "isAuthorized")]
        public bool IsAuthorized { get; private set; }
        [DataMember(Name = "currentUser")]
        public Manager CurrentUser {  get; private set; }
        public SettingsMemento()
        {
            IsAuthorized = false;
        }
    }
}
