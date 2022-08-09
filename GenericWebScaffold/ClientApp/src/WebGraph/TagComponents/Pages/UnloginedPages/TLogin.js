import React from "react";

import GenericWebGraph from "../../../../WebGraph/GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  Class,
  ObjectTypes,
} from "../../../../WebGraph/GenericCoreGraph/ClassFramework/Class";

import { withStyles } from "@mui/styles";

import TObject from "../../../../WebGraph/TagComponents/TObject";
import Actions from "../../../../WebGraph/GenericWebController/ActionGraph/Actions";

import GlobalStyles from "../../../../ScriptStyles/GlobalStyles";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';


var TLogin = Class(
  TObject,
  CommandInterfaces.ILogInOutCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("TLogin"),
    constructor: function (_Props) {
      TLogin.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        ButtonEnabled: true,
        UserName: "",
        Password: "",
        StaySession: false,
        selectedTimezone: null,
        showPassword: false,
      };
      document.addEventListener("keydown", this.HandleOnKeyUp);
    },
    AsyncLoad: function () {
      TLogin.BaseObject.AsyncLoad.call(this);
    },
    componentWillMount: function () {
      TLogin.BaseObject.componentWillMount.call(this);
      if (window.App.User != null) {
        GenericWebGraph.GoMainPage();
      }
    },
    Destroy: function () {
      TLogin.BaseObject.Destroy.call(this);
    },
    Receive_LogInOutCommand: function (_Data) {
      if (!_Data.LoginState) {
        this.setState({
          ButtonEnabled: true,
        });
      }
    },
    HandleSubmit(event) {
      event.preventDefault();
      var __This = this;
      this.setState(
        {
          ButtonEnabled: false,
        },
        () => {
          Actions.Login(
            this.state.UserName,
            this.state.Password,
            this.state.StaySession,
            function (_Message) {
              CommandIDs.SuccessResultCommand.RunIfNotHas(
                _Message,
                function (_Data) {
                  __This.setState({
                    ButtonEnabled: true,
                  });
                }
              );
              CommandIDs.SuccessResultCommand.RunIfHas(
                _Message,
                function (_Data) {
                  __This.setState({
                    ButtonEnabled: false,
                  });
                }
              );
            }
          );
        }
      );
    },
    render() {
      const { classes } = this.props;
      var __This = this;
      return (
          <Grid
              container
              direction="row"
              justifyContent="center"
              alignItems="center"
          >
              <Grid item>
                  <Typography variant="h1" component="h2">
                    h1. Heading
                  </Typography>;
              </Grid>
        </Grid>
      );
    },
  },
  {}
);

export default withStyles(GlobalStyles)(TLogin);
