import React, { Component } from "react";

import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../../TagComponents/TObject";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import {withStyles} from "@mui/styles";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import AboutImage from "../../../../../assets/img/About.png";
import logo from "../../../../../assets/img/icon.png";
import TwitterIcon from '@mui/icons-material/Twitter';
import TelegramIcon from '@mui/icons-material/Telegram';
import InstagramIcon from '@mui/icons-material/Instagram';
import YouTubeIcon from '@mui/icons-material/YouTube';
import IconButton from '@mui/material/IconButton';
import Medium from "../../../../../assets/img/medium.png";
import Discord from "../../../../../assets/img/discord.png";

var UnloginFooter = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("UnloginFooter"),
    constructor: function (_Props) {
      UnloginFooter.BaseObject.constructor.call(this, _Props);
    },
    Destroy: function () {
      UnloginFooter.BaseObject.Destroy.call(this);
    },
    render() {
      // eslint-disable-next-line
      const { children, ...attributes } = this.props;
      const { classes } = this.props;

      return (
        <div style={{ width: "100%", marginTop:80 }}>
            <Grid
            container
            direction="column"
            justifyContent="flex-start"
            alignItems="center"
          >
              <Grid item>
                <img style={{objectFit: "contain", width : "200px"}} src={logo} alt="..." />
               </Grid>
               <Grid item style={{padding:20}}>
                  
               <Typography 
                      variant="subtitle1" 
                      gutterBottom 
                      component="div"
                      align="center"

                style={{
                  color : "#f8f5e5",
                  
                  /*fontFamily: 'Hemi Head FourTwentySix',*/
                  maxWidth : "1000px",
                }}
                >
                  Brain Puzzle Art is a project that offers life energy, aiming to make you a profit while organizing exciting and enjoyable events, and to gather smart people together. Please follow our social media to be notified of updates.    
              </Typography>
               </Grid>

               <Grid
                  item
                  container
                  direction="row"
                  justifyContent="center"
                  alignItems="center"
                >
                  <Grid item>
                  <IconButton color="primary" aria-label="Twitter" component="span" onClick={() => {
                      window.open("https://twitter.com/BrainPuzzleArt", '_blank');
                  }}>
                    <TwitterIcon  style={{
                        color : "#f8f5e5",
                        fontSize : 50
                      }} />
                    </IconButton>
                  </Grid>
                  <Grid item>
                  <IconButton color="primary" aria-label="Telegram" component="span"  onClick={() => {
                      window.open("https://t.me/BrainPuzzleArt", '_blank');                      
                  }}>
                    <TelegramIcon  style={{
                        color : "#f8f5e5",
                        fontSize : 50
                      }} />
                    </IconButton>
                  </Grid>

                  <Grid item>
                  <IconButton color="primary" aria-label="Instagram" component="span"  onClick={() => {
                      window.open("https://www.instagram.com/BrainPuzzleArt/", '_blank');                      
                  }}>
                    <InstagramIcon  style={{
                        color : "#f8f5e5",
                        fontSize : 50
                      }} />
                    </IconButton>
                  </Grid>
                  <Grid item>
                  <IconButton color="primary" aria-label="YouTube" component="span"  onClick={() => {
                      window.open("https://www.youtube.com/channel/UC19V9C8XAV3LY9QweKFuKMQ", '_blank');                      
                  }}>
                    <YouTubeIcon  style={{
                        color : "#f8f5e5",
                        fontSize : 50
                      }} />
                    </IconButton>
                  </Grid>

                  <Grid item>
                  <IconButton color="primary" aria-label="YouTube" component="span"  onClick={() => {
                      window.open("https://medium.com/@brainpuzzleart", '_blank');
                  }}>
                    <img style={{objectFit: "contain", width : "50px"}} src={Medium}  />
                    </IconButton>
                  </Grid>

                  <Grid item>
                  <IconButton color="primary" aria-label="Discord" component="span"  onClick={() => {
                      window.open("https://discord.io/brainpuzzleart", '_blank');
                  }}>
                    <img style={{objectFit: "contain", width : "50px"}} src={Discord}  />
                    </IconButton>
                  </Grid>

               </Grid>

               </Grid>
        </div>
      );
    },
  },
  {}
);


export default withStyles(UnLoginStyles)(UnloginFooter);
