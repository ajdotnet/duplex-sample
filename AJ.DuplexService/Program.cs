using System;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using AJ.DuplexService.Information;
using AJ.DuplexService.Subscriptions;

using static AJ.DuplexService.Common.ConsoleHelper;

namespace AJ.DuplexService
{
    /// <summary>Console application, selfhosting WCF Services.</summary>
    class Program
    {
        /// <summary>The service hosts.</summary>
        static ServiceHost[] _hosts = null;

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args")]
        static void Main(string[] args)
        {
            try
            {
                WriteLine(ConsoleColor.Cyan, "AJ.DuplexService - ajdotnet.wordpress.com");
                OpenHosts();
                ReportEndpoints();
                WaitForExit();
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
            finally
            {
                CloseHosts();
            }
        }

        private static void OpenHosts()
        {
            WriteLine(ConsoleColor.White, "Opening hosts...");

            // two services, tcp/ip and http each...
            _hosts = new ServiceHost[]
            {
                new ServiceHost(typeof(SubscriptionServiceNetTcp)),
                new ServiceHost(typeof(InformationServiceNetTcp)),
                new ServiceHost(typeof(SubscriptionServiceHttp)),
                new ServiceHost(typeof(InformationServiceHttp)),
            };

            foreach (var host in _hosts)
            {
                try
                {
                    host.Open();
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                }
            }

            WriteLine(ConsoleColor.White, "Done.");
        }

        private static void ReportEndpoints()
        {
            WriteLine(ConsoleColor.White, "Available Endpoints:");
            foreach (var host in _hosts)
            {
                foreach (var ep in host.Description.Endpoints)
                    WriteLine(ConsoleColor.White, "        " + ep.Address.ToString());
            }
        }

        private static void CloseHosts()
        {
            WriteLine(ConsoleColor.White, "Closing hosts...");
            foreach (var host in _hosts)
            {
                WriteLine(ConsoleColor.White, "        " + host.Description.ServiceType.Name);
                host.Close(TimeSpan.FromSeconds(5));
                (host as IDisposable)?.Dispose();
            }
            WriteLine(ConsoleColor.White, "Done.");
        }
    }
}
