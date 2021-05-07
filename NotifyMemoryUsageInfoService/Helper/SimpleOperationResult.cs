using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class SimpleOperationResult<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
