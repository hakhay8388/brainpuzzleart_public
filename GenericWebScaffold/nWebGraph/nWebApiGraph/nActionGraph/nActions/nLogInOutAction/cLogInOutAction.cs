using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Core.GenericWebScaffold.Controllers;
using Data.GenericWebScaffold.nDataService.nDataManagers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction
{
	public class cLogInOutAction : cBaseActionWithProps<cLogInOutProps>, IActionWithProps<cLogInOutProps>
	{
		public cLogInOutAction(cApp _App, cWebGraph _WebGraph)
		   : base(_App, _WebGraph, ActionIDs.LogInOut)
		{
		}

		public override void Action(IController _Controller = null, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			cLogInOutProps __LogInOutProps = new cLogInOutProps();
			__LogInOutProps.LoginState = _Controller.ClientSession.IsLogined;
			__LogInOutProps.SessionID = _Controller.ClientSession.SessionID;

			__LogInOutProps.User = _Controller.ClientSession.User != null ? _Controller.ClientSession.User.ToDynamic() : null;

			JObject __JsonObject = __LogInOutProps.SerializeObject();

			if (__JsonObject["User"].HasValues)
			{

				__JsonObject["User"]["PerCreditCostTL"] = 0;
				__JsonObject["User"]["SubscriptionState"] = false;

				__JsonObject["User"]["Password"] = null;
				__JsonObject["User"]["PaymentCardDetailEntity"] = null;
				__JsonObject["User"]["PaymentIyzicoInstructionEntity"] = null;
				__JsonObject["User"]["UserPasswordChange"] = null;
				__JsonObject["User"]["UserPasswordChangeRequest"] = null;

				__JsonObject["User"]["Roles"] = JArray.FromObject(_Controller.ClientSession.User.Actor.GetValue().Roles.ToDynamicObjectList());
				__JsonObject["User"]["UserDetail"] = JObject.FromObject(_Controller.ClientSession.User.UserDetail.ToDynamic());
			}

			base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
		}

	}
}
