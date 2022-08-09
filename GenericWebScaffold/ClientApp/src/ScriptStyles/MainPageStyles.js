import GlobalStyles from "./GlobalStyles"


const MainPageStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  console.log(_Theme);
  return {
    ...__GlobalStyle,
    puzzleBig: {
      [_Theme.breakpoints.down('sm')]: {
        display: "none"
      },
    }
    ,
    puzzleSmall: {
      [_Theme.breakpoints.up('sm')]: {
        display: "none"
      },
    }
  };
};


export default MainPageStyles
