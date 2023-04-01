using System.Numerics;
using static System.Numerics.Complex;

namespace LongDistancePowerLinesCalsulate.Classes.OutputData
{
    /// <summary>
    /// Распределение напряжения.
    /// </summary>
    public class VoltageDistribution
    {
        private readonly InputData _inputData;

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        public VoltageDistribution(InputData inputData)
        {
            _inputData = inputData;

        }

        /// <summary>
        /// Котангенс комплексного числа.
        /// </summary>
        /// <param name="x">Комплексное число.</param>
        /// <returns>Результат.</returns>
        private static Complex Ctg(Complex x)
        {
            return 1 / Tan(x);
        }

        #region Токи

        /// <summary>
        /// Ток больше натуральной мощности.
        /// </summary>
        /// <returns>Значения тока.</returns>
        private Complex I2_more 
        {
            get 
            {
                var P2 = _inputData.K2 * _inputData.NaturalPower;

                var _Q2 = -Ctg(new Complex(_inputData.PhaseCoefficient, 0) * new Complex(_inputData.LineLength, 0)) +
                Sqrt(1 / Pow(Sin(new Complex(_inputData.PhaseCoefficient, 0) * new Complex(_inputData.LineLength, 0)), 2) -
                Pow(new Complex(_inputData.K2, 0), 2));

                var Q2 = _Q2.Magnitude;

                var deg = Math.Atan((_Q2 * _inputData.NaturalPower).Magnitude / (_inputData.K2 * _inputData.NaturalPower));

                return new Complex(
                           Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Cos(deg),
                           Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Sin(deg));
            }
        }

        #endregion


        /// <summary>
        /// Натуральная мощность.
        /// </summary>
        public double GetNaturalPower =>
            _inputData.NominalVoltage;

        /// <summary>
        /// Мощность больше натуральной.
        /// </summary>
        public double GetPowerMoreNatural(double l) => (Math.Cos(_inputData.PhaseCoefficient * (_inputData.LineLength - l)) *
            _inputData.NominalVoltage + _inputData.WaveResistanceLine.Magnitude * ImaginaryOne * Math.Sin(_inputData.PhaseCoefficient *
                (_inputData.LineLength - l)) * I2_more).Magnitude;

        //todo : сделать остальные напряжения
    }
}
