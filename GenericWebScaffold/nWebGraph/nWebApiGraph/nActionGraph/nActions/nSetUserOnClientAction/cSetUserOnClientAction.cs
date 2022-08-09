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

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetUserOnClientAction
{
    public class cSetUserOnClientAction : cBaseAction, IActionWithoutProps
    {
        public cSetUserOnClientAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.SetUserOnClient)
        {

        }

        public override void Action(IController _Controller = null, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            cSetUserOnClientProps __LogInOutProps = new cSetUserOnClientProps();

            __LogInOutProps.User = _Controller.ClientSession.User.ToDynamic();

            JObject __JsonObject = __LogInOutProps.SerializeObject();
            if (__JsonObject["User"].HasValues)
            {
                SetJObjectContent(_Controller, __JsonObject);

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

        private void SetJObjectContent(IController _Controller, JObject __JsonObject)
        {

            __JsonObject["User"]["PerCreditCostTL"] = 0;
            __JsonObject["User"]["SubscriptionState"] = false;
        }

        public void Action(IController _Controller, cUserEntity _UserEntity, List<cSession> _SignalSessions)
        {
            cSetUserOnClientProps __LogInOutProps = new cSetUserOnClientProps();

            __LogInOutProps.User = _UserEntity.ToDynamic();

            JObject __JsonObject = __LogInOutProps.SerializeObject();
            if (__JsonObject["User"].HasValues)
            {
                SetJObjectContent(_Controller, __JsonObject);
                __JsonObject["User"]["Password"] = null;
                __JsonObject["User"]["PaymentCardDetailEntity"] = null;
                __JsonObject["User"]["PaymentIyzicoInstructionEntity"] = null;
                __JsonObject["User"]["UserPasswordChange"] = null;
                __JsonObject["User"]["UserPasswordChangeRequest"] = null;

                __JsonObject["User"]["Roles"] = JArray.FromObject(_UserEntity.Actor.GetValue().Roles.ToDynamicObjectList());
                __JsonObject["User"]["UserDetail"] = JObject.FromObject(_UserEntity.UserDetail.ToDynamic());
            }

            base.Action(_Controller, __JsonObject, _SignalSessions, true);
        }

    }
}
