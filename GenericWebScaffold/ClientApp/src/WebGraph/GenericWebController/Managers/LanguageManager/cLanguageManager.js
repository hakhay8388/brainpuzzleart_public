import React, { Component } from "react";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
import $ from "jquery";
import queryString from "query-string";

var cLanguageManager = Class(
  cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cLanguageManager"),
    constructor: function () {
      cLanguageManager.BaseObject.constructor.call(this);
      this.SetLanguage = this.SetLanguage.bind(this);

      var __LanguageCode = window.GetLanguageCodeFromUrl();
      if (__LanguageCode.length == 2) {
        this.FirstLoadSetLanguage(__LanguageCode, true);
      } else {
        this.FirstLoadSetLanguage();
      }
    },
    HandleSetActiveLanguage: function (_Language) {
      this.ActiveLanguage = {};
      this.DefinedLanguages = {}; 
      this.ActiveLanguage.LanguageCode = _Language.LanguageCode;
      this.DefinedLanguages = _Language.DefinedLanguages;

      for (var __Item in _Language.Language) {
        this.ActiveLanguage[__Item] = _Language.Language[__Item].message;
      }
    },
    SetLanguage(_LanguageCode) {
      if (this.ActiveLanguage.LanguageCode != _LanguageCode) {
        var __Host = window.GetHost();
        var __Result = "";
        if ((window.GetUrl() + window.GetUrlParams()).left(3).right(1) == "/") {
          var __UrlLength = (window.GetUrl() + window.GetUrlParams()).length;
          __Result =
            _LanguageCode +
            "/" +
            (window.GetUrl() + window.GetUrlParams()).right(__UrlLength - 3);
        } else {
          var __UrlLength = (window.GetUrl() + window.GetUrlParams()).length;
          __Result =
            _LanguageCode + "/" + (window.GetUrl() + window.GetUrlParams());
        }
        window.location.href = window.GetHostHttp() + __Host + "/" + __Result;
      }
    },
    FirstLoadSetLanguage(_LanguageCode) {
      var __Url = window.GetUrlParams();
      var __Params = queryString.parse(__Url);
      var __ForcedLanguage = "";
      if (__Params.lang) {
        __ForcedLanguage = __Params.lang;
      }

      var __Language = null;
      var __This = this;
      $.ajax({
        async: false,
        type: "POST",
        url: "/api/WebApi/WebApi",
        dataType: "json",
        data: JSON.stringify({
          CID: 3,
          Data: {
            LanguageCode:
              __ForcedLanguage != ""
                ? __ForcedLanguage
                : _LanguageCode && _LanguageCode != null
                ? _LanguageCode
                : "",
          },
        }),
        success: function (_Data) {
          __Language = _Data[0].Data;
        },
        error: function () {},
        complete: function () {},
      });

      this.HandleSetActiveLanguage(__Language);
      /*this.ActiveLanguage = {};
      this.ActiveLanguage.LanguageCode = __Language.LanguageCode;

      for (var __Item in __Language.Language) {
        this.ActiveLanguage[__Item] = __Language.Language[__Item].message;
      }
*/
      /*
        if (_LanguageCode)
        {
          window.ClearPages()
          Pages.LoadPages(function ()
          {
            WebGraph.ForceUpdateAllWithAsyncLoad(true);
          });
        }
        else
        {
          WebGraph.ForceUpdateAllWithAsyncLoad(true);
        }
        */
    },
    Destroy: function () {
      cLanguageManager.BaseObject.Destroy.call(this);
    },
  },
  {}
);

export default cLanguageManager;
