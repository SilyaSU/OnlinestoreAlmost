using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OnlineStore.models;

namespace OnlineStore
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            ProductListViewModel productListViewModel = new ProductListViewModel();
            this.DataContext = productListViewModel;

            productListViewModel.LoadProducts();
        }
    }
}
