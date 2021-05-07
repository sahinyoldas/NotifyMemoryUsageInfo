using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NotifyMemoryUsageInfoService
{
    public class ConstantFields
    {
        #region [  Fields  ]

        private static int _serviceWorkingTime { get; set; }
        public static int ServiceWorkingTime
        {
            get
            {
                if (_serviceWorkingTime > 0)
                    return _serviceWorkingTime;
                else
                {
                    _serviceWorkingTime = ConfigurationManager.AppSetting["AppSettings:ServiceWorkingTime"] != null ? Convert.ToInt32(ConfigurationManager.AppSetting["AppSettings:ServiceWorkingTime"]) : 0;
                    return _serviceWorkingTime;
                }
            }
        }

        private static double _maxMemorySizeForNotify { get; set; }
        public static double MaxMemorySizeForNotify
        {
            get
            {
                if (_maxMemorySizeForNotify > 0)
                    return _maxMemorySizeForNotify;
                else
                {
                    _maxMemorySizeForNotify = ConfigurationManager.AppSetting["AppSettings:MaxMemorySize"] != null ? Convert.ToDouble(ConfigurationManager.AppSetting["AppSettings:MaxMemorySize"]) : 0;
                    return _maxMemorySizeForNotify;
                }
            }
        }

        private static IList<string> _mailListToSend { get; set; }
        public static IList<string> MailListToSend
        {
            get
            {
                if (_mailListToSend.IsNotNullOrNotEmpty())
                    return _mailListToSend;
                else
                {
                    _mailListToSend = CommonHelper.GetMailListToSend();
                    return _mailListToSend;
                }
            }
        }

        private static MailInfo _mailInfo { get; set; }
        public static MailInfo MailInfo
        {
            get
            {
                if (_mailInfo != null)
                    return _mailInfo;
                else
                {
                    _mailInfo = ConfigurationManager.AppSetting.GetSection("MailSettings").Get<MailInfo>();
                    return _mailInfo;
                }
            }
        }

        private static SystemText _systemText { get; set; }
        public static SystemText SystemText
        {
            get
            {
                if (_systemText != null)
                    return _systemText;
                else
                {
                    _systemText = ConfigurationManager.AppSetting.GetSection("SystemText").Get<SystemText>();
                    return _systemText;
                }
            }
        }

        #endregion
    }
}
