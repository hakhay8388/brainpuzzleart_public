import DefaultTheme from "../Themes/DefaultTheme";

const GlobalStyles = function(_Theme) {
  return {
    ThemeProps : {
      Theme : _Theme
    }
    ,
    background: {
      backgroundColor: "#26303c"
    },
  };
};


export default GlobalStyles;
