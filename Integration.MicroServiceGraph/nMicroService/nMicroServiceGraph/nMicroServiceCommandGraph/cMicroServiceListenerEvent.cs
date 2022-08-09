using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph
{
    public class cMicroServiceListenerEvent
    {
        public IMicroServiceCommand MicroServiceCommand { get; private set; }
        public bool IsPropogationStoped { get; private set; }
        public Dictionary<string, object> Values { get; private set; }

        public cMicroServiceListenerEvent(IMicroServiceCommand _MicroServiceCommand)
        {
            MicroServiceCommand = _MicroServiceCommand;
            IsPropogationStoped = false;
            Values = new Dictionary<string, object>();
        }

        public void StopPropogation()
        {
            IsPropogationStoped = true;
        }
    }
}
