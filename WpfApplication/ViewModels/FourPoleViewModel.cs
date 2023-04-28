using LongDistancePowerLinesCalsulate.Classes;
using LongDistancePowerLinesCalsulate.Classes.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Infrastructure.Base;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class FourPoleViewModel : ObserverViewModel
    {
        #region Свойства

        #region Входные данные

        /// <summary>
        /// Входные данные.
        /// </summary>
        public override InputData InputData
        {
            get { return _inputData; }
            set { Set(ref _inputData, value); }
        }

        private InputData _inputData;

        #endregion

        #region Отображаемая коллекция

        /// <summary>
        /// Отображаемая коллекция.
        /// </summary>
        public ObservableCollection<DataShell> ViewItems
        {
            get { return _viewItems; }
            set { Set(ref _viewItems, value); }
        }

        private ObservableCollection<DataShell> _viewItems;

        #endregion

        #endregion

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public FourPoleViewModel()
        {
            InputData = new InputData();
            ViewItems = new ObservableCollection<DataShell>();
            Update();
        }

        /// <summary>
        /// Обновление данных страницы.
        /// </summary>
        public override void Update()
        {
            ViewItems.Clear();
            ViewItems.Add(new DataShell("A",
                InputData.FourPole.A, InputData.FourPole.Azero));
            ViewItems.Add(new DataShell("B, Ом",
                InputData.FourPole.B, InputData.FourPole.Bzero));
            ViewItems.Add(new DataShell("C, См",
                InputData.FourPole.C, InputData.FourPole.Czero));
            ViewItems.Add(new DataShell("D",
                InputData.FourPole.D, InputData.FourPole.Dzero));
        }
    }
}
