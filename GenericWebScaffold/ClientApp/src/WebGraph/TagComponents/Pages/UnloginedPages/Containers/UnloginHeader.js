import React from "react";
import {  Class,  JSTypeOperator,  ObjectTypes } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../../TagComponents/TObject";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";

import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Avatar from '@mui/material/Avatar';
import classNames from "classnames";
import PropTypes from 'prop-types';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import { CardActionArea } from '@mui/material';
import LogoIcon from "../../../../../assets/img/TopIcon.png";
import WhitePaper from "../../../../../assets/pdfs/WhitePaper.pdf";

import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import ElevationScroll from "./ElevationScroll";
import Scroll from "react-scroll-to-element";



var UnloginHeader = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("UnloginHeader"),
    constructor: function (_Props) {
      UnloginHeader.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        expanded : false
      };
      window.App.UnloginHeader = this;
    },
    Destroy: function () {
      UnloginHeader.BaseObject.Destroy.call(this);
    },
    AsyncLoad: function () {
      UnloginHeader.BaseObject.AsyncLoad.call(this);
    },
    HandleTest : function()
    {

    }
    ,
    HandleMenuOpen : function()
    {
       var __Expanded = !this.state.expanded;
      this.setState({
        expanded : __Expanded
      });
    }
    ,
    HandleABOUT : function()
    {
      this.setState({
        expanded : false
      });
    }
    ,
    HandleROADMAP : function()
    {
      this.setState({
        expanded : false
      });
    }
    ,
    HandleFAQ : function()
    {
      this.setState({
        expanded : false
      });
    }
    ,
    HandleTEAM : function()
    {
      this.setState({
        expanded : false
      });
    }
    ,
    HandleWHITEPAPER : function()
    {
      this.setState({
        expanded : false
      });
      window.open(WhitePaper, '_blank');
      //window.location.href = WhitePaper;
    }    
    ,
    render() 
    {
      var __This = this;
      const { children, classes, ...attributes } = this.props;
      return (
        <ElevationScroll {...this.props}>
          <AppBar color={'dark'} position="static">
            <Container 
            maxWidth="xl">
              <Toolbar disableGutters style={{height : 68}}>
                <Grid container
                  direction="row"
                  justifyContent="center"
                  alignItems="center"
                  >
                  <Grid item xs={2}>
                  <Button
                      key={"Home"}
                      onClick={() => { }}
                      sx={{ my: 2, color: 'white', display: 'block' }}
                    >
                    <Avatar variant="rounded" src={LogoIcon} />
                    </Button>
                  </Grid>
                  <Grid item xs={2} 
                  className={classNames(
                    classes.headerLink,
                    classes.headerBig                      
                  )}>
                    <Scroll offset={-50} type="id" element={"TAbout"}>
                    <Button
                      key={"ABOUT"}
                      sx={{ my: 2, color: 'white', display: 'block' }}
                    >
                      ABOUT
                    </Button>
                    </Scroll>
                  </Grid>
                  <Grid item xs={2}  className={classNames(
                    classes.headerLink,
                    classes.headerBig                      
                  )}>
                    <Scroll offset={-100} type="id" element={"TRouteMap"}>
                      <Button
                        key={"ROADMAP"}
                        sx={{ my: 2, color: 'white', display: 'block' }}
                      >
                        ROADMAP
                      </Button>
                    </Scroll>
                  
                  </Grid>
                  <Grid item xs={2}  className={classNames(
                    classes.headerLink,
                    classes.headerBig
                  )}>
                    <Scroll offset={-50} type="id" element={"TTeam"}>
                      <Button
                        key={"TEAM"}
                        sx={{ my: 2, color: 'white', display: 'block' }}
                      >
                        TEAM
                      </Button>
                    </Scroll>                      
                  </Grid>
                  <Grid item xs={2}  className={classNames(
                    classes.headerLink,
                    classes.headerBig                      
                  )}>
                    <Scroll offset={-50} type="id" element={"TFaq"}>
                      <Button
                        key={"FAQ"}
                        sx={{ my: 2, color: 'white', display: 'block' }}
                      >
                        FAQ
                      </Button>
                    </Scroll>
                    
                  </Grid>
                  <Grid item xs={2}  className={classNames(
                    classes.headerLink,
                    classes.headerBig                
                  )}>
                  <Button
                      key={"WHITEPAPER"}
                      onClick={this.HandleWHITEPAPER}
                      sx={{ my: 2, color: 'white', display: 'block' }}
                    >
                      WHITEPAPER
                    </Button>                      
                  </Grid>

                  <Grid item xs={8}  className={classNames(
                    classes.headerLink,
                    classes.headerSmall                
                  )}></Grid>

                  <Grid item xs={2}  className={classNames(
                    classes.headerLink,
                    classes.headerSmall                
                  )}>
                      <IconButton
                          size="large"
                          edge="start"
                          color="inherit"
                          aria-label="menu"
                          onClick={this.HandleMenuOpen}
                        >
                          <MenuIcon />
                        </IconButton>
                  </Grid>
                </Grid>
                </Toolbar>
              </Container>
              <Accordion expanded={this.state.expanded} className={classNames(
                      classes.headerSmall                
                    )} style={{borderRadius: '0px 0px 0px 0px'}}>
                  <AccordionSummary style={{display:"none"}}>
                  </AccordionSummary>
                <AccordionDetails style={{ backgroundColor: "#26303c", padding : "0px"}}>
                    
                <Grid container
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    >
                    <Grid item xs={2} >
                        <Button
                          key={"ABOUT2"}
                          onClick={this.HandleABOUT}
                          sx={{ my: 1, color: 'white', display: 'block' }}
                        >
                         <Scroll offset={-50} type="id" element={"TAbout"}>
                          ABOUT
                          </Scroll>
                        </Button>
                    </Grid>
                    <Grid item xs={2}>
                        <Button
                            key={"ROADMAP2"}
                            onClick={this.HandleROADMAP}
                            sx={{ my: 1, color: 'white', display: 'block' }}
                          >
                           <Scroll offset={-100} type="id" element={"TRouteMap"}>
                              ROADMAP
                            </Scroll>
                         </Button>                      
                      </Grid>
                    <Grid item xs={2} >
                        <Button
                          key={"TEAM2"}
                          onClick={this.HandleTEAM}
                          sx={{ my: 1, color: 'white', display: 'block' }}
                        >
                          <Scroll offset={-50} type="id" element={"TTeam"}>
                            TEAM
                          </Scroll>
                        </Button>
                    </Grid>
                    <Grid item xs={2}>
                      <Button
                        key={"FAQ2"}
                        onClick={this.HandleFAQ}
                        sx={{ my: 1, color: 'white', display: 'block' }}
                      >
                        <Scroll offset={-50} type="id" element={"TFaq"}>
                        FAQ
                      </Scroll>
                      </Button>
                    </Grid>

                    <Grid item xs={2} >
                    <Button
                        key={"WHITEPAPER2"}
                        onClick={this.HandleWHITEPAPER}
                        sx={{ my: 1, color: 'white', display: 'block' }}
                      >
                        WHITEPAPER
                      </Button>                      
                    </Grid>

                  </Grid>



                </AccordionDetails>
              </Accordion>

          </AppBar>
          </ElevationScroll>
      );
    },
  },
  {}
);

export default withStyles(UnLoginStyles)(UnloginHeader);
