import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
import $ from 'jquery';

function onElementHeightChange(elm, callback)
{
  var lastHeight = elm.clientHeight, newHeight;
  var lastScrollY = window.pageYOffset, newScrollY;
  (function run()
  {
    newHeight = elm.clientHeight;
    if (lastHeight != newHeight)
      callback();
    lastHeight = newHeight;

    if (elm.onElementHeightChangeTimer)
      clearTimeout(elm.onElementHeightChangeTimer);

    elm.onElementHeightChangeTimer = setTimeout(run, 200);
  })();
}

var cMutationManager = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cMutationManager")
    ,
    constructor: function ()
    {
      cMutationManager.BaseObject.constructor.call(this);
      this.Receivers = [];
      var __This = this;
      /*
      onElementHeightChange(document.body, function ()
      {
        //alert('Body height changed');
        var __ScrollWidth = window.getScrollbarWidth();
        if (!window.isScrollVisible())
        {

          try
          {
            document.getElementById("pageheader").style.paddingRight = __ScrollWidth + "px";
            document.getElementById("footerStatus").style.marginRight = -1 * __ScrollWidth + "px";
            document.getElementById("footerInline").style.marginRight = __ScrollWidth + "px";
          }
          catch (_Ex)
          {
          }

          try
          {
            document.body.style.paddingRight = __ScrollWidth + "px";;
          }
          catch (_Ex)
          {
          }
        }
        else
        {
          try
          {
            document.getElementById("pageheader").style.paddingRight = "0px";
            document.getElementById("footerStatus").style.marginRight = "0px";
            document.getElementById("footerInline").style.marginRight = "0px";
          }
          catch (_Ex)
          {
          }

          try
          {
            document.body.style.paddingRight = "0px";;
          }
          catch (_Ex)
          {
          }
        }
      });
    */


      /*this.ResizeObserver = new ResizeObserver(entries =>
      {
        window.requestAnimationFrame(() =>
        {
          if (!Array.isArray(entries) || !entries.length)
          {
            return;
          }

          var __ScrollWidth = window.getScrollbarWidth();
          if (!window.isScrollVisible())
          {

            try
            {
              document.getElementById("pageheader").style.paddingRight = __ScrollWidth + "px";
              document.getElementById("footerStatus").style.marginRight = -1 * __ScrollWidth + "px";
              document.getElementById("footerInline").style.marginRight = __ScrollWidth + "px";
            }
            catch (_Ex)
            {
            }

            try
            {
              document.body.style.paddingRight = __ScrollWidth + "px";;
            }
            catch (_Ex)
            {
            }
          }
          else
          {
            try
            {
              document.getElementById("pageheader").style.paddingRight = "0px";
              document.getElementById("footerStatus").style.marginRight = "0px";
              document.getElementById("footerInline").style.marginRight = "0px";
            }
            catch (_Ex)
            {
            }

            try
            {
              document.body.style.paddingRight = "0px";;
            }
            catch (_Ex)
            {
            }
          }

        });
      });

      this.ResizeObserver.observe(document.body)*/

      this.MutationController = new MutationObserver(function (_Event, _MutationObserver)
      {
        /*try
        {
          document.getElementById("pageheader").style.paddingRight = "0px";
          document.getElementById("footerStatus").style.marginRight = "0px";
          document.getElementById("footerInline").style.marginRight = "0px";
        }
        catch (_Ex)
        {
        }

        try
        {
          document.body.style.paddingRight = "0px";;
        }
        catch (_Ex)
        {
        }*/

      /*  var __Found = false;

        for (var __Item in _Event[0].target.style)
        {
          if (_Event[0].target.style[__Item] == "overflow-x")
          {
            __Found = true;
          }
        }

        var __ScrollWidth = window.getScrollbarWidth();
        if (window.isScrollVisible() && !__Found)
        {
          try
          {
            document.getElementById("pageheader").style.paddingRight = "0px";
            document.getElementById("footerStatus").style.marginRight = "0px";
            document.getElementById("footerInline").style.marginRight = "0px";
          }
          catch (_Ex)
          {
          }

          try
          {
            document.body.style.paddingRight = "0px";;
          }
          catch (_Ex)
          {
          }
        }
        else if ((window.isScrollVisible() && __Found) || (!window.isScrollVisible() && !__Found) || (!window.isScrollVisible() && __Found))
        {
          try
          {
            document.getElementById("pageheader").style.paddingRight = __ScrollWidth + "px";
            document.getElementById("footerStatus").style.marginRight = -1*__ScrollWidth+ "px";
            document.getElementById("footerInline").style.marginRight = __ScrollWidth+ "px";
          }
          catch (_Ex)
          {
          }

          try
          {
            document.body.style.paddingRight = __ScrollWidth + "px";;
          }
          catch (_Ex)
          {
          }
        }*/
        //console.log(_Event.target);
      });
      this.HandleConnectToElement(document.getElementsByTagName("BODY")[0], { subtree: false, childList: false, attributes : true });
    }
    ,
    HandleConnectToElement: function (_Element, _Options)
    {
      this.Receivers.push({
        Element: _Element,
        Options: _Options
      });
      this.MutationController.observe(_Element, _Options);
    }
    ,
    Destroy: function ()
    {
      cMutationManager.BaseObject.Destroy.call(this);
    }
  }, {});

export default cMutationManager;







;
