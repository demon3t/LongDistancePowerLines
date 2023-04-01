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

    }
}
