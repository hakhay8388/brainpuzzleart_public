import { createTheme  } from "@mui/material/styles";
import HemiHead from "../assets/fonts/hemihead.ttf";

const DefaultTheme = createTheme ({
  palette: {
    primary: {
      main: "#79933d",
      dark: "#728b39",
      light: "#7e9842"
    },
    danger: {
      main: "#ce3d38",
    }
    ,
    light: {
      main: "#f8f5e5",
      contrastText: "#26303c"
    }
    ,
    dark: {
      main: "#26303c",
      light: '#2a3441',
      contrastText: "#f8f5e5",
    }
    ,
    secondary: {
      main: '#276f8f',
      dark: '#266b8a',
      contrastText: '#2a3441',
      light: "#297394",
    },
    success :{
      main: '#a2cf6e',
      contrastText: '#FFFFFF',
      light: "#E0E0E0"
    }
    ,
    link: {
      main: '#9C27B0'
    }
    ,
    default: {
      main: '#F0F0F0',
      light: "#FEFEFE",
      contrastText: "#fdfdfd",
      alternative: '#ffeeee',
      darkAlternative: '#b1b1b1',
      lightAlternative: '#f1f1f1',
      contrastAlternative: "#cccccc",
      contrastDark: "#9C9C9C",
      contrastLight: "#999999",
      mainAlternative: "#f2f2f6",
      mainLight: "#FAFAFC",
      mainDark: "#707070",
      secondAlternative: "#505050",
      thirdAlternative: "#A0A0A0",
      fourthAlternative: "#d0d0d9",
      fifthAlternative: "#c5c5c5",
      sixthAlternative: "#f7f7f7"
    },
    info : {
      dark: '#2582ac',
      main: '#35baf6',
      light: '#5dc7f7',
      contrastText: '#2548A5',
      alternative: '#6490b1',
      lightAlternative: "#1B9CE2",
      darkAlternative: "#4C5776",
      contrastAlternative: "#238AFF",
      secondAlternative: "#348dec"
    },
    error: {
      main: "#ff5757",
    },
    warning : {
      main: "#ff9100"
    },
    none : {
      main: "#ffffff"
    },
    action : {
      main: '#35baf6',
    }
  },
  typography : {
    fontFamily: "Montserrat",
    button: {
      textTransform: "none",
      textDecoration: "none",
    }
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: `
        @font-face {
          font-family: 'HemiHead';
          font-style: normal;
          font-display: swap;
          font-weight: 400;
          src: local('HemiHead'), local('HemiHead-Regular'), url(${HemiHead}) format('ttf');
          unicodeRange: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF;
        }
      `,
    },
  },
});

export default DefaultTheme;
