using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public static class CommonHelper
    {
        /// <summary>
        /// Get Mail list from config file.separating mails with '*' symbol
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetMailListToSend()
        {
            IList<string> returnList = new List<string>();
            var mailListText = ConfigurationManager.AppSetting["AppSettings:MailList"];
            if (mailListText != null)
            {
                string[] mailArray = mailListText.ToString().Split('*');
                returnList = mailArray.ToList();
            }
            return returnList;
        }
    }
}
