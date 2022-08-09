import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../../WebGraph/GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import AboutImage from "../../../../../assets/img/About.png";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import HemiHead from "../../../../../assets/fonts/hemihead.ttf";


var TAbout = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TAbout" )
    ,
    constructor: function ( _Props )
    {
      TAbout.BaseObject.constructor.call( this, _Props );
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TAbout.BaseObject.Destroy.call( this );
    }
    ,
    render() {
      const { classes } = this.props;

      return (
        <div id="TAbout" style={{paddingTop:"50px"}}>
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
              variant="h1">About</Typography>
            </Grid>
            <Grid item
             container
             direction="column"
             justifyContent="center"
             alignItems="center">
               <Grid item style={{padding:20}}>
                <Typography 
                      variant="subtitle1" 
                      gutterBottom 
                      component="div"
                      align="justify"

                style={{
                  color : "#f8f5e5",
                  
                  /*fontFamily: 'Hemi Head FourTwentySix',*/
                  maxWidth : "1000px",
                }}
                >
                  We are here with the first of dozens of intelligence games that we will develop in the future. Intelligence games are games that keep our minds fresh and spend a lot of time in them. Our aim is that people who enjoy spending time in intelligence games can earn money while playing the games. In order to become a player, you must have BPA Token in your hand and you will be able to register a player with a small BPA payment. Of course, there is a fine competition among the people who solve the puzzles about who is smarter. The competition of these people has also presented us with a concept that will create a betting environment. In the future, the players and as the game develops, the players will be known more and we will have our bookies doing detailed research such as who to bet on and when. So why not have an owner of these challenging brain teasers? When we focused on this issue, the idea of selling each stage as NFT emerged. So why should you have these puzzle NFTs? Because each puzzle NFT will have a BPA Token reward according to its difficulty value, and this reward will be shared with the person who solved the game and the wallet that owns the puzzle NFT, when the relevant puzzle stage is solved for the first time. 
                </Typography>
               </Grid>     
               <Grid item>
                  <img style={{objectFit: "contain", maxWidth : "300px", width : "100%"}} src={AboutImage} alt="..." />
               </Grid>
               <Grid item style={{padding:20}}>
                  
               <Typography 
                      variant="subtitle1" 
                      gutterBottom 
                      component="div"
                      align="justify"

                style={{
                  color : "#f8f5e5",
                  
                  /*fontFamily: 'Hemi Head FourTwentySix',*/
                  maxWidth : "1000px",
                }}
                >
                  This puzzle NFT won't run out after the chapter is solved. 2 times a day will be bet and 1 game randomly selected from puzzle NFT stages . Shares will also win from bets placed on the selected stage. If the bet is not completed on time, the majority of the stake will be transferred to the owner of the puzzle NFT. In short, if you are reading this article right now, you are very lucky to be faced with a project that is on the verge of a big game economy. To take a strong place in this economy, you can get our puzzle NFTs and request a pre-sale in BPA Token.
              </Typography>
                  

               </Grid>
                  
            </Grid>
          </Grid>
          
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TAbout);


