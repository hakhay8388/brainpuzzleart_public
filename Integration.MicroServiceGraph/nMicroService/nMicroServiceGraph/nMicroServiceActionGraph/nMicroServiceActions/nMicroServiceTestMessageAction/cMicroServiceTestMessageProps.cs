using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceTestMessageAction
{
    public class cMicroServiceTestMessageProps : cMicroServiceBaseProps
    {
        public virtual  string Message { get; set; }
        public cMicroServiceTestMessageProps(string _Message)
            :base()
        {
			Message = _Message;
        }

		public cMicroServiceTestMessageProps()
			: base()
		{ 
		}
	}
}
