import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject";
import cMenuManager from "./MenuManager/cMenuManager";
import cLanguageManager from "./LanguageManager/cLanguageManager";
import cKeyboardManager from "./KeyboardManager/cKeyboardManager";
import cMutationManager from "./MutationManager/cMutationManager";


var cManagers = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cManagers")
    ,
    constructor: function () {
      cManagers.BaseObject.constructor.call(this);
      this.InitManagers();
    }
    ,
    InitManagers: function () {
      this.MenuManager = new cMenuManager();
      this.LanguageManager = new cLanguageManager();
      this.KeyboardManager = new cKeyboardManager();
      this.MutationManager = new cMutationManager();
    }
    ,
    Destroy: function () {
      cManagers.BaseObject.Destroy.call(this);
    }
  }, {});

export default cManagers;







