using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.models
{
    internal class AddItem : INotifyPropertyChanged
    {
        string NewItemTitle { get; set; }

        string NewItemCompany { get; set; }

        int NewItemPrice { get; set; }

        int NewItemQuantity { get; set; }

        string NewItemDescription { get; set; }

        int NewItemScore { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
