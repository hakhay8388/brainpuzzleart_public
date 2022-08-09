using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction
{
    public class cProgressStatusAction : cBaseActionWithProps<cProgressStatusProps>, IActionWithProps<cProgressStatusProps>
    {
		private List<cProgressItem> ProgressItems = null;

		public cProgressStatusAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ProgressStatus)
        {
			ProgressItems = new List<cProgressItem>();
		}

		public override void Action(IController _Controller, cProgressStatusProps _ActionData, List<cSession> _SignalSessions, bool _ApplyWaitSeconds)
		{
			if (_ApplyWaitSeconds)
			{
				List<cSession> __List = CreateProgressItems(_SignalSessions);
				if (__List.Count > 0)
				{
					base.Action(_Controller, _ActionData.SerializeObject(), _SignalSessions, true);
				}
			}
			else
			{
				base.Action(_Controller, _ActionData.SerializeObject(), _SignalSessions, true);
			}
		}

		public cProgressItem GetProgressBySession(cSession _Session)
		{
			lock (ProgressItems)
			{
				return ProgressItems.Where(__Item => __Item.Session.SessionID == _Session.SessionID).ToList().FirstOrDefault();
			}
		}

		public List<cSession> CreateProgressItems(List<cSession> _SignalSessions)
		{
			lock (ProgressItems)
			{
				ClearUnusedSession();
				List<cSession> __Result = new List<cSession>();

				for (int i = 0; i < _SignalSessions.Count; i++)
				{
					cProgressItem __Item = GetProgressBySession(_SignalSessions[i]);
					if (__Item != null)
					{
						if (__Item.CreateTime.AddSeconds(10) < DateTime.Now)
						{
							__Result.Add(__Item.Session);
							__Item.RefreshValue();
						}
					}
					else
					{
						__Item = new cProgressItem(_SignalSessions[i]);
						__Item.RefreshValue();
						ProgressItems.Add(__Item);
						__Result.Add(__Item.Session);
					}
				} 

				return __Result;
			}
		}

		private void ClearUnusedSession()
		{
			lock (ProgressItems)
			{
				ProgressItems.RemoveAll(__Item => __Item.CreateTime.AddMinutes(1) < DateTime.Now);
			}
		}
	}
}
