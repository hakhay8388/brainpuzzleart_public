import React from "react";
import {  GlobalEval,  JSTypeOperator } from "../../WebGraph/GenericCoreGraph/ClassFramework/Class";
import Actions from "../../WebGraph/GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../WebGraph/GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../WebGraph/GenericWebController/GenericWebGraph";

const Pages = {};
Pages.Routes = [];

window.Pages = Pages;

window.ClearPages = function () 
{
  for (var __Item in Pages) 
  {
    delete Pages[__Item];
  }

  Pages.Routes = [];


  /*
   Örnek

  Pages.Routes.push(
    {
      path: "/register",
      name: "Register",
      component: Pages.TRegister
    });*/

  Pages.GetRouteString = function (_Item, _UsePageName) {
    var __Result = "";
    if (_UsePageName) {
      __Result = _Item.path;
      if (__Result.startsWith("/")) {
        __Result = __Result.substring(1);
      }
    }

    if (_Item.subParamName && JSTypeOperator.IsArray(_Item.subParamName)) {
      for (var i = 0; i < _Item.subParamName.length; i++) {
        __Result += "/:" + _Item.subParamName;
      }
      return __Result;
    }
    return __Result;
  };

  Pages.GetRoutePureName = function (_Item) {
    var __Result = "";
    __Result = _Item.path;
    if (__Result.startsWith("/")) {
      __Result = __Result.substring(1);
    }
    return __Result;
  };

  Pages.LoadPages = function (_CallbackFunction) {
    Pages.Names = {};
    Actions.GetPageList(function (_Message) {
      CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data) {
        Pages.Routes = [];

        
       /* Pages.Routes.push({
          path: "/",
          name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
            "HomePage"
          ],
          component: () => <div>Component Setlenemedi</div>,
          exact: true,
        });*/
        if (window.App.User == null) {
          Pages.Routes.push({
            path: "/",
            purepath: "/",
            exact: true,
            name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
              "HomePage"
            ],
            component: () => <div></div>,
          });
          _Data.ResultList.map(function (_Item, _Index) {
            var __ActiveLanguagePageName =
              GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                "PageRoute_" + _Item.name
              ];
            if (
              __ActiveLanguagePageName != undefined &&
              __ActiveLanguagePageName != null &&
              __ActiveLanguagePageName != ""
            ) {
              _Item.path = __ActiveLanguagePageName;
            }
            Pages.Names[_Item.originalCode] =
            GenericWebGraph.Managers.LanguageManager.ActiveLanguage.LanguageCode + "/" + _Item.path;
            Pages[_Item.component] = React.lazy(() =>
              import("./Pages/UnloginedPages/" + _Item.component).catch(() =>
                import("./Pages/GlobalPages/" + _Item.component)
              )
            );

            if (_Index == 0) {
              Pages.Routes[0] = {
                path: "/" + Pages.GetRouteString(_Item, false),
                purepath: "/",
                name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                  "HomePage"
                ],
                component: Pages[_Item.component],
                exact: true,
              };
            }

            Pages.Routes.push({
              path: "/" + Pages.GetRouteString(_Item, true),
              purepath: "/" + _Item.path,
              name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                _Item.name
              ],
              component: Pages[_Item.component],
              exact: true,
            });
            if (_Item.subParamName.length > 0) {
              Pages.Routes.push({
                path: "/" + Pages.GetRoutePureName(_Item),
                purepath: "/" + _Item.path,
                name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                  _Item.name
                ],
                component: Pages[_Item.component],
                exact: true,
              });
            }
          });
          if (JSTypeOperator.IsFunction(_CallbackFunction)) _CallbackFunction();
        } else {
          Pages.Routes.push({
            path: "/",
            purepath: "/",
            exact: true,
            name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
              "HomePage"
            ],
            component: () => <div></div>,
          });
          _Data.ResultList.map(function (_Item, _Index) {
            var __ActiveLanguagePageName =
              GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                "PageRoute_" + _Item.name
              ];
            if (
              __ActiveLanguagePageName != undefined &&
              __ActiveLanguagePageName != null &&
              __ActiveLanguagePageName != ""
            ) {
              _Item.path = __ActiveLanguagePageName;
            }
            Pages.Names[_Item.originalCode] =
            GenericWebGraph.Managers.LanguageManager.ActiveLanguage.LanguageCode + "/" + _Item.path;
            if (
              window.App &&
              window.App.User &&
              window.App.User.Roles.length > 0
            ) {
              Pages[_Item.component] = React.lazy(() =>
                import(
                  "./Pages/" +
                    window.App.User.Roles[0].Code +
                    "Pages/" +
                    _Item.component
                ).catch(() => import("./Pages/GlobalPages/" + _Item.component))
              );
              if (_Index == 0) {
                Pages.Routes[0] = {
                  path: "/" + Pages.GetRouteString(_Item, false),
                  purepath: "/",
                  name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                    "HomePage"
                  ],
                  component: Pages[_Item.component],
                  exact: true,
                };
              }
              Pages.Routes.push({
                path: "/" + Pages.GetRouteString(_Item, true),
                purepath: "/" + _Item.path,
                name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                  _Item.name
                ],
                component: Pages[_Item.component],
                exact: true,
              });

              if (
                _Item.subParamName &&
                JSTypeOperator.IsArray(_Item.subParamName)
              ) {
                Pages.Routes.push({
                  path: "/" + Pages.GetRoutePureName(_Item),
                  purepath: "/" + _Item.path,
                  name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[
                    _Item.name
                  ],
                  component: Pages[_Item.component],
                  exact: true,
                });
              }
            }
          });
          if (JSTypeOperator.IsFunction(_CallbackFunction)) _CallbackFunction();
        }
      });
    });
  };
};


export default Pages;
