using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Logic
{
  public static class BLFactory
  {
    private static ICurrencyCalculator calc;

    public static ICurrencyCalculator GetCalculator()
    {
      if (calc == null)
        calc = new CurrencyCalculatorImpl();
      return calc;
    }
  }
}
