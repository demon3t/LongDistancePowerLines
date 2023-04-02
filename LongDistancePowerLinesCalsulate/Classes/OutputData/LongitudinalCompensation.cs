using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongDistancePowerLinesCalsulate.Classes.OutputData
{
    /// <summary>
    /// Продольная компенсация.
    /// </summary>
    public class LongitudinalCompensation
    {
        private readonly InputData _inputData;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        public LongitudinalCompensation(InputData inputData)
        {
            Validator.ArgumentNullException(inputData, nameof(inputData));

            _inputData = inputData;
        }


    }
}
