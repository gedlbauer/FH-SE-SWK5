using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public class Downloader
    {
        public void DownloadAndSaveSync(string url, string filePath)
        {
            using WebClient client = new WebClient();
            var bytes = client.DownloadData(url);

            Console.WriteLine($"DownloadAndSaveSync: Downloaded '{url}'.");
            File.WriteAllBytes(filePath, bytes);

            Console.WriteLine($"DownloadAndSaveSync: Saved '{filePath}'.");
        }

        public Task DownloadAndSaveTask(string url, string filePath)
        {
            using WebClient client = new WebClient();
            var task = client.DownloadDataTaskAsync(url).ContinueWith(t =>
                {
                    Console.WriteLine($"DownloadAndSaveTask: Downloaded '{url}'.");
                    return t.Result;
                })
                .ContinueWith(t => File.WriteAllBytes(filePath, t.Result))
                .ContinueWith(t => Console.WriteLine($"DownloadAndSaveTask: Saved '{filePath}'."));
            return task;
        }

        public async Task DownloadAndSaveAsync(string url, string filePath)
        {
            using WebClient client = new WebClient();

            var bytes = await client.DownloadDataTaskAsync(url);
            Console.WriteLine($"DownloadAndSaveAsync: Downloaded '{url}'.");

            await File.WriteAllBytesAsync(filePath, bytes);
            Console.WriteLine($"DownloadAndSaveAsync: Saved '{filePath}'.");
        }

        public async Task DownloadAndSaveMultipleAsync(string url1, string filePath1, string url2, string filePath2)
        {
            Task t1 = DownloadAndSaveAsync(url1, filePath1);
            Console.WriteLine($"DownloadAndSaveMultipleAsync: DownloadAndSaveAsync of '{url1}' started.");

            Task t2 = DownloadAndSaveAsync(url2, filePath2);
            Console.WriteLine($"DownloadAndSaveMultipleAsync: DownloadAndSaveAsync of '{url2}' started.");

            await Task.WhenAll(t1, t2);
            Console.WriteLine("DownloadAndSaveMultipleAsync: DownloadAndSaveAsync of all files completed.");
        }
    }
}
