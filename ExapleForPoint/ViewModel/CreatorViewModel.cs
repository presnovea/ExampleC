using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Security;

using ExapleForPoint.Modelling;

namespace ExapleForPoint.ViewModel
{
    class CreatorViewModel : INotifyPropertyChanged
    {
        private DbParams dbParams;
        private decimal customerValue = 10, orderValue = 1000;
        private bool isSldrEnabled;

        private DataBaseWorker dbWorker;
        private PointExampleContext exampleContext;



        //--------Объявления свойств и событий----------
        public IDelegateCommand ConfigureDBCommand { protected set; get; }
        public IDelegateCommand FillDBCommand { protected set; get; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CreatorViewModel()
        {
            dbParams = SetDbPrmDefault();
            ConfigureDBCommand = new DelegateCommand(ExecuteConfigureDb, CanExecuteConfigureDb);
            FillDBCommand = new DelegateCommand(ExecuteFillDB, CanExecuteFillDB);
        }

        //---------Геттеры и сеттеры----------------------
        public DbParams DBaseParams
        {
            get { return dbParams; }
            set
            {
                dbParams = value;
                OnPropertyChanged("DBaseParams");
            }
        }

        public String SecurePassword
        {
            private get { return ""; }
            set
            {
                dbParams.Password = value;
            }
        }
    public decimal CustomerValue
        {
            get { return (int)customerValue; }
            set { customerValue = value;
                OnPropertyChanged("CustomerValue"); }
        }
        public decimal OrderValue
        {
            get { return (int)orderValue; }
            set
            { orderValue = value;
                OnPropertyChanged("OrderValue"); }
        }

        public bool ConnectionEnabled
        {
            get {
                if (dbWorker != null)
                    return isSldrEnabled = true;
                else
                    return isSldrEnabled = false;
                    }
            set
            {   isSldrEnabled = value;
                OnPropertyChanged("ConnectionEnabled"); }
        }

        //----------Методы--------------------------------
        /// <summary>
        /// Метод дефолтного заполнения данными для строки подключения.
        /// Не обязательный, создан для удобства
        /// </summary>
        /// <returns></returns>
        internal DbParams SetDbPrmDefault()
        {
            dbParams = new DbParams();
            dbParams.DataSource = "127.0.0.1";
            dbParams.Port = "49172";
            dbParams.DbName = "PointDb";
            dbParams.UserID = "sa";
            dbParams.Password = "";
            return dbParams;
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Обрабатывает нажатие кнопкуи "Подключить".
        /// </summary>
        private void ExecuteConfigureDb(object param)
        {

            dbWorker = new DataBaseWorker();
            exampleContext = dbWorker.ConfigDb(dbParams);

            OnPropertyChanged("ConnectionEnabled");
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Реализует проверку возможности выполнения метода ExecuteConfigureDb.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool CanExecuteConfigureDb(object param)
        {
            if (dbParams.DataSource != "" &&
                dbParams.DbName != "" &&
                dbParams.Port != "" &&
                dbParams.UserID != "")
                return true;
            else
            {
                MessageBox.Show("Один из элементов подключения заполнен не правильно/не заполнен.");
                return false;
            }
        }


        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Обрабатывает нажатие кнопкуи "Заполнить".
        /// </summary>
        private void ExecuteFillDB(object param)
        {
            OnPropertyChanged("ConnectionEnabled");
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Реализует проверку возможности выполнения метода ExecuteFillDB.
        /// </summary>
        private bool CanExecuteFillDB(object param)
        {
                return false;
        }

    }    
}
