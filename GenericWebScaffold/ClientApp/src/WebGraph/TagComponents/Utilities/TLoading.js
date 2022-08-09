import React from "react";
import logo from "../../../assets/img/icon.png";
import Grid from "@mui/material/Grid";
import CircularProgress from '@mui/material/CircularProgress';
import classNames from "classnames";
import {withStyles} from "@mui/styles";
import GlobalStyles from "../../../ScriptStyles/GlobalStyles";


var TLoading = function (props) {
  const {classes} = props;
  return (
    <div className={classNames(classes.appStyle, classes.alignItemsCenter, classes.flexRow)}>
      <Grid container 
          direction="column"
          justifyContent="center"
          alignItems="center"
          style={{overflow: "hidden"}}>
        <Grid container 
          direction="column"
          justifyContent="center"
          alignItems="center" item xs={12} style={{ display: "contents" }}>
            <Grid item>
              <img style={{objectFit: "contain", width : "100%"}} src={logo} alt="..." />
            </Grid>          
        </Grid>
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="center"
        >
          <CircularProgress color={'primary'} />
        </Grid>
      </Grid>
    </div>  );
};

export default withStyles(GlobalStyles) (TLoading);
