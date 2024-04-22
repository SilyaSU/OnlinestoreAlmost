using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.models;
using System.Data.SqlClient;
using System.Windows.Input;
using System.ComponentModel;

namespace OnlineStore
{
    internal class AddItemModel : INotifyPropertyChanged
    {
        public ICommand AddNewItemCommand { get; private set; }
        public event Action Refresh;
        private ProductListViewModel productListViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddItemModel(ProductListViewModel productListViewModel)
        {
            this.productListViewModel = productListViewModel;
            NewItemTitle = "";
            NewItemCompany = "";
            NewItemPrice = 0;
            NewItemQuantity = 0;
            NewItemDescription = "";
            NewItemScore = 0;

            AddNewItemCommand = new RelayCommand(AddNewItem);

            this.Refresh += () =>
            {
                productListViewModel.LoadProducts();
            };
        }

        private void AddNewItem(object parameter)
        {
            Product newProduct = new Product
            {
                ProductName = NewItemTitle,
                ProductProducer = NewItemCompany,
                ProductPrice = NewItemPrice,
                ProductQuantity = NewItemQuantity,
                ProductDescription = NewItemDescription,
                ProductScore = NewItemScore
            };

            DataBase db = new DataBase();
            db.openConnection();

            string query = @"INSERT INTO Товары (Наименование_товара, Описание, Цена, КоличествоТоваров, Рейтинг, Fk_Продавцы) 
                     VALUES (@Name, @Description, @Price, @Quantity, @Score, @FK_Company)";

            SqlCommand command = new SqlCommand(query, db.sqlConnection);

            command.Parameters.AddWithValue("@Name", newProduct.ProductName);
            command.Parameters.AddWithValue("@FK_Company", newProduct.ProductProducer);
            command.Parameters.AddWithValue("@Description", newProduct.ProductDescription);
            command.Parameters.AddWithValue("@Price", newProduct.ProductPrice);
            command.Parameters.AddWithValue("@Quantity", newProduct.ProductQuantity);
            command.Parameters.AddWithValue("@Score", newProduct.ProductScore);

            command.ExecuteNonQuery();

            db.closeConnection();

            Refresh?.Invoke();

            productListViewModel.LoadProducts();
        }


        private string newItemTitle;
        public string NewItemTitle
        {
            get { return newItemTitle; }
            set
            {
                newItemTitle = value;
                OnPropertyChanged(nameof(NewItemTitle));
            }
        }

        private string newItemCompany;
        public string NewItemCompany
        {
            get { return newItemCompany; }
            set
            {
                newItemCompany = value;
                OnPropertyChanged(nameof(NewItemCompany));
            }
        }

        private int newItemPrice;
        public int NewItemPrice
        {
            get { return newItemPrice; }
            set
            {
                newItemPrice = value;
                OnPropertyChanged(nameof(NewItemPrice));
            }
        }


        private int newItemQuantity;
        public int NewItemQuantity
        {
            get { return newItemQuantity; }
            set
            {
                newItemQuantity = value;
                OnPropertyChanged(nameof(NewItemQuantity));
            }
        }


        private string newItemDescription;
        public string NewItemDescription
        {
            get { return newItemDescription; }
            set
            {
                newItemDescription = value;
                OnPropertyChanged(nameof(NewItemDescription));
            }
        }

        

        private int newItemScore;
        public int NewItemScore
        {
            get { return newItemScore; }
            set
            {
                newItemScore = value;
                OnPropertyChanged(nameof(NewItemScore));
            }
        }

      


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
