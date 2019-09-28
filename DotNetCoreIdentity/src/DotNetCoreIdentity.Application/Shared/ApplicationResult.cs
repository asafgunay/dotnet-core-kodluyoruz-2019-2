using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application
{
    public class ApplicationResult<T> : CommonApplicationResult
    {
        public T Result { get; set; }
    }
    public class ApplicationResult : CommonApplicationResult
    {

    }
    public class CommonApplicationResult
    {
        // public DateTime RequestTime { get; set; }
        // public DateTime ResponseTime { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

    }
}
