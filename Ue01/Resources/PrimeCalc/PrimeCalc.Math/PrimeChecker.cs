using static System.Math;

namespace PrimeCalc
{
    public class PrimeChecker
    {
        private const double EPS = .1;
        public static bool IsPrime(int number)
        {
            for (int i = 2; i <= Sqrt(number) + EPS; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}