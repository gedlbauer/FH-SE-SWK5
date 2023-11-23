using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.Domain;

namespace CurrencyConverter.Logic
{
  public class CurrencyCalculatorImpl : ICurrencyCalculator
  {
    private class Entry
    {
      public String Name;      // long form of currency name
      public String Country;   // country currency is used in
      public double Rate;      // current rate of exchange

      public Entry(String name, String country, double rate)
      {
        this.Name = name;
        this.Country = country;
        this.Rate = rate;
      }
    }

    // currTable maps each currency string to an Entry object
    private static readonly IDictionary<String, Entry> currTable =
      new ConcurrentDictionary<string, Entry>();

    static CurrencyCalculatorImpl()
    {
      // initialize currencyTable
      currTable.Add("USD", new Entry("Dollar", "USA", 1.21));
      currTable.Add("AUD", new Entry("Dollar", "Australia", 1.61));
      currTable.Add("BRL", new Entry("Real", "Brazil", 6.14));
      currTable.Add("GBP", new Entry("Pound", "GB", 0.92));
      currTable.Add("CAD", new Entry("Dollar", "Canada", 1.52));
      currTable.Add("CNY", new Entry("Yuan", "China", 7.67));
      currTable.Add("DKK", new Entry("Krone", "Denmark", 7.44));
      currTable.Add("HKD", new Entry("Dollar", "Hong Kong", 9.22));
      currTable.Add("INR", new Entry("Rupee", "India", 75.39));
      currTable.Add("JPY", new Entry("Yen", "Japan", 125.98));
      currTable.Add("MYR", new Entry("Ringgit", "Malysia", 4.77));
      currTable.Add("MXN", new Entry("Peso", "Mexico", 22.25));
      currTable.Add("EUR", new Entry("Euro", "Europe", 1.0));
    }

    private Task<double> EuroRate(String currSymbol)
    {
      if (currTable.TryGetValue(currSymbol, out Entry entry))
        return Task.FromResult(entry.Rate);
      else
        throw new ArgumentException("invalid currency " + currSymbol);
    }


    public Task<CurrencyData> GetCurrencyDataAsync(string currSymbol)
    {
      if (currTable.TryGetValue(currSymbol, out Entry entry))
        return Task.FromResult(new CurrencyData { Symbol = currSymbol, Name = entry.Name, Country = entry.Country, EuroRate = entry.Rate });
      else
        throw new ArgumentException("invalid currency " + currSymbol);
    }

    public async Task<double> RateOfExchangeAsnyc(string srcCurr, string targCurr) =>
      (await EuroRate(targCurr)) / (await EuroRate(srcCurr));

    public Task<IEnumerable<string>> GetCurrenciesAsync() => 
      Task.FromResult<IEnumerable<string>>(currTable.Keys.OrderBy(s => s));

    public Task<bool> CurrencyExistsAsync(string currSymbol) => 
      Task.FromResult(currTable.ContainsKey(currSymbol));

    public Task InsertAsnyc(CurrencyData data)
    {
      if (currTable.ContainsKey(data.Symbol))
        throw new ArgumentException("currency " + data.Symbol + " already exists");
      currTable.Add(data.Symbol, new Entry(data.Name, data.Country, data.EuroRate));
      return Task.CompletedTask;
    }

    public Task UpdateAsync(CurrencyData data)
    {
      if (currTable.TryGetValue(data.Symbol, out Entry entry))
      {
        entry.Name = data.Name;
        entry.Country = data.Country;
        entry.Rate = data.EuroRate;
        return Task.CompletedTask;
      }
      else
        throw new ArgumentException("invalid currency " + data.Symbol);
    }

    public Task DeleteAsync(string symbol)
    {
      currTable.Remove(symbol);
      return Task.CompletedTask;
    }
  }

}
