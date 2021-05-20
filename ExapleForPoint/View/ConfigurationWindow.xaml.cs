using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ExapleForPoint.ViewModel;

namespace ExapleForPoint.View
{
    /// <summary>
    /// Логика взаимодействия для ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            //Присвоение контекста данных для окна
            DataContext = new CreatorViewModel(App.sessionContext);
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события смены пароля PasswordBox
        /// В реальной жизни так делать нельзя и необходимо использовать SecureString, но в ConnectuionString 
        /// пароль все равно входит типом String
        /// </summary>

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext !=null)
            {
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
