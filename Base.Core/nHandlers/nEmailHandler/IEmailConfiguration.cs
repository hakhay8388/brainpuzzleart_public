using Base.Core.nApplication;
using Base.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Base.Core.nHandlers.nEmailHandler
{
    public interface IEmailConfiguration
    {
        cEmailConfiguration GetEmailConfiguration();
    }
}
