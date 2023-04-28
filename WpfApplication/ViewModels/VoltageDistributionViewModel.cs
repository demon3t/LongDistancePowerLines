using LongDistancePowerLinesCalsulate.Classes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication.Infrastructure;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class VoltageDistributionViewModel : ObserverViewModel
    {
        /// <summary>
        /// Шаблон графика.
        /// </summary>
        private PlotModel PlotModelPattern => new()
        {
            TextColor = OxyColor.FromArgb(255, 50, 109, 242),
            PlotAreaBorderColor = OxyColor.FromArgb(255, 50, 109, 242),
            Axes =
            {
                new LinearAxis
                {
                    Title = "Степень компенсации",
                    TitleColor = OxyColors.Transparent,
                    MinorTicklineColor = OxyColor.FromArgb(255, 50,109, 242),
                    TicklineColor = OxyColor.FromArgb(255, 50,109, 242),
                    Position = AxisPosition.Bottom,
                    IsZoomEnabled = false,
                    MajorGridlineThickness = 1,
                    MajorGridlineStyle = LineStyle.Solid,
                    MajorGridlineColor = OxyColors.LightBlue,
                },
                new LinearAxis
                {
                    Title = "Напряжение",
                    TitleColor = OxyColors.Transparent,
                    MinorTicklineColor =OxyColor.FromArgb(255, 50, 109, 242),
                    TicklineColor = OxyColor.FromArgb(255, 50,109, 242),
                    Position = AxisPosition.Left,
                    IsZoomEnabled = false,
                    MajorGridlineThickness = 1,
                    MajorGridlineStyle = LineStyle.Solid,
                    MajorGridlineColor = OxyColors.LightBlue,
                },
            },
            Series =
            {
                new LineSeries
                {
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Blue,
                    LineStyle = LineStyle.Solid,
                    BrokenLineThickness = 3,
                    RenderInLegend = false,
                },
                new LineSeries
                {
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Red,
                    LineStyle = LineStyle.Solid,
                    BrokenLineThickness = 3,
                    RenderInLegend = false,
                },
                new LineSeries
                {
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Violet,
                    LineStyle = LineStyle.Solid,
                    BrokenLineThickness = 3,
                    RenderInLegend = false,
                },
                new LineSeries
                {
                    MarkerType = MarkerType.None,
                    Color = OxyColors.Green,
                    LineStyle = LineStyle.Solid,
                    BrokenLineThickness = 3,
                    RenderInLegend = false,
                },
            },

        };

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
        /// График натуральной мощности.
        /// </summary>
        public PlotModel VoltageDistribution { get { return _voltageDistribution; } }

        private PlotModel _voltageDistribution;

        #endregion

        #endregion

        public VoltageDistributionViewModel()
        {
            #region Команды

            ZoomEnableCommand = new RelayCommand(OnZoomEnableCommandExecuted, CanZoomEnableCommandExecute);

            #endregion

            var s = new PlotView();

            InputData = new InputData();
            _voltageDistribution = PlotModelPattern;
            _voltageDistribution.Title = "Распределение напряжения";

            Update();
        }

        public override void Update()
        {
            //doto: Что-то не считает, проверить 
            ((LineSeries)VoltageDistribution.Series[0]).Points.Clear();
            ((LineSeries)VoltageDistribution.Series[0]).Points.AddRange(GetPointCollection(InputData.VoltageDistribution.GetVoltageCollection_OneSided));

            ((LineSeries)VoltageDistribution.Series[1]).Points.Clear();
            ((LineSeries)VoltageDistribution.Series[1]).Points.AddRange(GetPointCollection(InputData.VoltageDistribution.GetVoltageCollection_LessNaturalPower));

            ((LineSeries)VoltageDistribution.Series[2]).Points.Clear();
            ((LineSeries)VoltageDistribution.Series[2]).Points.AddRange(GetPointCollection(InputData.VoltageDistribution.GetVoltageCollection_NaturalPower));

            ((LineSeries)VoltageDistribution.Series[3]).Points.Clear();
            ((LineSeries)VoltageDistribution.Series[3]).Points.AddRange(GetPointCollection(InputData.VoltageDistribution.GetVoltageCollection_MoreNaturalPower));

            VoltageDistribution.InvalidatePlot(true);
        }

        private IEnumerable<DataPoint> GetPointCollection(Func<IEnumerable<(double, double)>> func)
        {
            foreach (var item in func())
            {
                yield return new DataPoint(item.Item1, item.Item2);
            }

            yield break;
        }

        #region Команды

        #region Разрешить приближение

        public ICommand ZoomEnableCommand { get; }

        private bool CanZoomEnableCommandExecute(object p)
        {
            if (p is PlotView plotView)
            {
                return plotView.Model.Axes is not null;
            }

            return false;
        }

        private void OnZoomEnableCommandExecuted(object p)
        {
            if (p is PlotView plotView)
            {
                foreach (var axis in plotView.Model.Axes)
                {
                    axis.IsZoomEnabled = !axis.IsZoomEnabled;
                }
            }
        }

        #endregion

        #endregion
    }
}
