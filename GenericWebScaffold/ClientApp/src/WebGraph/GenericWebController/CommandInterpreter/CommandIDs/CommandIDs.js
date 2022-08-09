import React, { Component } from "react";
import {
  JSTypeOperator,
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  cListForBase,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
import cCommandID from "./cCommandID";
import $ from "jquery";

var CommandIDs_Class = Class(
  Component,
  {
    CommandIDList: null,
    constructor: function () {
      CommandIDs_Class.BaseObject.constructor.call(this);
      this.CommandIDList = new cListForBase();
      //var __Temp = this.NewFetch();
      var __CommandList = null;
      $.ajax({
        async: false,
        type: "POST",
        url: "/api/WebApi/WebApi",
        dataType: "json",
        data: JSON.stringify({
          CID: 1,
          Data: {},
        }),
        success: function (_Data) {
          __CommandList = _Data[0].Data.CommandList;
        },
        error: function (xhr, status, error) {
          var errorMessage =
            xhr.status + ": " + xhr.statusText + ":" + xhr.responseText;
          DebugAlert.Show("Error - " + errorMessage);
        },
        complete: function () {},
      });

      var ECommandID = cCommandID;
      for (var i = 0; i < __CommandList.length; i++) {
        var __Eval =
          "this." +
          __CommandList[i].Name +
          "Command = new ECommandID(" +
          __CommandList[i].ID.toString() +
          ', "' +
          __CommandList[i].Name +
          '", "' +
          __CommandList[i].Info +
          '", ' +
          __CommandList[i].Enabled.toString() +
          ")";
        eval(__Eval);
        __Eval =
          "this.CommandIDList.Add(this." + __CommandList[i].Name + "Command)";
        eval(__Eval);
      }

      //this.ShowMessageCommand = new cCommandID(7, "ShowMessage", true, "")
      //this.CommandIDList.Add(this.ShowMessageCommand);
    },
    Destroy: function () {
      this.CommandIDList.DestroyWithItems();
      CommandIDs_Class.BaseObject.Destroy.call(this);
    },
  },
  {}
);

export const CommandIDs = new CommandIDs_Class();
