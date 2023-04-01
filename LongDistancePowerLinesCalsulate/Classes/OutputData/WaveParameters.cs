﻿using System.Numerics;

namespace LongDistancePowerLinesCalsulate.Classes.OutputData
{
    /// <summary>
    /// Волновые параметры.
    /// </summary>
    public class WaveParameters
    {
        private readonly InputData _inputData;

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        public WaveParameters(InputData inputData)
        {
            _inputData = inputData;
        }

        /// <summary>
        /// Волновые параметры одиночного провода.
        /// </summary>
        public (double, double, double, double,
            Complex, Complex, Complex, double,
            double, double, Complex, double)
            Single => (
            _inputData.MeanDistanceBetweenPhaseWires_Single,
            _inputData.RadiusWire_Single,
            _inputData.ResistanceWire_Single,
            _inputData.CapacitiveConductivityWire_Single,
            _inputData.ComplexResistanceWire_Single,
            _inputData.ComplexConductivityWire_Single,
            _inputData.WavePropagationCoefficient_Single,
            _inputData.AttenuatioCoefficient_Single,
            _inputData.PhaseCoefficient_Single,
            _inputData.PhaseCoefficientDeg_Single,
            _inputData.WaveResistanceLine_Single,
            _inputData.NaturalPower_Single
            );

        /// <summary>
        /// Волновые параметры расщеплённого провода.
        /// </summary>
        public (double, double, double, double,
            Complex, Complex, Complex, double,
            double, double, Complex, double)
            Split => (
            _inputData.MeanDistanceBetweenPhaseWires,
            _inputData.RadiusWire,
            _inputData.ResistanceWire,
            _inputData.CapacitiveConductivityWire,
            _inputData.ComplexResistanceWire,
            _inputData.ComplexConductivityWire,
            _inputData.WavePropagationCoefficient,
            _inputData.AttenuatioCoefficient,
            _inputData.PhaseCoefficient,
            _inputData.PhaseCoefficientDeg,
            _inputData.WaveResistanceLine,
            _inputData.NaturalPower
            );
    }
}