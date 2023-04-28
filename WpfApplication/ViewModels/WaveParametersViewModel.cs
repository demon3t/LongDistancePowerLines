using LongDistancePowerLinesCalsulate.Classes;
using System.Collections.ObjectModel;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    /// <summary>
    /// Контекст WaveParameters.
    /// </summary>
    public class WaveParametersViewModel : ObserverViewModel
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
        public WaveParametersViewModel()
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
            ViewItems.Add(new DataShell("Средне-геометрическое расстояние между проводами фаз, м",
                InputData.WaveParameters.Single.MeanDistanceBetweenPhaseWires, InputData.WaveParameters.Split.MeanDistanceBetweenPhaseWires));
            ViewItems.Add(new DataShell("Эквивалентный радиус провода, мм",
                InputData.WaveParameters.Single.RadiusWire, InputData.WaveParameters.Split.RadiusWire));
            ViewItems.Add(new DataShell("Индуктивное сопротивление, Ом/км",
                InputData.WaveParameters.Single.ResistanceWire, InputData.WaveParameters.Split.ResistanceWire));
            ViewItems.Add(new DataShell("Емкостная проводимость, См/км",
                InputData.WaveParameters.Single.CapacitiveConductivityWire, InputData.WaveParameters.Split.CapacitiveConductivityWire));
            ViewItems.Add(new DataShell("Комплексное сопротивление, Ом/км",
                InputData.WaveParameters.Single.ComplexResistanceWire, InputData.WaveParameters.Split.ComplexResistanceWire));
            ViewItems.Add(new DataShell("Полная проводимость, См/км",
                InputData.WaveParameters.Single.ComplexConductivityWire, InputData.WaveParameters.Split.ComplexConductivityWire));
            ViewItems.Add(new DataShell("Коэффициент распространения волны, град/км",
                InputData.WaveParameters.Single.WavePropagationCoefficient, InputData.WaveParameters.Split.WavePropagationCoefficient));
            ViewItems.Add(new DataShell("Коэффициент затухания, 1/км",
                InputData.WaveParameters.Single.AttenuatioCoefficient, InputData.WaveParameters.Split.AttenuatioCoefficient));
            ViewItems.Add(new DataShell("Коэффициент фазы, град/км",
                InputData.WaveParameters.Single.PhaseCoefficientDeg, InputData.WaveParameters.Split.PhaseCoefficientDeg));
            ViewItems.Add(new DataShell("Волновое сопротивление, Ом",
                InputData.WaveParameters.Single.WaveResistanceLine, InputData.WaveParameters.Split.WaveResistanceLine));
            ViewItems.Add(new DataShell("Натуральная мощность, МВт",
                InputData.WaveParameters.Single.NaturalPower, InputData.WaveParameters.Split.NaturalPower));
        }
    }
}
