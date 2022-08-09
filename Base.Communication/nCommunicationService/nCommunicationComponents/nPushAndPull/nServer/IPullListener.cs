using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPushAndPull.nServer
{
	public interface IPullListener
	{
		string ReceivePullData(cPullNode _PullNode, string _Message);
	}
}
