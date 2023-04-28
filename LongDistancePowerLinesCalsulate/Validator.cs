namespace LongDistancePowerLinesCalsulate
{
    /// <summary>
    /// Валидатор.
    /// </summary>
    internal static class Validator
    {
        /// <summary>
        /// Проверка на null значение.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="item">Проверяемый объект.</param>
        /// <param name="name">Имя объекта.</param>
        public static void ArgumentNullException<T>(T item, string name) where T : class
        {
            if (item is null)
            {
                throw new ArgumentNullException(name, "Не ожидалось null значение.");
            }
        }
    }
}
