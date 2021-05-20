using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using ExapleForPoint.Modelling;

namespace ExapleForPoint.ViewModel
{
    /// <summary>
    /// Класс, реализующий интерфейс IDelegateCommand
    /// С помощью делегатов абстрагирует выполнение необходимых методов Execute, AlwaysCanExecute и CanExecute.
    /// Дает возможность создания различных команд для ViewModel. 
    /// В данном случае нужен для назначения поведения кнопок в модели MVVM
    /// </summary>
    public class DelegateCommand : IDelegateCommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Констркутор класса с обеспечением проверки возможности выполнения метода Execute 
        /// </summary>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// ККонстркутор класса без обеспечения проверки возможности выполнения метода Execute
        /// </summary>
        /// <param name="execute"></param>
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = this.AlwaysCanExecute;
        }

        /// <summary>
        /// Метод, необходимый для реализации ICommand
        /// </summary>
        /// <param name="param"></param>
        public void Execute(object param)
        { execute(param); }

        /// <summary>
        /// Метод, необходимый для реализации ICommand
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool CanExecute(object param)
        { return canExecute(param); }

        // Метод, необходимый для IDelegateCommand
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Метод CanExecute по умолчанию
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool AlwaysCanExecute(object param)
        { return true; }

    }

    //----------------------------------Интрефейс------------------------

    /// <summary>
    /// Интерфейс, делегирующий реализацию интерфейса ICommand
    /// </summary>
    public interface IDelegateCommand : ICommand
    {
        /// <summary>
        /// Метод, инициирующий событие CanExecuteChanged
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
