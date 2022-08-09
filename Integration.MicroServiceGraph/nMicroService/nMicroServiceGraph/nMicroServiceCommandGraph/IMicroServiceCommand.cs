using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph
{
	public interface IMicroServiceCommand
	{
		MicroServiceIDs MicroServiceID { get; set; }
		void Interpret(cMicroServiceApi _MicroServiceApi, JToken _JsonObject);

	}
}
