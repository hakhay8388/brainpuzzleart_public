import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject";

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import AboutImage from "../../../../../assets/img/About.png";
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Discord from "../../../../../assets/img/discord.png";


import Blue_Cube from "../../../../../assets/animation_games/img/Blue_Cube.png";
import Brown_Cube from "../../../../../assets/animation_games/img/Brown_Cube.png";
import Red_Cube from "../../../../../assets/animation_games/img/Red_Cube.png";
import Green_Cube from "../../../../../assets/animation_games/img/Green_Cube.png";
import DarkYellow_Cube from "../../../../../assets/animation_games/img/DarkYellow_Cube.png";
import Orange_Cube from "../../../../../assets/animation_games/img/Orange_Cube.png";
import Purple_Cube from "../../../../../assets/animation_games/img/Purple_Cube.png";

import UpArrow from "../../../../../assets/animation_games/img/UpArrow.png";

var TFaq = Class( TObject,
  {
    ObjectType: ObjectTypes.Get( "TFaq" )
    ,
    constructor: function ( _Props )
    {
      TFaq.BaseObject.constructor.call( this, _Props );
      this.state =
        {
        ...this.state,
        expanded: ""
        }
        this.boxSize = 50;
    }
    ,
    AsyncLoad: function () 
    {
    }
    ,
    Destroy: function ()
    {
      TFaq.BaseObject.Destroy.call( this );
    }
    ,
    HandleChange : function(_PanelName)
    {
      if (_PanelName == this.state.expanded && _PanelName != null && _PanelName != "") _PanelName = "";
      this.setState({
        expanded : _PanelName
      });
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
    render() {
      const { classes } = this.props;

      return (
        <div id="TFaq" style={{paddingTop:"50px"}}>
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
              variant="h1">Faq</Typography>
            </Grid>
            <Grid style={{maxWidth:800}}>
              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel1'} onChange={() => this.HandleChange('panel1')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel1' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Red") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>What game is the first Brain Puzzle Art game will be released?</Typography>
                </AccordionSummary>
                <AccordionDetails>
                  <Typography sx={{ color: "#f8f5e5" }}>
                      Our first game will be released as a triple game. There will be 3 different games with the same concept, Brain Boxes, Clever and Brain Burner.
                  </Typography>
                </AccordionDetails>
              </Accordion>
              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel2'} onChange={() => this.HandleChange('panel2')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel2' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Green") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>Will you have a chance to practice before the game is fully released?</Typography>
                </AccordionSummary>
                <AccordionDetails>
                  <Typography sx={{ color: "#f8f5e5" }}>
                  Yes, we will release a trial game so you can practice before our Game starts playing on NFT stages
                  </Typography>
                </AccordionDetails>
              </Accordion>


              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel3'} onChange={() => this.HandleChange('panel3')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel3' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Blue") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>What is the Total Supply/Price?</Typography>
                </AccordionSummary>
                <AccordionDetails>
                
                  <Typography sx={{ color: "#f8f5e5" }}>
                  Brain Boxes NFT Supply 5000 
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }}>
                  Clever NFT Supply 5000
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }}>
                  Brain Burner NFT Supply 5000
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }}>
                  Total Supply will be around 15000 / Price will be around 1-2 SOLs depending on the difficulty of the stage.
                  </Typography>
                </AccordionDetails>
              </Accordion>


              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel4'} onChange={() => this.HandleChange('panel4')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel4' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("DarkYellow") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>What are 3 different puzzle games and what are the differences between them?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} variant="h5">
                    Brain Box
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                    Brain Box game is just a game consisting of boxes and targets. The direction of the boxes cannot be changed. The boxes are tried to be brought over the relevant target by pushing each other in their own direction.
                  </Typography>
                  
                  <Typography sx={{ color: "#f8f5e5", marginTop:"20px" }} variant="h5">
                  Clever
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  Clever is a game of boxes, target and rotators that rotate boxes in one direction. The direction of the boxes can be changed. It is a slightly more challenging game as more complex game algorithms have emerged. The boxes are tried to be brought over the relevant target by pushing each other in their own direction.
                  </Typography>

                  <Typography sx={{ color: "#f8f5e5", marginTop:"20px" }} variant="h5">
                  Brain Burner
                  </Typography>
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  Brain Burner is the latest game in our puzzle game series and is our hardest and most complex game. There are multiple rotators that rotate the direction of the boxes. Rotator types are Unidirectional rotator, clockwise and counter-clockwise 90-degree rotator and 180-degree rotator. It is our most challenging intelligence game, as very, very complex game algorithms have emerged. The boxes are tried to be brought over the relevant target by pushing each other in their own direction.
                  </Typography>
  
                </AccordionDetails>
              </Accordion>


              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel5'} onChange={() => this.HandleChange('panel5')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel5' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Orange") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>Is there any ready puzzle NFT asset?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                    We have made all our preparations to create Puzzle NFTs in a unique way.
                  </Typography>
                </AccordionDetails>
              </Accordion>


              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel6'} onChange={() => this.HandleChange('panel6')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel6' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Purple") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>When is the mint date?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                      Wait for our practice game, but we think it will probably be the first week of September.
                  </Typography>
                </AccordionDetails>
              </Accordion>



              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel7'} onChange={() => this.HandleChange('panel7')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel7' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Red") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>Is there an OG/whitelist?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  Yes. While we want to be equal to everyone, we think our initial supporters should have a difference.
                  </Typography>
                </AccordionDetails>
              </Accordion>



              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel8'} onChange={() => this.HandleChange('panel8')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel8' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Green") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>Is the team doxxed?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  We are people who have worked side by side in the organizations we have worked with before and you can find the real identity of everyone working in our team and you can review us on Linkedin.
                  </Typography>
                </AccordionDetails>
              </Accordion>



              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel9'} onChange={() => this.HandleChange('panel9')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel9' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Blue") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>Secondary market?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  We want to contact Magic Eden at the first stage and use the launchpads. But we want our collection to be listed in other solana markets as well.
                  </Typography>
                </AccordionDetails>
              </Accordion>




              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel10'} onChange={() => this.HandleChange('panel10')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel10' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("DarkYellow") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>How can I pre-mint before whitelist?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  Pre-mint is the name given to the purchase of NFTs from the collection before they are whitelisted and released in a marketplace. Because it is a fiduciary relationship, NFT's pre-mint price is 0.5 Solana. You can benefit from NFT pre-mint by contacting us via social media and especially telegram. However, this purchase is limited to 3 NFTs.
                  </Typography>
                </AccordionDetails>
              </Accordion>


              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel11'} onChange={() => this.HandleChange('panel11')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel11' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Orange") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>You bought it, why should you keep it?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  The puzzle NFT you bought is designed to provide you with continuous passive income. This profit will start when the puzzle section you have is first solved, and as your section is selected and published in the tournaments, you will also earn from the betting fees.
                  </Typography>
                </AccordionDetails>
              </Accordion>

              <Accordion style={{backgroundColor:"#36404c" }} expanded={this.state.expanded === 'panel12'} onChange={() => this.HandleChange('panel12')}>
                <AccordionSummary
                  style={{paddingRight: this.state.expanded === 'panel12' ? "8px" : "0px"}}
                  expandIcon={
                    <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain",  backgroundImage: this.HandleGetBoxByColor("Purple") }}>
                      <div style={{ height: this.boxSize, width: this.boxSize, backgroundSize: "contain", transform :"rotate(" + this.HandleGetDirection("Left") + "deg)" ,  backgroundImage: "url(" + UpArrow + ")" }}>
                      </div> 
                    </div>
                  
                  
                  }
                  aria-controls="panel1bh-content"
                  id="panel1bh-header"
                >
                  <Typography sx={{ color:  "#f8f5e5" }}>How can I bu BPA token?</Typography>
                </AccordionSummary>
                <AccordionDetails>          
                  <Typography sx={{ color: "#f8f5e5" }} align="justify">
                  Since our BPA price is not determined yet, we do not have sales, but you can contact us via social media for pre-sale and register your name.
                  </Typography>
                </AccordionDetails>
              </Accordion>






            </Grid>
          </Grid>
         
        </div>
      )
    }
  }, {} );

export default withStyles(UnLoginStyles)(TFaq);


