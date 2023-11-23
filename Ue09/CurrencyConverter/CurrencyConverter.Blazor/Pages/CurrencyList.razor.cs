using CurrencyConverter.Blazor.Services.Generated;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Blazor.Pages
{
    public partial class CurrencyList
    {
        [Inject]
        private IConverterService ConverterService { get; set; }

        private IEnumerable<CurrencyData> currencies;
        private double sourceValue = 1;
        private bool inputValid = true;

        protected async override Task OnInitializedAsync() => await RefreshAsync();

        private async Task RefreshAsync()
        {
            currencies = (await ConverterService.GetAllAsync()).Result;
        }

        private void HandleInput(string value)
        {
            inputValid = double.TryParse(value, out double newValue);
            if (inputValid)
            {
                sourceValue = newValue;
            }
        }
    }
}
