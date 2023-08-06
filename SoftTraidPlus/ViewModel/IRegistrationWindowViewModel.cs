using SoftTradePlus.Model.Data;
using System.Collections.Generic;

namespace SoftTradePlus.ViewModel
{
    public interface IRegistrationWindowViewModel
    {
        public List<Manager> AllManagers { get; set; }
        public static Manager SelectedManager { get; set; }
    }
}
