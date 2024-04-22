using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OnlineStore.models;
using System.Windows;
using System.ComponentModel;


namespace OnlineStore
{
    internal class PVAViewModel : INotifyPropertyChanged
    {

        public ICommand GoToPVZCommand { get; private set; }
        public ICommand GoToProductsCommand { get; private set; }

        public PVAViewModel()
        {
            GoToPVZCommand = new RelayCommand(OpenPVZWindow);
            GoToProductsCommand = new RelayCommand(OpenProductsWindow);
            LoadPVZList();
        }

        private void OpenPVZWindow(object parameter)
        {
            PVZ pvzWindow = new PVZ();
            pvzWindow.DataContext = new PVAViewModel();
            pvzWindow.Show();
            CloseCurrentWindow(parameter as Window);
        }

        private void OpenProductsWindow(object parameter)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new ProductListViewModel();
            mainWindow.Show();
            CloseCurrentWindow(parameter as Window);
        }


        private void CloseCurrentWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private double pvzModelRating;
        public double PVZModelRating
        {
            get { return pvzModelRating; }
            set
            {
                if (pvzModelRating != value)
                {
                    pvzModelRating = value;
                    OnPropertyChanged(nameof(PVZModelRating));
                }
            }
        }

        private string pvzModelSettlement;
        public string PVZModelSettlement
        {
            get { return pvzModelSettlement; }
            set
            {
                if (pvzModelSettlement != value)
                {
                    pvzModelSettlement = value;
                    OnPropertyChanged(nameof(PVZModelSettlement));
                }
            }
        }

        private string pvzModelStreet;
        public string PVZModelStreet
        {
            get { return pvzModelStreet; }
            set
            {
                if (pvzModelStreet != value)
                {
                    pvzModelStreet = value;
                    OnPropertyChanged(nameof(PVZModelStreet));
                }
            }
        }

        private string pvzModelHouse;
        public string PVZModelHouse
        {
            get { return pvzModelHouse; }
            set
            {
                if (pvzModelHouse != value)
                {
                    pvzModelHouse = value;
                    OnPropertyChanged(nameof(PVZModelHouse));
                }
            }
        }

        private int pvzModelOrderQuantity;
        public int PVZModelOrderQuantity
        {
            get { return pvzModelOrderQuantity; }
            set
            {
                if (pvzModelOrderQuantity != value)
                {
                    pvzModelOrderQuantity = value;
                    OnPropertyChanged(nameof(PVZModelOrderQuantity));
                }
            }
        }
        


        private ObservableCollection<string> pvzsortingOptions;


        public ObservableCollection<string> PVZSortingOptions
        {
            get => pvzsortingOptions;
            set
            {
                pvzsortingOptions = value;
                OnPropertyChanged(nameof(PVZSortingOptions));
            }
        }

        private string pvzselectedSortingOption;

        public string PVZSelectedSortingOption
        {
            get => pvzselectedSortingOption;
            set
            {
                pvzselectedSortingOption = value;
                OnPropertyChanged(nameof(PVZSelectedSortingOption));
            }
        }

        private ObservableCollection<PVZModel> pvzList;
        public ObservableCollection<PVZModel> PVZList
        {
            get => pvzList;
            set
            {
                pvzList = value;
                OnPropertyChanged(nameof(PVZList));
            }
        }

        public void LoadPVZList()
        {
            DataBase db = new DataBase();
            db.openConnection();

            string query = @"SELECT 
                        ПунктыВыдачи.id_пункта_выдачи, 
                        ПунктыВыдачи.Рейтинг, 
                        ПунктыВыдачи.НаселенныйПункт, 
                        ПунктыВыдачи.Улица, 
                        ПунктыВыдачи.Дом, 
                        SUM(Товары_В_Заказе.количество) AS КоличествоТоваров 
                    FROM 
                        ПунктыВыдачи 
                    LEFT JOIN 
                        Товары_В_Заказе ON ПунктыВыдачи.id_пункта_выдачи = Товары_В_Заказе.Fk_ПунктыВыдачи 
                    GROUP BY 
                        ПунктыВыдачи.id_пункта_выдачи, 
                        ПунктыВыдачи.Рейтинг, 
                        ПунктыВыдачи.НаселенныйПункт, 
                        ПунктыВыдачи.Улица, 
                        ПунктыВыдачи.Дом";

            var reader = db.ExecuteQuery(query);

            PVZList = new ObservableCollection<PVZModel>();

            while (reader.Read())
            {
                PVZModel pvz = new PVZModel
                {
                    PVZModelID = reader.GetInt32(0),
                    PVZModelRating = reader.GetInt32(1),
                    PVZModelSettlement = reader.GetString(2),
                    PVZModelStreet = reader.GetString(3),
                    PVZModelHouse = reader.GetString(4),
                    PVZModelOrderQuantity = reader.IsDBNull(5) ? 0 : reader.GetInt32(5)
                };
                PVZList.Add(pvz);
            }

            db.closeConnection();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
