using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSellerRegisterCommand
{
    public interface ISellerRegisterReceiver : ICommandReceiver
    {
        void ReceiveSellerRegisterData(cListenerEvent _ListenerEvent, IController _Controller, cSellerRegisterCommandData _ReceivedData);

    }
}