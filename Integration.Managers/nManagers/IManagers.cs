using Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber;
using Integration.Managers.nManagers.nConfigBackupManager;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Managers.nManagers
{ 
	public interface IManagers
	{
		cConfigBackupManager ConfigBackupManager { get; set; }
	}
 
}
