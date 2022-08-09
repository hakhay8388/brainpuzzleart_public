import React, { Component, Suspense } from "react";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TObject";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import {withStyles} from "@mui/styles";
import MainPageStyles from "../../../../ScriptStyles/MainPageStyles";
import TDataTable from "../../DataSourcedComponent/TDataTable";

import classNames from "classnames";
import Helmet from "react-helmet";
import MaterialTable from "material-table";
import Grid from '@mui/material/Grid';


import TTeam from "./MainPageComponents/TTeam";
import TAbout from "./MainPageComponents/TAbout";
import TDivider from "./MainPageComponents/TDivider";
import TRouteMap from "./MainPageComponents/TRouteMap";
import TAnimation from "./MainPageComponents/TAnimation";
import TFaq from "./MainPageComponents/TFaq";

import Big_Steps from "../../../../assets/animation_games/Big_Steps.json";
import Small_Steps from "../../../../assets/animation_games/Small_Steps.json";




var TMainPage = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("TMainPage"),
    constructor: function (_Props) {
      TMainPage.BaseObject.constructor.call(this, _Props);
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
      TMainPage.BaseObject.Destroy.call(this);
    },
    render() {
      const { classes } = this.props;

      console.log(GenericWebGraph.MainContainerSize);

      return (
            <div style={{ maxWidth: "800" }}>
               <Grid container
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    >
                    <Grid item xs={12} className={classNames(
                      classes.puzzleBig
                    )} >
                        <TAnimation gameJson={Big_Steps} showMainText={true} boxSize={GenericWebGraph.MainContainerSize.Width < 1000 ? GenericWebGraph.MainContainerSize.Width/15 : GenericWebGraph.MainContainerSize.Width/19}/>
                    </Grid>
                    <Grid item xs={12} className={classNames(
                      classes.puzzleSmall
                    )} >
                        <TAnimation gameJson={Small_Steps} boxSize={50} showMainText={true}/>
                    </Grid>
              </Grid>
              
              
              <TAbout />
              <TDivider />
              <TRouteMap />
              <TDivider color="Green"/>
              <TTeam />
              <TDivider color="DarkYellow"/>
              <TFaq />
              <TDivider />
        </div>
      )
    },
  },
  {}
);

export default withStyles(MainPageStyles)(TMainPage);
