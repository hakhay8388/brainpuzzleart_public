import GlobalStyles from "./GlobalStyles"


const UnLoginStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  console.log(_Theme);
  return {
    ...__GlobalStyle,
    test: {
      position: 'absolute',
      top: 50,
      margin : '0px !important'
    },
    headerLink: {
      textAlign: 'center',
      color : _Theme.palette.light.main
    },
    headerAvatar: {
      textAlign: 'center',
      [_Theme.breakpoints.only('xs')]: {
        display: "none"
      },
    },
    headerBig: {
      [_Theme.breakpoints.down('sm')]: {
        display: "none"
      },
    }
    ,
    headerSmall: {
      [_Theme.breakpoints.up('sm')]: {
        display: "none"
      },
    }
  };
};


export default UnLoginStyles
