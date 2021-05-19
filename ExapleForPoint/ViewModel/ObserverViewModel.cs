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
    class ObserverViewModel : INotifyPropertyChanged
    {
        //--------Объявление свойств и событий----------
        internal static PointExampleContext exampleContext;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObserverViewModel()
        {

        }
        //---------Геттеры и сеттеры----------------------

        public PointExampleContext MainExampleContext
        {
            get { return exampleContext; }
            set 
            { 
                exampleContext = value;
                OnPropertyChanged("MainExampleContext");
            }
            
        }


        //----------Методы--------------------------------
    }
}
