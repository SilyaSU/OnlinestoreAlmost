using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.models
{
    public class Product : INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public string ProductProducer { get; set; }

        public string ProductDescription { get; set; }

        public int ProductQuantity { get; set; }

        public int ProductPrice { get; set; }

        public int ProductScore { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
