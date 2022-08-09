using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nServer
{
	public interface IServerListener
	{
		string ReceiveServerData(cServerNode _ServerNode, string _Message);
	}
}
