import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import AboutImage from "../../../../../assets/img/About.png";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import HemiHead from "../../../../../assets/fonts/hemihead.ttf";
import Avatar from '@mui/material/Avatar';
import hayri from "../../../../../assets/img/hayri.jpg";
import suleyman from "../../../../../assets/img/suleyman.jpg";
import muhammed from "../../../../../assets/img/muhammed.jpg";
import hakan from "../../../../../assets/img/hakan.jpg";
import ilknur from "../../../../../assets/img/ilknur.jpg";
import LinkedInIcon from '@mui/icons-material/LinkedIn';
import IconButton from '@mui/material/IconButton';


var TTeam = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TTeam" )
    ,
    constructor: function ( _Props )
    {
      TTeam.BaseObject.constructor.call( this, _Props );
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TTeam.BaseObject.Destroy.call( this );
    }
    ,
    HandleOpenLinkedInHayri : function()
    {
      window.open("https://www.linkedin.com/in/hayri-ery%C3%BCrek-b29820172/", '_blank');
    }
    ,
    HandleOpenLinkedInSuleyman : function()
    {
      window.open("https://www.linkedin.com/in/s%C3%BCleyman-g%C3%BCven%C3%A7-25b18ab1/", '_blank');
    }
    ,
    HandleOpenLinkedInMuhammed : function()
    {
      window.open("https://www.linkedin.com/in/mozturkceng/", '_blank');
    }
    ,
    HandleOpenLinkedInHakan : function()
    {
      window.open("https://www.linkedin.com/in/mete-hakan-ery%C3%BCrek-659566237/", '_blank');
    }
    ,
    render() {
      const { classes } = this.props;

      return (
        <div id="TTeam" style={{paddingTop:"50px"}}>
          <Grid
            container
            direction="column"
            justifyContent="flex-start"
            alignItems="center"
          >
            <Grid item >
              <Typography
              style={{
                align:"left",
                color : "#f8f5e5",
                fontFamily: 'Hemi Head FourTwentySix',
              }}
              variant="h1">Team</Typography>
            </Grid>
            <Grid 
              style={{paddingTop:30}}
              item
             container
             direction="column"
             justifyContent="center"
             alignItems="center">
               <Grid item>
                <Avatar
                    alt="Remy Sharp"
                    src={hayri}
                    sx={{ width: 200, height: 200 }}
                  />
               </Grid>
               <Grid item style={{paddingTop:20}}>
                  <Typography
                style={{
                  align:"left",
                  color : "#f8f5e5",
                  fontFamily: 'Hemi Head FourTwentySix',
                }}
                variant="h4">Hayri Eryürek</Typography>
               </Grid>
               <Grid item >
                  <Typography
                style={{
                  align:"left",
                  color : "#f8f5e5",
                }}
                variant="h6">CEO & Senior Developer</Typography>
               </Grid>

               <Grid item>

               <IconButton color="primary" aria-label="Hayri Eryurek" component="span" onClick={this.HandleOpenLinkedInHayri}>
                <LinkedInIcon  style={{
                    color : "#f8f5e5",
                    fontSize : 50
                  }} />
                </IconButton>
               </Grid>              
            </Grid>

            <Grid item
             container
             direction="row"
             justifyContent="center"
             alignItems="center">

           

                <Grid 
                  sm={3}
                    style={{paddingTop:30}}
                    item
                  container
                  direction="column"
                  justifyContent="center"
                  alignItems="center">
                    <Grid item>
                      <Avatar
                          alt="Remy Sharp"
                          src={suleyman}
                          sx={{ width: 200, height: 200 }}
                        />
                    </Grid>
                    <Grid item style={{paddingTop:20}}>
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                        fontFamily: 'Hemi Head FourTwentySix',
                      }}
                      variant="h4">Süleyman Güvenç</Typography>
                    </Grid>
                    <Grid item >
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                      }}
                      variant="h6">CTO & Senior Developer</Typography>
                    </Grid>

                    <Grid item>

                    <IconButton color="primary" aria-label="Suleyman Guvenc" component="span" onClick={this.HandleOpenLinkedInSuleyman}>
                      <LinkedInIcon  style={{
                          color : "#f8f5e5",
                          fontSize : 50
                        }} />
                      </IconButton>
                    </Grid>              
                  </Grid>



                  <Grid 
                  sm={3}
                    style={{paddingTop:30}}
                    item
                  container
                  direction="column"
                  justifyContent="center"
                  alignItems="center">
                    <Grid item>
                      <Avatar
                          alt="Remy Sharp"
                          src={hakan}
                          sx={{ width: 200, height: 200 }}
                        />
                    </Grid>
                    <Grid item style={{paddingTop:20}}>
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                        fontFamily: 'Hemi Head FourTwentySix',
                      }}
                      variant="h4">Mete Hakan Eryürek</Typography>
                    </Grid>
                    <Grid item >
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                      }}
                      variant="h6">CIO & Senior Developer</Typography>
                    </Grid>

                    <Grid item>

                    <IconButton color="primary" aria-label="Suleyman Guvenc" component="span" onClick={this.HandleOpenLinkedInHakan}>
                      <LinkedInIcon  style={{
                          color : "#f8f5e5",
                          fontSize : 50
                        }} />
                      </IconButton>
                    </Grid>              
                  </Grid>


                

                  <Grid 
                    sm={3}
                    style={{paddingTop:30}}
                    item
                  container
                  direction="column"
                  justifyContent="center"
                  alignItems="center">
                    <Grid item>
                      <Avatar
                          alt="Remy Sharp"
                          src={muhammed}
                          sx={{ width: 200, height: 200 }}
                        />
                    </Grid>
                    <Grid item style={{paddingTop:20}}>
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                        fontFamily: 'Hemi Head FourTwentySix',
                      }}
                      variant="h4">Muhammed Öztürk</Typography>
                    </Grid>
                    <Grid item >
                        <Typography
                      style={{
                        align:"left",
                        color : "#f8f5e5",
                      }}
                      variant="h6">COO & Senior Developer</Typography>
                    </Grid>

                    <Grid item>

                    <IconButton color="primary" aria-label="Hayri Eryurek" component="span" onClick={this.HandleOpenLinkedInMuhammed}>
                      <LinkedInIcon  style={{
                          color : "#f8f5e5",
                          fontSize : 50
                        }} />
                      </IconButton>
                    </Grid>              
                  </Grid>


                  <Grid 
              style={{paddingTop:30}}
              item
             container
             direction="column"
             justifyContent="center"
             alignItems="center">
               <Grid item>
                <Avatar
                    alt="Remy Sharp"
                    src={ilknur}
                    sx={{ width: 200, height: 200 }}
                  />
               </Grid>
               <Grid item style={{paddingTop:20}}>
                  <Typography
                style={{
                  align:"left",
                  color : "#f8f5e5",
                  fontFamily: 'Hemi Head FourTwentySix',
                }}
                variant="h4">ilknur Erol</Typography>
               </Grid>
               <Grid item >
                  <Typography
                style={{
                  align:"left",
                  color : "#f8f5e5",
                }}
                variant="h6">CAO & Marketing Manager</Typography>
               </Grid>

               <Grid item>

               {/*<IconButton color="primary" aria-label="İlknur Erol" component="span" onClick={this.HandleOpenLinkedInIlknur}>
                <LinkedInIcon  style={{
                    color : "#f8f5e5",
                    fontSize : 50
                  }} />
                </IconButton>*/}
               </Grid>              
            </Grid>


            </Grid>
          </Grid>
          
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TTeam);


