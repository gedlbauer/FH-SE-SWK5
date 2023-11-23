using OrderManagement.Proxy;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderManagement.CSharpClient
{
    static class DtoExtension
    {
        public static string ToPropertyString(this CustomerDto customerDto)
        {
            return $"Customer {{ Id = '{customerDto.Id}', Name = '{customerDto.Name}', " +
                   $"zipCode = {customerDto.ZipCode}, city = '{customerDto.City}', " +
                   $"totalRevenue = {customerDto.TotalRevenue} }}";
        }
    }

    class Program
    {
        private static readonly string ORDER_MANAGEMENT_BASE_URI = "http://localhost:5000";

        private static void PrintTitle(string text = "", int length = 110, char character = '-')
        {
            int preLen = (length - (text.Length + 2)) / 2;
            int postLen = length - (preLen + text.Length + 2);
            Console.WriteLine($"{new string(character, preLen)} {text} {new string(character, postLen)}");
        }

        private async static Task OpenApiClient(HttpClient httpClient)
        {
            async Task PrintCustomersAsync(CustomersClient customersClient)
            {
                foreach (CustomerDto customer in (await customersClient.GetCustomersAsync()).Result)
                {
                    Console.WriteLine(customer.ToPropertyString());
                }
            }

            async Task GetCustomersByIdAsync(CustomersClient customersClient, params Guid[] ids)
            {
                foreach (Guid id in ids)
                {
                    try
                    {
                        CustomerDto customer = (await customersClient.GetCustomerByIdAsync(id)).Result;
                        Console.WriteLine(customer.ToPropertyString());
                    }
                    catch (ApiException ex)
                    {
                        Console.WriteLine($"StatusCode={ex.StatusCode}, Message={ex.Message}");
                    }
                }
            }

            //
            // Create CustomersClient
            //
            var customersClient = new CustomersClient(ORDER_MANAGEMENT_BASE_URI, httpClient);

            PrintTitle("Customer List");
            await PrintCustomersAsync(customersClient);

            PrintTitle("GetCustomersByIdAsync");
            await GetCustomersByIdAsync(customersClient,
              new Guid("cccccccc-0000-0000-0000-111111111111"),
              new Guid("cccccccc-0000-0000-0000-000000009999"));
        }

        static async Task Main(string[] args)
        {
            //
            // Create HttpClient
            // 
            using var httpClient = new HttpClient();

            PrintTitle("OpenApiClient", character: '=');
            await OpenApiClient(httpClient);
        }
    }
}
