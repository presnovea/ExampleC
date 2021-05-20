using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ExapleForPoint.Modelling;
using ExapleForPoint.ViewModel;

namespace ExapleForPoint
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Только для примера. Статический объект, хранящий контекст сессии EF для работы с данными.
        /// </summary>
        public static ISessionContext sessionContext = new SessionContext();
        public App()
        { }
    }
}
