import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";


var cMenuManager = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cMenuManager")
    ,
    constructor: function ()
    {
      cMenuManager.BaseObject.constructor.call(this);
    }
    ,
    Destroy: function ()
    {
      cMenuManager.BaseObject.Destroy.call(this);
    }
  }, {});

export default cMenuManager;







