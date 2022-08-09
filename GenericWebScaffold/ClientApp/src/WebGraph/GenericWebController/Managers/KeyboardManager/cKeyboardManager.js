import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";


var cKeyboardManager = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cKeyboardManager")
    ,
    constructor: function ()
    {
      cKeyboardManager.BaseObject.constructor.call(this);
      this.ConnectedList = new cListForBase();
      window.document.addEventListener('keyup', this.HandleKeyPressed);
    }
    ,
    ConnectKeypress: function (_Object)
    {
      this.ConnectedList.Add(_Object);
    }
    ,
    DisconnectKeypress: function (_Object)
    {
      this.ConnectedList.RemoveAll(__Item => __Item == _Object);
    }
    ,
    HandleKeyPressed: function (_Event)
    {
      if (JSTypeOperator.IsArray(_Event.path) && _Event.path.length > 1)
      {
        for (var i = 0; i < this.ConnectedList.Count(); i++)
        {
          var __TItem = this.ConnectedList.GetItem(i);
          var __Temp = _Event.path[1].attributes["keyowner"];
          //console.log(__TItem.GetObjectType().ObjectName);
          if (__Temp && __Temp.value == __TItem.GetObjectType().ObjectName)
          {
            if (JSTypeOperator.IsFunction(__TItem["VK_" + _Event.key]))
            {
              var __Result = __TItem["VK_" + _Event.key](_Event);
              if (__Result)
              {
                break;
              }
            }
          }
        }

      }
    }
    ,
    Destroy: function ()
    {
      cKeyboardManager.BaseObject.Destroy.call(this);
    }
  }, {});

export default cKeyboardManager;







