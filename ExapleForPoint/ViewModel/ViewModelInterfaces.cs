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
    /// Класс, определющий контекст сессии.
    /// Хранит "общие" объекты, используемые View-model совместно.
    /// </summary>
    public class SessionContext : ISessionContext
    {
        private PointExampleContext sessionExampleContext;
        public event PropertyChangedEventHandler PropertyChanged;

        public PointExampleContext SessionExampleContext
        {
            get { return sessionExampleContext; }
            set
            {
                sessionExampleContext = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SessionExampleContext"));
            }
        }
    }

    /// <summary>
    /// Интерфейс, определяющий контекст общей сессии
    /// </summary>
    public interface ISessionContext : INotifyPropertyChanged
    {
        PointExampleContext SessionExampleContext { get; set; }
    }
}
