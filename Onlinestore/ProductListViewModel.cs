using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OnlineStore.models;
using System.Data.SqlClient;


namespace OnlineStore
{
    public class ProductListViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> SortingOptions { get; set; }
        private ObservableCollection<Product> products;

        public ICommand ApplyFilterCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand OpenAddItemWindowCommand { get; private set; }

        public ICommand RefreshCommand { get; private set; }

        public ICommand RemoveProductCommand { get; private set; }

        public ICommand GoToPVZCommand { get; private set; }

        private int productID;

        public int ProductID
        {
            get { return productID; }
            set
            {
                productID = value;
                OnPropertyChanged(nameof(ProductID));
            }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string productProducer;
        public string ProductProducer
        {
            get { return productProducer; }
            set
            {
                productProducer = value;
                OnPropertyChanged(nameof(ProductProducer));
            }
        }

        private string productDescription;
        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }


        private int productQuantity;
        public int ProductQuantity
        {
            get { return productQuantity; }
            set
            {
                productQuantity = value;
                OnPropertyChanged(nameof(ProductQuantity));
            }
        }


        private int productPrice;
        public int ProductPrice
        {
            get { return productPrice; }
            set
            {
                productPrice = value;
                OnPropertyChanged(nameof(ProductPrice));
            }
        }

        private int productScore;
        public int ProductScore
        {
            get { return productScore; }
            set
            {
                productScore = value;
                OnPropertyChanged(nameof(ProductScore));
            }
        }




        public ProductListViewModel()
        {
            SortingOptions = new ObservableCollection<string>
            {
                "По цене",
                "Ещё как-нибудь"
            };

            OpenAddItemWindowCommand = new RelayCommand(OpenAddItemWindow);
            RefreshCommand = new RelayCommand(RefreshProducts);
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            GoToPVZCommand = new RelayCommand(OpenPVZWindow);

            Products = new ObservableCollection<Product>();
            LoadProducts();
        }

        private void OpenPVZWindow(object parameter)
        {
            PVZ pvzWindow = new PVZ();
            pvzWindow.DataContext = new PVAViewModel();
            pvzWindow.Show();
        }

        private void RemoveProduct(object parameter)
        {
            if (parameter is Product productToRemove)
            {
                DataBase db = new DataBase();
                db.openConnection();

                string query = "DELETE FROM Товары WHERE Id_товара = @ProductId";
                SqlCommand command = new SqlCommand(query, db.sqlConnection);
                command.Parameters.AddWithValue("@ProductId", productToRemove.ProductID);

                command.ExecuteNonQuery();

                db.closeConnection();

                LoadProducts();
            }
        }

        private void OpenAddItemWindow(object parameter)
        {
            AddItemWindow addItemWindow = new AddItemWindow(this);
            addItemWindow.ShowDialog();
        }

        private void RefreshProducts(object parameter)
        {
            LoadProducts();
        }

        private ObservableCollection<Product> currentProducts;
        public ObservableCollection<Product> CurrentProducts
        {
            get { return currentProducts; }
            set
            {
                currentProducts = value;
                OnPropertyChanged(nameof(CurrentProducts));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }



        public void LoadProducts()
        {
            DataBase db = new DataBase();
            db.openConnection();

            string query = "SELECT Товары.Id_товара, Товары.Наименование_товара, Товары.Описание, Товары.Цена, Товары.КоличествоТоваров, Товары.Рейтинг, Продавцы.наименование_продавца " +
                           "FROM Товары INNER JOIN Продавцы ON Товары.Fk_Продавцы = Продавцы.Id_продавца";



            System.Data.SqlClient.SqlDataReader reader = db.ExecuteQuery(query);

            ObservableCollection<Product> productsList = new ObservableCollection<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    ProductDescription = reader.GetString(2),
                    ProductPrice = reader.GetInt32(3),
                    ProductQuantity = reader.GetInt32(4),
                    ProductScore = reader.GetInt32(5),
                    ProductProducer = reader.GetString(6),
                };

                productsList.Add(product);
            }

            Products = productsList;

            db.closeConnection();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
