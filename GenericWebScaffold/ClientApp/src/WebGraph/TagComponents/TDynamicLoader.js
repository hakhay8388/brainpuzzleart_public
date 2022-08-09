import React, { Component } from 'react';


import GenericWebGraph from "../../WebGraph/GenericWebController/GenericWebGraph"
import { CommandInterfaces } from "../GenericWebController/CommandInterpreter/cCommandInterpreter"
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../WebGraph/GenericCoreGraph/ClassFramework/Class"
import TObject from "../../WebGraph/TagComponents/TObject"
import Actions from "../../WebGraph/GenericWebController/ActionGraph/Actions"
import { CommandIDs } from "../../WebGraph/GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"
import { WebGraph } from "../../WebGraph/GenericCoreGraph/WebGraph/WebGraph"

import cCommandListenerGraph from "../../WebGraph/GenericWebController/CommandListenerGraph/cCommandListenerGraph"
import cManagersWithListener from "../../WebGraph/GenericWebController/ManagersWithListener/cManagersWithListener"

var TDynamicLoader = Class(TObject,
  {
    ObjectType: ObjectTypes.Get("TDynamicLoader")
    ,
    constructor: function (_Props)
    {
      TDynamicLoader.BaseObject.constructor.call(this, _Props);
      GenericWebGraph.CommandListenerGraph = new cCommandListenerGraph();
      GenericWebGraph.ManagersWithListener = new cManagersWithListener();
    }
    ,
    Destroy: function ()
    {
      TDynamicLoader.BaseObject.Destroy.call(this);
    }
    ,
    render()
    {
      return (
        null
      );
    }
  }, {});

export default TDynamicLoader;
