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
    /// <summary>
    /// Модель-представление окна Конфигуратора 
    /// </summary>
    class CreatorViewModel : INotifyPropertyChanged
    {
        //--------Объявление свойств, делегатов и событий----------
        private DbParams dbParams;
        private DataBaseWorker dbWorker;
        internal  PointExampleContext exampleContext;
        private DataBaseFiller dbFiller;
        private ISessionContext currentSessionContext;

        private int customerValue = 10, orderValue = 1000;
        private bool isSldrEnabled, connInProcess;
        private string processBox;
        

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
        public CreatorViewModel(ISessionContext sessionContext)
        {
            currentSessionContext = sessionContext;
            dbParams = SetDbPrmDefault();
            ConfigureDBCommand = new DelegateCommand(ExecuteConfigureDb, CanExecuteConfigureDb);
            FillDBCommand = new DelegateCommand(ExecuteFillDB, CanExecuteFillDB);
            connInProcess = true;
        }

        //--------- Свойства, к которым происходит привязка View ----------------------
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
        public int CustomerValue
        {
            get { return (int)customerValue; }
            set { customerValue = value;
                OnPropertyChanged("CustomerValue"); }
        }
        public int OrderValue
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
        public bool ConnectionInProcess
        {
            get
            {return connInProcess;}
            set
            {
                connInProcess = !value;
                OnPropertyChanged("ConnectionInProcess");
            }
        }
        
        public string ProcessBox
        {
            get
            { return processBox; }
            set
            {
                processBox = value;
                OnPropertyChanged("ProcessBox");
            }
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
            dbParams.Port = "4567";
            dbParams.DbName = "PointDb";
            dbParams.UserID = "sa";
            dbParams.Password = "";
            return dbParams;
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Обрабатывает нажатие кнопки "Подключить".
        /// </summary>
        private async void ExecuteConfigureDb(object param)
        {
            ConnectionInProcess = true;
            ProcessBox = "Установка соединения с БД";
            dbWorker = new DataBaseWorker();
            try { await Task.Run(() => { exampleContext = dbWorker.ConfigDb(dbParams); }); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            OnPropertyChanged("ConnectionEnabled");
            ConnectionInProcess = false;
            ProcessBox = "Соединение с БД установлено";
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Реализует проверку возможности выполнения метода ExecuteConfigureDb.
        /// </summary>
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
        /// Обрабатывает нажатие кнопки "Заполнить".
        /// </summary>
        private async void ExecuteFillDB(object param)
        {
            ProcessBox = "Наполнение БД...";
            dbFiller = new DataBaseFiller(exampleContext);
            try
            {
                await Task.Run(() =>
                 {
                     dbFiller.SetCustomers(CustomerValue);
                     dbFiller.SetOders(CustomerValue, OrderValue);
                 });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //ObserverViewModel.ExampleContext = exampleContext;

            currentSessionContext.SessionExampleContext = exampleContext;
            OnPropertyChanged("ConnectionEnabled");
            ProcessBox = "БД наполнена данными";
        }

        /// <summary>
        /// Метод, реализующий требования интерфейса ICommand.
        /// Реализует проверку возможности выполнения метода ExecuteFillDB.
        /// </summary>
        private bool CanExecuteFillDB(object param)
        {
            //Должна присутствовать проверка полей количества клиентов и заказов.
            //В примере есть начальное значение, в проверке нет необходимости.
                return true;
        }

    }    
}
