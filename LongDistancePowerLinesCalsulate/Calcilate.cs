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
            Validator.ArgumentNullException(inputData, nameof(inputData));

            return new(inputData);
        }

        /// <summary>
        /// Получить данные четырёхполюсника.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Четырёхполюсник.</returns>
        public static FourPole GetFourPole(InputData inputData)
        {
            Validator.ArgumentNullException(inputData, nameof(inputData));

            return new(inputData);
        }

        /// <summary>
        /// Получить данные поправочных коэффициентов.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Поправочные коэффициенты</returns>
        public static CorrectionFactor GetCorrectionFactor(InputData inputData)
        {
            Validator.ArgumentNullException(inputData, nameof(inputData));

            return new(inputData);
        }

        /// <summary>
        /// Получить данные распределения напряжения вдоль линии.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Данные распределения напряжения.</returns>
        public static VoltageDistribution GetVoltageDistribution(InputData inputData)
        {
            Validator.ArgumentNullException(inputData, nameof(inputData));

            return new(inputData);
        }

        /// <summary>
        /// Получить данные реактора.
        /// </summary>
        /// <param name="inputData">Входные данные.</param>
        /// <returns>Данные реактора.</returns>
        public static Reactor GetReactor(InputData inputData)
        {
            Validator.ArgumentNullException(inputData, nameof(inputData));

            return new(inputData);
        }
    }
}
