using LongDistancePowerLinesCalsulate.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Infrastructure.Base
{
    /// <summary>
    /// Обновление данных.
    /// </summary>
    internal interface IUpdate
    {
        /// <summary>
        /// Обновить.
        /// </summary>
        public void Update();

        /// <summary>
        /// Входные данные.
        /// </summary>
        public InputData InputData { get; set; }
    }
}
