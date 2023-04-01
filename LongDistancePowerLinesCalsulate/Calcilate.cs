using LongDistancePowerLinesCalsulate.Classes;
using LongDistancePowerLinesCalsulate.Classes.OutputData;

namespace LongDistancePowerLinesCalsulate
{
    /// <summary>
    /// Расчёт дальних линий.
    /// </summary>
    public static class Calcilate
    {
        /// <summary>
        /// Получить данные волновых параметров.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Волновые параметры.</returns>
        public static WaveParameters GetWaveParameters(InputData inputData)
        {
            return new(inputData);
        }

        /// <summary>
        /// Получить данные четырёхполюсника.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Четырёхполюсник.</returns>
        public static FourPole GetFourPole(InputData inputData)
        {
            return new(inputData);
        }

        /// <summary>
        /// Получить данные поправочных коэффициентов.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Поправочные коэффициенты</returns>
        public static CorrectionFactor GetCorrectionFactor(InputData inputData)
        {
            return new(inputData);
        }

        /// <summary>
        /// Получить данные распределения напряжения вдоль линии.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Данные распределения напряжения.</returns>
        public static ((double[] Xs, double[] Ys) Natural, (double[] Xs, double[] Ys) More, (double[] Xs, double[] Ys) Less, (double[] Xs, double[] Ys) OneSided) GetVoltageDistribution(InputData inputData)
        {
            var distribution = new VoltageDistribution(inputData);

            return (
                distribution.GetVoltageCollection_NaturalPower(), distribution.GetVoltageCollection_MoreNaturalPower(),
                distribution.GetVoltageCollection_LessNaturalPower(), distribution.GetVoltageCollection_OneSided());
        }

        /// <summary>
        /// Получить данные напряжения на определённом расстоянии.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Данные напряжения.</returns>
        public static (double Natural, double More, double Less, double OneSided) GetVoltage(InputData inputData, double l)
        {
            var distribution = new VoltageDistribution(inputData);

            return (
                distribution.GetVoltage_NaturalPower(l), distribution.GetVoltage_MoreNaturalPower(l),
                distribution.GetVoltage_LessNaturalPower(l), distribution.GetVoltage_OneSided(l));
        }
    }
}
