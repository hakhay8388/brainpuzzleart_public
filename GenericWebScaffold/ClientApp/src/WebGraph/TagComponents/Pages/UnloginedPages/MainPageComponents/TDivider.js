import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import TAnimation from "./TAnimation";
import Divider_Steps from "../../../../../assets/animation_games/Divider_Steps.json";
import Divider_StepsGreen from "../../../../../assets/animation_games/Divider_StepsGreen.json";
import Divider_StepsDarkYellow from "../../../../../assets/animation_games/Divider_StepsDarkYellow.json";

import { ThreeSixty } from '@mui/icons-material';




var TDivider = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TDivider" )
    ,
    constructor: function ( _Props )
    {
      TDivider.BaseObject.constructor.call( this, _Props );
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TDivider.BaseObject.Destroy.call( this );
    }
    ,
    HandleGetDividerColor : function()
    {
      if (this.props.color == "Green") return <TAnimation gameJson={Divider_StepsGreen} boxSize={50}/>;
      if (this.props.color == "DarkYellow") return <TAnimation gameJson={Divider_StepsDarkYellow} boxSize={50}/>;
      
      return <TAnimation gameJson={Divider_Steps} boxSize={50}/>;
    }
    ,
    render() {
      const { classes } = this.props;

      return (
        <div style={{paddingTop:"50px"}}>
          {this.HandleGetDividerColor()}
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TDivider);


