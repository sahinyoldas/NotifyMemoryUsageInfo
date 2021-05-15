using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class MemoryManager : IMemoryService
    {
        private ILogger _logger;
        private IMailService _mailService;

        public MemoryManager(ILogger logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        private static double _systemTotalMemorySize { get; set; }
        public static double SystemTotalMemorySize
        {
            get
            {
                if (_systemTotalMemorySize > 0)
                    return _systemTotalMemorySize;
                else
                {
                    _systemTotalMemorySize = getSystemTotalMemorySize();
                    return _systemTotalMemorySize;
                }
            }
        }

        public static double SystemAvailableMemorySize
        {
            get
            {
                return gettAvailableMemorySize();
            }
        }

        private static double getSystemTotalMemorySize()
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            double fres = 0;
            if (results.Count > 0)
                foreach (ManagementObject result in results)
                {
                    double res = Convert.ToDouble(result[ConstantFields.SystemText.TotalVisibleMemorySize]);
                    fres = Math.Round((res / (1024 * 1024)), 2);
                }

            return fres;
        }
        private static double gettAvailableMemorySize()
        {
            PerformanceCounter ramCounter = new PerformanceCounter(ConstantFields.SystemText.Memory, ConstantFields.SystemText.AvailableMemoryByte);
            return Math.Round((ramCounter.NextValue() / (1024 * 1024 * 1024)), 2);
        }

        public void CheckMemoryStatus()
        {
            try
            {
                double totalMemorySize = SystemTotalMemorySize;
                double availableMemorySize = SystemAvailableMemorySize;
                double remainingMemorySize = totalMemorySize - availableMemorySize;
                string memoryUsageSummary = ConstantFields.SystemText.ControlTime + DateTime.Now + "{0}" +
                            ConstantFields.SystemText.TotalMemorySize + totalMemorySize + ConstantFields.SystemText.GBText + "{0}" +
                            ConstantFields.SystemText.AvailableMemorySize + availableMemorySize + ConstantFields.SystemText.GBText + "{0}" +
                            ConstantFields.SystemText.UsingMemorySize + Math.Round(remainingMemorySize, 2) + ConstantFields.SystemText.GBText;

                _logger.WriteToFile(string.Format(memoryUsageSummary, Environment.NewLine));

                if (ConstantFields.MaxMemorySizeForNotify == 0)
                {
                    _logger.WriteToFile(ConstantFields.SystemText.CheckMaxMemorySize + Environment.NewLine);
                }
                else if (ConstantFields.MaxMemorySizeForNotify > 0 && remainingMemorySize > ConstantFields.MaxMemorySizeForNotify)
                {
                    _mailService.SendMail(ConstantFields.SystemText.ExcessiveMemoryUsage + "<br/>" + string.Format(memoryUsageSummary, "<br/>"));
                }
                else
                {
                    _logger.WriteToFile(ConstantFields.SystemText.MemoryUsageNormal + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteToFile(ex.ToString() + Environment.NewLine);
            }
        }
    }
}
