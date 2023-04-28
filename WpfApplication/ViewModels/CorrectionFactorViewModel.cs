using LongDistancePowerLinesCalsulate.Classes.Base;
using LongDistancePowerLinesCalsulate.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Infrastructure.Base;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class CorrectionFactorViewModel : ObserverViewModel
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
        public ObservableCollection<DataShell> ViewItemsBefore
        {
            get { return _viewItemsBefore; }
            set { Set(ref _viewItemsBefore, value); }
        }

        private ObservableCollection<DataShell> _viewItemsBefore;

        /// <summary>
        /// Отображаемая коллекция.
        /// </summary>
        public ObservableCollection<DataShell> ViewItemsAfter
        {
            get { return _viewItemsAfter; }
            set { Set(ref _viewItemsAfter, value); }
        }

        private ObservableCollection<DataShell> _viewItemsAfter;

        #endregion

        #endregion

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public CorrectionFactorViewModel()
        {
            InputData = new InputData();
            _viewItemsBefore = new ObservableCollection<DataShell>();
            _viewItemsAfter = new ObservableCollection<DataShell>();
            Update();
        }

        /// <summary>
        /// Обновление данных страницы.
        /// </summary>
        public override void Update()
        {
            ViewItemsBefore.Clear();
            ViewItemsBefore.Add(new DataShell("По активному сопротивлению",
                InputData.CorrectionFactor.FactorActiveResistance));
            ViewItemsBefore.Add(new DataShell("По индуктивному сопротивлению",
                InputData.CorrectionFactor.FactorCapacitiveResistance));
            ViewItemsBefore.Add(new DataShell("По ёмкостному сопротивлению",
                InputData.CorrectionFactor.FactorCapacitiveConductivity));
            ViewItemsBefore.Add(new DataShell("Активное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeActiveResistance));
            ViewItemsBefore.Add(new DataShell("Индуктивное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeCapacitiveResistance));
            ViewItemsBefore.Add(new DataShell("Емкостное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeCapacitiveConductivity));
            ViewItemsBefore.Add(new DataShell("Суммарная проводимость компенсирующих реакторов",
                InputData.CorrectionFactor.TotalConductivityReactor));
            ViewItemsBefore.Add(new DataShell("Номинальная мощность реактора",
                InputData.CorrectionFactor.NominalPowerOneReactor));

            ViewItemsAfter.Clear();
            ViewItemsAfter.Add(new DataShell("По активному сопротивлению",
                InputData.CorrectionFactor.FactorActiveResistanceAfter));
            ViewItemsAfter.Add(new DataShell("По индуктивному сопротивлению",
                InputData.CorrectionFactor.FactorCapacitiveResistanceAfter));
            ViewItemsAfter.Add(new DataShell("По ёмкостному сопротивлению",
                InputData.CorrectionFactor.FactorCapacitiveConductivityAfter));
            ViewItemsAfter.Add(new DataShell("Активное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeActiveResistanceAfter));
            ViewItemsAfter.Add(new DataShell("Индуктивное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeCapacitiveResistanceAfter));
            ViewItemsAfter.Add(new DataShell("Емкостное сопротивление схемы замещения",
                InputData.CorrectionFactor.PShemeCapacitiveConductivityAfter));
        }
    }
}
