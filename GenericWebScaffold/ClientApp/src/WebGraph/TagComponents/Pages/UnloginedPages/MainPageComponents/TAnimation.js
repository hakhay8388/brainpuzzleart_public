import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../../WebGraph/GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import RouteMapImage from "../../../../../assets/img/RouteMap.png";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';

import Tween from 'rc-tween-one';
import BackgroundBox from "../../../../../assets/animation_games/img/BackgroundBox.png";
import MainText from "../../../../../assets/img/MainText.png";

import Blue_Cube from "../../../../../assets/animation_games/img/Blue_Cube.png";
import Brown_Cube from "../../../../../assets/animation_games/img/Brown_Cube.png";
import Red_Cube from "../../../../../assets/animation_games/img/Red_Cube.png";
import Green_Cube from "../../../../../assets/animation_games/img/Green_Cube.png";
import DarkYellow_Cube from "../../../../../assets/animation_games/img/DarkYellow_Cube.png";
import Orange_Cube from "../../../../../assets/animation_games/img/Orange_Cube.png";
import Purple_Cube from "../../../../../assets/animation_games/img/Purple_Cube.png";

import Blue_Target from "../../../../../assets/animation_games/img/Blue_Target.png";
import Brown_Target from "../../../../../assets/animation_games/img/Brown_Target.png";
import Red_Target from "../../../../../assets/animation_games/img/Red_Target.png";
import Green_Target from "../../../../../assets/animation_games/img/Green_Target.png";
import DarkYellow_Target from "../../../../../assets/animation_games/img/DarkYellow_Target.png";
import Orange_Target from "../../../../../assets/animation_games/img/Orange_Target.png";
import Purple_Target from "../../../../../assets/animation_games/img/Purple_Target.png";


import UpArrow from "../../../../../assets/animation_games/img/UpArrow.png";



var TAnimation = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TAnimation" )
    ,
    constructor: function ( _Props )
    {
      TAnimation.BaseObject.constructor.call( this, _Props );
      this.GameJson = _Props.gameJson;
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TAnimation.BaseObject.Destroy.call( this );
    }
    ,
    HandleGetBackground:function()
    {
      //return "url(" + GameBackground + ")";
      return "url(" + BackgroundBox + ")";
    }
    ,
    HandleGetMainText:function()
    {
      //return "url(" + GameBackground + ")";
      return "url(" + MainText + ")";
    }
    ,
    HandleGetDirection:function(_Direction)
    {
      if (_Direction == "Up") return 0;
      if (_Direction == "Right") return 90;
      if (_Direction == "Down") return 180;
      return 270;
    }
    ,
    HandleGetBoxByColor:function(_Color)
    {
      if (_Color == "Blue") return "url(" + Blue_Cube + ")";
      if (_Color == "Brown") return "url(" + Brown_Cube + ")";
      if (_Color == "Red") return "url(" + Red_Cube + ")";
      if (_Color == "Green") return "url(" + Green_Cube + ")";
      if (_Color == "DarkYellow") return "url(" + DarkYellow_Cube + ")";
      if (_Color == "Purple") return "url(" + Purple_Cube + ")";      
      return "url(" + Orange_Cube + ")";
    }
    ,
    HandleGetTargetByColor:function(_Color)
    {
      if (_Color == "Blue") return "url(" + Blue_Target + ")";
      if (_Color == "Brown") return "url(" + Brown_Target + ")";
      if (_Color == "Red") return "url(" + Red_Target + ")";
      if (_Color == "Green") return "url(" + Green_Target + ")";
      if (_Color == "DarkYellow") return "url(" + DarkYellow_Target + ")";
      if (_Color == "Purple") return "url(" + Purple_Target + ")";      
      return "url(" + Orange_Target + ")";
    }
    ,
    HandleGetAnimation : function(_Color, _Direction)
    {
        var __Animation = [];
        for (var i = 0 ; i < this.GameJson.Steps.length;i++)
        {
            var __Positions = this.GameJson.Steps[i].BoxesPosition[_Color];
            __Animation.push({ x:this.props.boxSize * __Positions.Column, y:this.props.boxSize * __Positions.Row, duration: 1000 });
        }

        for (var i = 0 ; i < this.GameJson.Targets.length;i++)
        {
          if (this.GameJson.Targets[i].Color == _Color)
          {
            __Animation.push({ x:this.props.boxSize * this.GameJson.Targets[i].Column, y:this.props.boxSize * this.GameJson.Targets[i].Row, duration: 1000 });
            break;
          }
        }

        var __StartPosition = {};
        for (var i = 0 ; i < this.GameJson.Boxes.length;i++)
        {
          if (this.GameJson.Boxes[i].Color == _Color)
          {
            __StartPosition.Row = this.GameJson.Boxes[i].Row;
            __StartPosition.Column = this.GameJson.Boxes[i].Column;
            break;
          }
        }

        return <Tween animation={__Animation}
        repeat={-1}
        /*paused={this.state.paused}
        reverse={this.state.reverse}
        reverseDelay={this.state.reverseDelay}
        moment={this.state.moment}*/
        style={{
          position: 'absolute',
          height: this.props.boxSize,
          width: this.props.boxSize, 
          transform: "translate(" +  (__StartPosition.Column * this.props.boxSize) + "px," + (__StartPosition.Row * this.props.boxSize)+ "px)"
        }}

      >
        <div style={{ height: this.props.boxSize, width: this.props.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor(_Color) }}>
          <div style={{ height: this.props.boxSize, width: this.props.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection(_Direction) + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
          </div> 
        </div>
      </Tween>
    }
    ,
    HandleGetTarget : function(_TargetItem)
    {
      return  <Tween animation={[]}
      style={{
        position: 'absolute',
        height: this.props.boxSize,
        width: this.props.boxSize, 
        transform: "translate(" +  (_TargetItem.Column * this.props.boxSize) + "px," + (_TargetItem.Row * this.props.boxSize)+ "px)"
      }}>
      <div style={{ height: this.props.boxSize, width: this.props.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetTargetByColor(_TargetItem.Color) }}>
      </div>
      </Tween>
    }
    ,
    render() {
      const { classes } = this.props;
      
      var __Height = this.GameJson.MatrixRowCount * this.props.boxSize;
      var __Width = this.GameJson.MatrixColumnCount * this.props.boxSize;

      return (
        <div>
          <Grid
            container
            direction="column"
            justifyContent="center"
            alignItems="center"
          >
            <Grid item>
            <div style={{ backgroundRepeat: "repeat",  backgroundSize: this.props.boxSize + "px " + this.props.boxSize + "px",  height: __Height, width: __Width, backgroundImage: this.HandleGetBackground() }}>
              { this.GameJson.Targets.map((_TargetItem) => {
                return this.HandleGetTarget(_TargetItem);
              })}
              { this.GameJson.Boxes.map((_BoxItem) => {
                return this.HandleGetAnimation(_BoxItem.Color, _BoxItem.Direction);
              })}

            </div>
            {this.props.showMainText &&
              <div style={{ backgroundRepeat: "no-repeat",position:"absolute", height: __Height, width: __Width, transform: "translate(0px,-" + __Height+ "px)", backgroundImage: this.HandleGetMainText(), backgroundSize: __Width + "px " +(__Width/3) + "px"  }}>
              </div>
            }
            </Grid>
          </Grid>
          
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TAnimation);


