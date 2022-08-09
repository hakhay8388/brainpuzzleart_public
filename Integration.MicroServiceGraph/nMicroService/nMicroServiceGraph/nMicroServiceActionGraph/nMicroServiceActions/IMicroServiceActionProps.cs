using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions
{
    public interface IMicroServiceActionProps
    {
		string Host { get; set; }
		JObject SerializeObject();
    }
}

