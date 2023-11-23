using System.Resources;

namespace PrimeCalc.Client
{
    internal class Resources
    {
        private static ResourceManager resourceManager;

        private static ResourceManager ResourceManager
        {
            get
            {
                if (resourceManager == null)
                {
                    resourceManager = new ResourceManager(
                        "PrimeCalc.Client.Messages",
                        typeof(Resources).Assembly
                    );
                }
                return resourceManager;
            }
        }

        public static string GetString(string resourceName) =>
          ResourceManager.GetString(resourceName);
    }
}
