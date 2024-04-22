using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OnlineStore
{
    /// <summary>
    /// Логика взаимодействия для AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow(ProductListViewModel productListViewModel)
        {
            InitializeComponent();

            AddItemModel addItemModel = new AddItemModel(productListViewModel);
            this.DataContext = addItemModel;

            addItemModel.Refresh += () =>
            {
                productListViewModel.LoadProducts();
            };
        }
    }
}
