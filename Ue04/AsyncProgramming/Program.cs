using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class Program
    {
        private const string URL1 = "https://github.com/progit/progit2/releases/download/2.1.331/progit.pdf"; // Pro Git
        private const string URL2 = "https://enos.itcollege.ee/~jpoial/oop/naited/Clean%20Code.pdf"; // Clean Code

        static async Task Main()
        {
            var downloader = new Downloader();

            Console.WriteLine("====================== DownloadAndSaveSync ======================");
            downloader.DownloadAndSaveSync(URL1, $"{nameof(Downloader.DownloadAndSaveSync)}.pdf");
            Console.WriteLine("Main: DownloadAndSaveSync completed work.");
            Console.WriteLine();



            Console.WriteLine("====================== DownloadAndSaveTask ======================");
            var task = downloader.DownloadAndSaveTask(URL2, $"{nameof(Downloader.DownloadAndSaveTask)}.pdf");
            Console.WriteLine("Main: DownloadAndSaveTask gave control back to caller");
            task.Wait();
            Console.WriteLine("Main: DownloadAndSaveTask completed work.");
            Console.WriteLine();



            Console.WriteLine("====================== DownloadAndSaveAsync ======================");
            var awaitable = downloader.DownloadAndSaveAsync(URL1, $"{nameof(Downloader.DownloadAndSaveAsync)}.pdf");
            Console.WriteLine("Main: DownloadAndSaveAsync gave control back to caller");
            await awaitable;
            Console.WriteLine("Main: DownloadAndSaveAsync completed work.");
            Console.WriteLine();



            Console.WriteLine("======================= DownloadAndSaveMultipleAsync =======================");
            var awaitable2 = downloader.DownloadAndSaveMultipleAsync(
                URL1, $"{nameof(Downloader.DownloadAndSaveMultipleAsync)}-1.pdf",
                URL2, $"{nameof(Downloader.DownloadAndSaveMultipleAsync)}-2.pdf");
            Console.WriteLine("Main: DownloadAndSaveMultipleAsync gave control back to caller");
            await awaitable2;
            Console.WriteLine("Main: DownloadAndSaveMultipleAsync) completed work.");
            Console.WriteLine();
        }
    }
}
