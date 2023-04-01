﻿using System.Numerics;
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

        #region Котангенс

        /// <summary>
        /// Котангенс комплексного числа.
        /// </summary>
        /// <param name="x">Комплексное число.</param>
        /// <returns>Результат.</returns>
        private static Complex Ctg(Complex x)
        {
            return 1 / Tan(x);
        }

        /// <summary>
        /// Котангенс числа.
        /// </summary>
        /// <param name="x">Число.</param>
        /// <returns>Результат.</returns>
        private static double Ctg(double x)
        {
            return 1 / Math.Tan(x);
        }

        #endregion

        #region Токи

        /// <summary>
        /// Ток больше натуральной мощности.
        /// </summary>
        private Complex I2_more
        {
            get
            {
                var P2 = _inputData.K2 * _inputData.NaturalPower;

                var _Q2 = -Ctg(new Complex(_inputData.PhaseCoefficient, 0) * new Complex(_inputData.LineLength, 0)) +
                Sqrt(1 / Pow(Sin(new Complex(_inputData.PhaseCoefficient, 0) * new Complex(_inputData.LineLength, 0)), 2) -
                Pow(new Complex(_inputData.K2, 0), 2));

                var Q2 = _Q2.Magnitude;

                var rad = Math.Atan((_Q2 * _inputData.NaturalPower).Magnitude / (_inputData.K2 * _inputData.NaturalPower));

                return new Complex(
                           Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Cos(rad),
                           Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Sin(rad));
            }
        }

        /// <summary>
        /// Ток меньше натуральной мощности.
        /// </summary>
        private Complex I2_less
        {
            get
            {
                var P2 = _inputData.K1 * _inputData.NaturalPower;

                var _Q2 = Ctg(_inputData.PhaseCoefficient * _inputData.LineLength) +
                    Math.Sqrt(1 / Math.Pow(Math.Sin(_inputData.PhaseCoefficient * _inputData.LineLength), 2) -
                    Math.Pow(_inputData.K1, 2));

                var Q2 = _Q2 * _inputData.NaturalPower;

                var rad = Math.Atan(Q2 / P2);

                return new Complex(
                    Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Cos(-rad),
                    Math.Sqrt(Math.Pow(P2, 2) + Math.Pow(Q2, 2)) / _inputData.NominalVoltage * Math.Sin(-rad));
            }
        }

        #endregion

        #region Напряжение при натуральной мощности

        /// <summary>
        /// Напряжение при натуральной мощности.
        /// </summary>
        public double GetVoltage_NaturalPower(double l) =>
            _inputData.NominalVoltage;

        /// <summary>
        /// Получить распределение напряжения вдоль линии при натуральной мощности.
        /// </summary>
        /// <param name="Xs">Расстояний от начала линии.</param>
        /// <param name="Ys">Значение напряжения.</param>
        public void GetVoltageCollection_NaturalPower(out double[] Xs, out double[] Ys)
        {
            Xs = new double[2];
            Ys = new double[2];

            Xs[0] = 0;
            Xs[1] = _inputData.LineLength;
            Ys[0] = _inputData.NominalVoltage;
            Ys[1] = _inputData.NominalVoltage;
        }

        #endregion

        #region Напряжение при мощности больше натуральной

        /// <summary>
        /// Напряжение при мощности больше натуральной.
        /// </summary>
        /// <param name="l">Расстояние от начала линии.</param>
        /// <returns>Напряжение.</returns>
        public double GetVoltage_MoreNaturalPower(double l) => (Math.Cos(_inputData.PhaseCoefficient * (_inputData.LineLength - l)) *
            _inputData.NominalVoltage + _inputData.WaveResistanceLine.Magnitude * ImaginaryOne * Math.Sin(_inputData.PhaseCoefficient *
                (_inputData.LineLength - l)) * I2_more).Magnitude;

        /// <summary>
        /// Получить распределение напряжения вдоль линии при мощности больше натуральной.
        /// </summary>
        /// <returns>
        /// <para>Xs - Расстояний от начала линии.</para>
        /// <para>Ys - Значение напряжения.</para>
        /// </returns>
        public (double[] Xs, double[] Ys) GetVoltageCollection_MoreNaturalPower()
        {
            double[] Xs = new double[(int)_inputData.LineLength + 1];
            double[] Ys = new double[(int)_inputData.LineLength + 1];

            double beta = _inputData.WavePropagationCoefficient.Magnitude * Math.Sin(_inputData.WavePropagationCoefficient.Phase);
            Complex i2 = I2_more;
            double z = _inputData.WaveResistanceLine.Magnitude;

            Parallel.For(0, Xs.Length, (i) =>
            {
                Xs[i] = i;
                Ys[i] = (Math.Cos(beta * (_inputData.LineLength - i)) * _inputData.NominalVoltage +
                z * ImaginaryOne * Math.Sin(beta * (_inputData.LineLength - i)) * i2).Magnitude;
            });

            return (Xs, Ys);
        }


        #endregion

        #region Напряжение при мощности меньше натуральной

        /// <summary>
        /// Напряжение при мощности меньше натуральной.
        /// </summary>
        /// <param name="l">Расстояние от начала линии.</param>
        /// <returns>Напряжение.</returns>
        public double GetVoltage_LessNaturalPower(double l) => (Math.Cos(_inputData.PhaseCoefficient * (_inputData.LineLength - l)) *
            _inputData.NominalVoltage + _inputData.WaveResistanceLine.Magnitude * ImaginaryOne * Math.Sin(_inputData.PhaseCoefficient *
                (_inputData.LineLength - l)) * I2_less).Magnitude;

        /// <summary>
        /// Получить распределение напряжения вдоль линии при мощности меньше натуральной.
        /// </summary>
        /// <returns>
        /// <para>Xs - Расстояний от начала линии.</para>
        /// <para>Ys - Значение напряжения.</para>
        /// </returns>
        public (double[] Xs, double[] Ys) GetVoltageCollection_LessNaturalPower()
        {
            double[] Xs = new double[(int)_inputData.LineLength + 1];
            double[] Ys = new double[(int)_inputData.LineLength + 1];

            double beta = _inputData.WavePropagationCoefficient.Magnitude * Math.Sin(_inputData.WavePropagationCoefficient.Phase);
            Complex i2 = I2_less;
            double z = _inputData.WaveResistanceLine.Magnitude;

            Parallel.For(0, Xs.Length, (i) =>
            {
                Xs[i] = i;
                Ys[i] = (Math.Cos(beta * (_inputData.LineLength - i)) * _inputData.NominalVoltage +
                z * Complex.ImaginaryOne * Math.Sin(beta * (_inputData.LineLength - i)) * i2).Magnitude;
            });

            return (Xs, Ys);
        }

        #endregion

        #region Напряжение при одностороннем включении

        /// <summary>
        /// Напряжение при одностороннем включении.
        /// </summary>
        /// <param name="l">Расстояние от начала линии.</param>
        /// <returns>Напряжение.</returns>
        public double GetVoltage_OneSided(double l)
        {
            var beta = _inputData.WavePropagationCoefficient.Magnitude * Math.Sin(_inputData.WavePropagationCoefficient.Phase);
            var a = (Complex)Math.Cos(beta * _inputData.LineLength);
            var b = _inputData.WaveResistanceLine.Magnitude * Math.Sin(beta * _inputData.LineLength) * ImaginaryOne;
            var i2 = I2_more;

            return Math.Cos(beta * (_inputData.LineLength - l)) *
                ((a * _inputData.NominalVoltage + b * I2_more).Magnitude / a.Magnitude);
        }

        /// <summary>
        /// Получить распределение напряжения вдоль линии при одностороннем включении.
        /// </summary>
        /// <returns>
        /// <para>Xs - Расстояний от начала линии.</para>
        /// <para>Ys - Значение напряжения.</para>
        /// </returns>
        public (double[] Xs, double[] Ys) GetVoltageCollection_OneSided()
        {
            double[] Xs = new double[(int)_inputData.LineLength + 1];
            double[] Ys = new double[(int)_inputData.LineLength + 1];

            var beta = _inputData.WavePropagationCoefficient.Magnitude * Math.Sin(_inputData.WavePropagationCoefficient.Phase);
            var a = (Complex)Math.Cos(beta * _inputData.LineLength);
            var b = _inputData.WaveResistanceLine.Magnitude * Math.Sin(beta * _inputData.LineLength) * ImaginaryOne;
            var i2 = I2_more;

            Parallel.For(0, Xs.Length, (i) =>
            {
                Xs[i] = i;
                Ys[i] = Math.Cos(beta * (_inputData.LineLength - i)) *
                ((a * _inputData.NominalVoltage + b * I2_more).Magnitude / a.Magnitude);
            });

            return (Xs, Ys);
        }

        #endregion

    }
}
