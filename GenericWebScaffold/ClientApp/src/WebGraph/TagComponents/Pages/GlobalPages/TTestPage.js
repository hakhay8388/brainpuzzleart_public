import React, { Component, Suspense } from "react";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TObject";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import {withStyles} from "@mui/styles";
import GlobalStyles from "../../../../ScriptStyles/GlobalStyles";
import TDataTable from "../../DataSourcedComponent/TDataTable";


import classNames from "classnames";
import Helmet from "react-helmet";
import MaterialTable from "material-table";



var TTestPage = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("TTestPage"),
    constructor: function (_Props) {
      TTestPage.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        bestLessons: null,
        lastSellers: null,
        mostPurchasedLessonList: null,
      };
    },
    AsyncLoad: function () {
      var __This = this;
      /*Actions.GetBestLessonList(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            bestLessons: _Data.Item.BestLessonList,
          });
        });
      });
      Actions.GetAllSellerWithLesson(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            lastSellers: _Data.Item.SellerListWithLesson,
          });
        });
      });
      Actions.GetMostPurchasedLessons(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            mostPurchasedLessonList: _Data.Item.LessonList,
          });
        });
      });*/
    },
    Destroy: function () {
      TTestPage.BaseObject.Destroy.call(this);
    },
    render() {
      const { classes } = this.props;

      return (
            <div style={{ maxWidth: "100%" }}>
          orta
        </div>
      )
    },
  },
  {}
);

export default withStyles(GlobalStyles)(TTestPage);
