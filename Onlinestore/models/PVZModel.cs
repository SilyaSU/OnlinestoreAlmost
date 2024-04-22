using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.models
{
    public class PVZModel : INotifyPropertyChanged
    {
        public int PVZModelID { get; set; }
        public int PVZModelRating { get; set; }
        public string PVZModelSettlement { get; set; }
        public string PVZModelStreet { get; set; }
        public string PVZModelHouse { get; set; }

        public int PVZModelOrderQuantity { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
