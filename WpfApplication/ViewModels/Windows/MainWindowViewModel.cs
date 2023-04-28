using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using LongDistancePowerLinesCalsulate.Classes;
using LongDistancePowerLinesCalsulate.Classes.Base;
using LongDistancePowerLinesCalsulate.Classes.OutputData;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApplication.Infrastructure;
using WpfApplication.Infrastructure.Base;
using WpfApplication.Models;
using WpfApplication.Views;
using CorrectionFactor = WpfApplication.Views.CorrectionFactor;
using FourPole = WpfApplication.Views.FourPole;
using LongitudinalCompensation = WpfApplication.Views.LongitudinalCompensation;
using Reactor = WpfApplication.Views.Reactor;
using TabItem = System.Windows.Controls.TabItem;
using VoltageDistribution = WpfApplication.Views.VoltageDistribution;
using WaveParameters = WpfApplication.Views.WaveParameters;
using Window = HandyControl.Controls.Window;

namespace WpfApplication.ViewModels.Windows
{
    /// <summary>
    /// Контекст MainWindow.
    /// </summary>
    internal class MainWindowViewModel : ViewModel
    {
        #region Свойства

        #region Входные данные

        /// <summary>
        /// Входные данные.
        /// </summary>
        public InputData InputData
        {
            get { return _inputData; }
            set { Set(ref _inputData, value); }
        }
        private InputData _inputData;

        #endregion

        #region Страницы расчёта

        /// <summary>
        /// Коллекция расчётов.
        /// </summary>
        public ObservableCollection<TabItem> TabItems
        {
            get { return _tabItems; }
            set { Set(ref _tabItems, value); }
        }
        private ObservableCollection<TabItem> _tabItems;

        #endregion

        #endregion

        #region Приватные поля

        /// <summary>
        /// Путь к файлу сохранения.
        /// </summary>
        private string _filePath = null;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public MainWindowViewModel()
        {
            #region Команды

            FontSizeCommand = new RelayCommand(OnFontSizeCommandExecuted, CanFontSizeCommandExecute);
            ThemeChangeCommand = new RelayCommand(OnThemeChangeCommandExecuted, CanThemeChangeCommandExecute);
            UpdateCommand = new RelayCommand(OnUpdateCommandExecuted, CanUpdateCommandExecute);
            OpenFileCommand = new RelayCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            SafeFileCommand = new RelayCommand(OnSafeFileCommandExecuted, CanSafeFileCommandExecute);
            SafeFileAsCommand = new RelayCommand(OnSafeFileAsCommandExecuted, CanSafeFileAsCommandExecute);

            #endregion

            InputData = new InputData();
            InputData.PropertyChanged += IntutDataPropertyChanged;

            FillTabControl();
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Заполнить TabControl расчётами.
        /// </summary>
        private void FillTabControl()
        {
            TabItems = new ObservableCollection<TabItem>();
            TabItems.Add(new TabItem()
            {
                Header = "ВОЛНОВЫЕ ПАРАМЕТРЫ",
                Content = new WaveParameters()
                {
                    DataContext = new WaveParametersViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
            TabItems.Add(new TabItem()
            {
                Header = "ДАННЫЕ ЧЕТЫРЁХПОЛЮСНИКА",
                Content = new FourPole()
                {
                    DataContext = new FourPoleViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
            TabItems.Add(new TabItem()
            {
                Header = "ПОПРАВОЧНЫЕ КОЭФФИЦИЕНТЫ",
                Content = new CorrectionFactor()
                {
                    DataContext = new CorrectionFactorViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
            TabItems.Add(new TabItem()
            {
                Header = "РЕАКТОР",
                Content = new Reactor()
                {
                    DataContext = new ReactorViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
            TabItems.Add(new TabItem()
            {
                Header = "ПРОДОЛЬНАЯ КОМПЕНСАЦИЯ",
                Content = new LongitudinalCompensation()
                {
                    DataContext = new LongitudinalCompensationViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
            TabItems.Add(new TabItem()
            {
                Header = "РАСПРЕДЕЛЕНИЕ НАПРЯЖЕНИЯ",
                Content = new VoltageDistribution()
                {
                    DataContext = new VoltageDistributionViewModel()
                    {
                        InputData = InputData
                    }
                }
            });
        }

        /// <summary>
        /// Обновить расчёты.
        /// </summary>
        private void IntutDataPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateCommand?.Execute(this);
        }

        #endregion

        #region Команды

        #region Открыть файл

        public ICommand OpenFileCommand { get; }

        private bool CanOpenFileCommandExecute(object p)
        {
            return true;
        }

        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog()
        {
            AddExtension = true,
            Filter = "Вильнер|*.vilner|Дальние линии|*.dlep|Все файлы|*.dlep;*.vilner",
            Title = "Открыть файл",
        };

        private void OnOpenFileCommandExecuted(object p)
        {
            if (!_openFileDialog.ShowDialog() ?? false)
            {
                return;
            }

            _filePath = _openFileDialog.FileName;

            var inputData = JsonSerializer.Deserialize<InputData>(File.ReadAllText(_filePath));

            if (inputData is not null)
            {
                UpdateCommand?.Execute(this);
            }
        }

        #endregion

        #region Сохранить файл

        public ICommand SafeFileCommand { get; }

        private bool CanSafeFileCommandExecute(object p)
        {
            return InputData is not null && File.Exists(_filePath);
        }

        private void OnSafeFileCommandExecuted(object p)
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize<InputData>(InputData));

            UpdateCommand?.Execute(null);
        }

        #endregion

        #region Сохранить файл как

        public ICommand SafeFileAsCommand { get; }

        private bool CanSafeFileAsCommandExecute(object p)
        {
            return InputData is not null;
        }

        private readonly OpenFileDialog _saveFileAsDialog = new OpenFileDialog()
        {
            AddExtension = true,
            Filter = "Вильнер|*.vilner|Дальние линии|*.dlep|Все файлы|*.dlep;*.vilner",
            CheckFileExists = false,
            Title = "Сохранить файл",
        };

        private void OnSafeFileAsCommandExecuted(object p)
        {
            if (!_saveFileAsDialog.ShowDialog() ?? true)
            {
                return;
            }

            _filePath = _saveFileAsDialog.FileName;

            if (string.IsNullOrEmpty(_filePath))
            {
                return;
            }

            File.WriteAllText(_filePath, JsonSerializer.Serialize<InputData>(InputData));
        }

        #endregion

        #region Обновить

        public ICommand UpdateCommand { get; }

        private bool CanUpdateCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateCommandExecuted(object p)
        {
            foreach (var d in TabItems)
            {
                ((ObserverViewModel)((UserControl)d.Content).DataContext).Update();
            }
        }

        #endregion

        #region Изменить тему

        public ICommand ThemeChangeCommand { get; }

        private bool CanThemeChangeCommandExecute(object p)
        {
            return true;
        }

        private void OnThemeChangeCommandExecuted(object p)
        {
            if (p is Window window)
            {
                Theme.SetSkin(window, Theme.GetSkin(window) == HandyControl.Data.SkinType.Default ?
                    HandyControl.Data.SkinType.Dark :
                    HandyControl.Data.SkinType.Default);
            }

        }

        #endregion

        #region Изменить размер шрифта

        public ICommand FontSizeCommand { get; }

        private bool CanFontSizeCommandExecute(object p)
        {
            return true;
        }

        private void OnFontSizeCommandExecuted(object p)
        {
            if (p is Window window)
            {
                window.FontSize = window.FontSize == 12 ?
                    14 : 12;
            }

        }

        #endregion

        #endregion
    }
}
