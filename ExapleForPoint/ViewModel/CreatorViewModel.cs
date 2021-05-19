using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using ExapleForPoint.Modelling;

namespace ExapleForPoint.ViewModel
{
    class CreatorViewModel : INotifyPropertyChanged
    {
        private DbParams dbParams;
        private decimal customerValue, orderValue;

        private DataBaseWorker dbWorker;



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

        public decimal CustomerValue
        {
            get { return customerValue; }
            set { customerValue = value;
                OnPropertyChanged("CustomerValue"); }
        }
            



        //----------Методы--------------------------------
        internal DbParams SetDbPrmDefault()
        {
            dbParams = new DbParams();
            dbParams.DataSource = "127.0.0.1";
            dbParams.Port = "49172";
            dbParams.DbName = "PointDb";
            dbParams.UserID = "sa";
            dbParams.Password = "default";
            return dbParams;
        }



        //public ICommand COnfigureDB (DbParams prms)
        //{
        //    int i = 0;
        //}

        private void ExecuteConfigureDb(object param)
        {
            var i = dbParams;
        }

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
    }    
}
