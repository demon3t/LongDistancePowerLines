using LongDistancePowerLinesCalsulate.Classes;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(ConvertToString(new Complex(2, 4.07)));
            Console.ReadLine();
        }

        public static string ConvertToString(Complex c)
        {
            StringBuilder sb = new StringBuilder();

            double real = c.Real;
            double imag = c.Imaginary;

            double realMagnitude = Math.Abs(real);
            double imagMagnitude = Math.Abs(imag);

            // Проверяем знаки вещественной и мнимой частей
            bool realNegative = real < 0;
            bool imagNegative = imag < 0;

            // Форматируем вещественную часть
            if (realMagnitude >= 1)
            {
                sb.AppendFormat("{0:N0}", real);
            }
            else
            {
                sb.AppendFormat("{0:N3}", real);
            }

            sb.Append(" ∙ 10");

            // Форматируем степень десяти
            int realExponent = (int)Math.Floor(Math.Log10(realMagnitude));
            if (realExponent != 0)
            {
                sb.Append("^" + realExponent.ToString());
            }

            sb.Append(" ");

            // Добавляем знак умножения, если мнимая часть не нулевая
            if (imag != 0)
            {
                sb.Append(imagNegative ? "- j" : "+ j");
            }

            // Форматируем мнимую часть
            if (imagMagnitude >= 1)
            {
                sb.AppendFormat("{0:N0}", imag);
            }
            else
            {
                sb.AppendFormat("{0:N3}", imag);
            }

            sb.Append(" ∙ 10");

            // Форматируем степень десяти
            int imagExponent = (int)Math.Floor(Math.Log10(imagMagnitude));
            if (imagExponent != 0)
            {
                sb.Append("^" + imagExponent.ToString());
            }

            return sb.ToString();
        }


    }
}