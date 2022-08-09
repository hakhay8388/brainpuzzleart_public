import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../../WebGraph/GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import RouteMapImage from "../../../../../assets/img/RouteMap.png";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';



var TRouteMap = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TRouteMap" )
    ,
    constructor: function ( _Props )
    {
      TRouteMap.BaseObject.constructor.call( this, _Props );
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TRouteMap.BaseObject.Destroy.call( this );
    }
    ,
    render() {
      const { classes } = this.props;

      return (
        <div id="TRouteMap">
          <Grid
          style={{marginTop:50}}
            container
            direction="column"
            justifyContent="center"
            alignItems="center"
          >
             <Grid item >
              <Typography
              style={{
                align:"left",
                color : "#f8f5e5",
                fontFamily: 'Hemi Head FourTwentySix',
              }}
              variant={GenericWebGraph.MainContainerSize.Width < 600 ?  "h2" : "h1"}>RouteMap</Typography>
            </Grid>
            <Grid item>
              <img style={{objectFit: "contain", maxWidth : "800px", width : "100%"}} src={RouteMapImage} alt="..." />
            </Grid>
          </Grid>
          
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TRouteMap);


