﻿using HandyControl.Themes;
using LongDistancePowerLinesCalsulate.Classes;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfApplication.Infrastructure;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class ReactorViewModel : ObserverViewModel
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
                    Title = "Значение ",
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
                    MarkerType = MarkerType.Circle,
                    Color = OxyColors.Blue,
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
        public PlotModel NaturalPowerGraph { get { return _naturalPowerGraph; } }

        private PlotModel _naturalPowerGraph;

        /// <summary>
        /// График волнового сопротивления.
        /// </summary>
        public PlotModel WaveResistanceGraph { get { return _waveResistanceGraph; } }

        private PlotModel _waveResistanceGraph;

        /// <summary>
        /// График волнового сопротивления.
        /// </summary>
        public PlotModel CornerGraph { get { return _cornerGraph; } }

        private PlotModel _cornerGraph;

        /// <summary>
        /// График A.
        /// </summary>
        public PlotModel AGraph { get { return _aGraph; } }

        private PlotModel _aGraph;

        /// <summary>
        /// График B.
        /// </summary>
        public PlotModel BGraph { get { return _bGraph; } }

        private PlotModel _bGraph;

        /// <summary>
        /// График C.
        /// </summary>
        public PlotModel CGraph { get { return _cGraph; } }

        private PlotModel _cGraph;

        /// <summary>
        /// График D.
        /// </summary>
        public PlotModel DGraph { get { return _dGraph; } }

        private PlotModel _dGraph;

        #endregion

        #endregion

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public ReactorViewModel()
        {
            #region Команды

            ZoomEnableCommand = new RelayCommand(OnZoomEnableCommandExecuted, CanZoomEnableCommandExecute);

            #endregion

            var s = new PlotView();

            InputData = new InputData();
            _naturalPowerGraph = PlotModelPattern;
            _naturalPowerGraph.Title = "Натуральная мощность";
            _naturalPowerGraph.Axes.Last().Title += "натуральной мощности";
            _waveResistanceGraph = PlotModelPattern;
            _waveResistanceGraph.Title = "Волновое сопротивление";
            _waveResistanceGraph.Axes.Last().Title += "волнового сопротивления";
            _cornerGraph = PlotModelPattern;
            _cornerGraph.Title = "Угол";
            _cornerGraph.Axes.Last().Title += "угла";
            _aGraph = PlotModelPattern;
            _aGraph.Title = "A";
            _aGraph.Axes.Last().Title += "A";
            _bGraph = PlotModelPattern;
            _bGraph.Title = "B";
            _bGraph.Axes.Last().Title += "B";
            _cGraph = PlotModelPattern;
            _cGraph.Title = "C";
            _cGraph.Axes.Last().Title += "C";
            _dGraph = PlotModelPattern;
            _dGraph.Title = "D";
            _dGraph.Axes.Last().Title += "D";

            Update();
        }

        public override void Update()
        {
            ((LineSeries)NaturalPowerGraph.Series.First()).Points.Clear();
            ((LineSeries)NaturalPowerGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetPc_Reactor));
            NaturalPowerGraph.InvalidatePlot(true);

            ((LineSeries)WaveResistanceGraph.Series.First()).Points.Clear();
            ((LineSeries)WaveResistanceGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetZw_Reactor));
            WaveResistanceGraph.InvalidatePlot(true);

            ((LineSeries)CornerGraph.Series.First()).Points.Clear();
            ((LineSeries)CornerGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetCorner_Reactor));
            CornerGraph.InvalidatePlot(true);

            ((LineSeries)AGraph.Series.First()).Points.Clear();
            ((LineSeries)AGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetA_Reactor));
            AGraph.InvalidatePlot(true);

            ((LineSeries)BGraph.Series.First()).Points.Clear();
            ((LineSeries)BGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetB_Reactor));
            BGraph.InvalidatePlot(true);

            ((LineSeries)CGraph.Series.First()).Points.Clear();
            ((LineSeries)CGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetC_Reactor));
            CGraph.InvalidatePlot(true);

            ((LineSeries)DGraph.Series.First()).Points.Clear();
            ((LineSeries)DGraph.Series.First()).Points.AddRange(GetPointCollection(InputData.Reactor.GetD_Reactor));
            DGraph.InvalidatePlot(true);
        }

        private IEnumerable<DataPoint> GetPointCollection(Func<int, double> func)
        {
            return Enumerable
                .Range(0, 4)
                .Select(i => new DataPoint(i, func(i)));
        }

        private IEnumerable<DataPoint> GetPointCollection(Func<int, Complex> func)
        {
            return Enumerable
                .Range(0, 4)
                .Select(i => new DataPoint(i, func(i).Magnitude));
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
