using LongDistancePowerLinesCalsulate.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using WpfApplication.Infrastructure.Base;

namespace WpfApplication.Models
{

    public abstract class ObserverViewModel : IUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Входные параметры.
        /// </summary>
        public abstract InputData InputData { get; set; }

        /// <summary>
        /// Обновление данных.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Событие изменения свойства.ы
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызываемый при изменении свойства.
        /// </summary>
        /// <param name="PropertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Установить значение свойства.
        /// </summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Изменяемое поле.</param>
        /// <param name="value">Значение.</param>
        /// <param name="PropertyName">Имя свойства.</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
