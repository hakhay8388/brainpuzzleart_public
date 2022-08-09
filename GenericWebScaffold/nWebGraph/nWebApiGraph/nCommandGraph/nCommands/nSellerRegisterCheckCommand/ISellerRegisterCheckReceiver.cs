using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCheckCommand
{
    public interface ISellerRegisterCheckReceiver : ICommandReceiver
    {
        void ReceiveSellerRegisterCheckData(cListenerEvent _ListenerEvent, IController _Controller, cSellerRegisterCheckCommandData _ReceivedData);

    }
}