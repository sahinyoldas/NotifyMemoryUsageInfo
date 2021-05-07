using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class SystemText
    {
        public string NewLineSymbolsText { get; set; }
        public string LogDirectoryPath { get; set; }
        public string LogFilePath { get; set; }
        public string TotalVisibleMemorySize { get; set; }
        public string Memory { get; set; }
        public string AvailableMemoryByte { get; set; }
        public string MailSubject { get; set; }
        public string CheckMaxMemorySize { get; set; }
        public string TotalMemorySize { get; set; }
        public string AvailableMemorySize { get; set; }
        public string UsingMemorySize { get; set; }
        public string ServiceStartText { get; set; }
        public string ServiceStopedText { get; set; }
        public string ServiceStartAgainText { get; set; }
        public string GBText { get; set; }
        public string MemoryUsageNormal { get; set; }
        public string ExcessiveMemoryUsage { get; set; }
        public string CheckMailListToSend { get; set; }
        public string NotificationText { get; set; }
        public string ControlTime { get; set; }
    }
}
