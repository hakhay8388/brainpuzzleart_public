import React, { Component } from 'react';
import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class"
import cActionID from "./cActionID"
import $ from 'jquery';

var ActionIDs_Class = Class(Component,
  {
    ActionIDList: null,
    constructor: function ()
    {
      ActionIDs_Class.BaseObject.constructor.call(this);
      this.ActionIDList = new cListForBase();
      var __ActionList = null;

      $.ajax(
        {
          async: false,
          type: 'POST',
          url: '/api/WebApi/WebApi',
          dataType: "json",
          data: JSON.stringify(
            {
              CID: 2,
              Data: {
              }
            }),
          success: function (_Data)
          {
            __ActionList = _Data[0].Data.ActionList;
          },
          error: function () {
          },
          complete: function () {
          }
        });

      var EActionID = cActionID;
      for (var i = 0; i < __ActionList.length; i++)
      {
        var __Parameters = "[";

        for (var j = 0; j < __ActionList[i].Parameters.length; j++)
        {
          if (j > 0) __Parameters += ", ";
          __Parameters += "\"" + __ActionList[i].Parameters[j] + "\"";
        }
        __Parameters += "]";

        var __Eval = "this." + __ActionList[i].Name + "Action = new EActionID(" + __ActionList[i].ID.toString() + ", \"" + __ActionList[i].Name + "\", \"" + __ActionList[i].Info + "\", " + __ActionList[i].Enabled.toString() + ", " + __Parameters + ")";
        eval(__Eval)
        __Eval = "this.ActionIDList.Add(this." + __ActionList[i].Name + "Action)";
        eval(__Eval)
      }

      //this.ShowMessageCommand = new cCommandID(7, "ShowMessage", true, "")
      //this.CommandIDList.Add(this.ShowMessageCommand);
    }
    ,
    Destroy: function () {
      this.ActionIDList.DestroyWithItems();
      ActionIDs_Class.BaseObject.Destroy.call(this);
    }
  }, {});

export const ActionIDs = new ActionIDs_Class();

