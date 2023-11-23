using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Blazor.Models
{
  public partial class CurrencyData
  {
    public string Symbol { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public double EuroRate { get; set; }
  }
}
