using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.MicroServiceGraph.nMicroService;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nGeneralListener
{
    public class cPermissionListener : cBaseListener, IGetMenuListReceiver, IGetPageListReceiver
    {
        public cActorDataManager ActorDataManager { get; set; }
        public cMenuDataManager MenuDataManager { get; set; }

        public cPermissionListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, cActorDataManager _ActorDataManager, cMenuDataManager _MenuDataManager)
          : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
            ActorDataManager = _ActorDataManager;
            MenuDataManager = _MenuDataManager;
        }

        public void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData)
        {
			if (_Controller.ClientSession.IsLogined)
			{
				IDataService __DataService = DataServiceManager.GetDataService();
				List<cMenuEntity> __Menus = ActorDataManager.GetMenuByActor(_Controller.ClientSession.User.Actor.GetValue(), _ReceivedData.MenuTypeCode, _ReceivedData.RootMenuCode);

				List<object> __ResultList = new List<object>();
				foreach (cMenuEntity __MenuEntity in __Menus)
				{
					MenuIDs __MenuID = MenuIDs.GetByCode(__MenuEntity.Code, null);
					if (__MenuID != null)
					{
						if (__MenuID.IsMainMenu)
						{
							List<dynamic> __SubMenus = ActorDataManager.GetMenuByActorToDynamicList(
								_Controller.ClientSession.User.Actor.GetValue(), MenuTypes.CenterMenu.Code, __MenuID.Code,
								(_Item) =>
								{
									cPageEntity __Page = __MenuEntity.Page.GetValue();
									
									_Item.icon = _Item.Icon;
									_Item.name = _Item.Name;
									_Item.mainMenu = false;
									_Item.subMenu = new object[]{};
									_Item.Active = false;
									_Item.url = _Item.Url;
								});

							__ResultList.Add(new
							{
								icon = __MenuID.Icon,
								name = __MenuID.Name,
								mainMenu = true,
								subMenu = __SubMenus
							});
						}
						else
						{
							cPageEntity __Page = __MenuEntity.Page.GetValue();
							__ResultList.Add(new
							{
								url = __Page.Url,
								icon = __MenuEntity.Icon,
								name = __MenuEntity.Name,
								mainMenu = false,
								subMenu = new object[]{}
							});
						}
					}
				}

				WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __ResultList, Page = 1, Total = __ResultList.Count });
			}
			else
			{

				List<object> __ResultList = new List<object>();
				foreach (MenuIDs __MenuEntity in MenuIDs.TypeList)
				{
					if (__MenuEntity.UnloginedPage)
					{
						if (__MenuEntity.IsMainMenu)
						{
							__ResultList.Add(new
							{
								url = "menupage/" + __MenuEntity.Code,
								icon = __MenuEntity.Icon,
								name = __MenuEntity.Name
							});
						}
						else
						{
							PageIDs __PageID = PageIDs.GetByCode(__MenuEntity.Code, PageIDs.MainPage);
							__ResultList.Add(new
							{
								//url = __PageID.Code == PageIDs.MainPage.Code ? "global" : __PageID.Url,
								url = __PageID.Url,
								icon = __MenuEntity.Icon,
								name = __MenuEntity.Name
							});
						}





					}
				}

				WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __ResultList, Page = 1, Total = __ResultList.Count });
			}
        }

        public void ReceiveGetPageListData(cListenerEvent _ListenerEvent, IController _Controller, cGetPageListCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                List<cPageEntity> __Pages = ActorDataManager.GetPageByActor(_Controller.ClientSession.User.Actor.GetValue());

                List<object> __ResultList = new List<object>();
                foreach (cPageEntity __Page in __Pages)
                {
                    __ResultList.Add(new
                    {
                        path = __Page.Url,
                        name = __Page.Name,
                        originalCode = PageIDs.GetByCode(__Page.Code, PageIDs.MainPage).OriginalCode,
                        subParamName = PageIDs.GetByCode(__Page.Code, PageIDs.MainPage).SubParamName,
                        component = __Page.ComponentName
                    });
                }

                WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __ResultList, Page = 1, Total = __ResultList.Count });
            }
            else
            {

                List<object> __ResultList = new List<object>();
                foreach (PageIDs __Page in PageIDs.TypeList)
                {
                    if (__Page.UnloginedPage)
                    {
                        __ResultList.Add(new
                        {
                            path = __Page.Url,
                            name = __Page.Name,
                            originalCode = __Page.OriginalCode,
                            subParamName = PageIDs.GetByCode(__Page.Code, PageIDs.MainPage).SubParamName,
                            component = __Page.Component
                        });
                    }
                }

                WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __ResultList, Page = 1, Total = __ResultList.Count });
            }
        }
    }
}
