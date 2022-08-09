import React, { Component } from "react";
import { useLocation,
  useNavigate,
  useParams, Routes,  Route } from "react-router-dom";

import { WebGraph } from "../../WebGraph/GenericCoreGraph/WebGraph/WebGraph";
import GenericWebGraph from "../../WebGraph/GenericWebController/GenericWebGraph";
import {  ObjectTypes,  DebugAlert } from "../../WebGraph/GenericCoreGraph/ClassFramework/Class";
import { ThemeProvider } from "@mui/material/styles";
import "Themes/Themes.js";

import withRouter from "./Utilities/withRouter";
import TLoading from "./Utilities/TLoading";
import Pages from "./Pages";


const TMessageBox = React.lazy(() => import("./Listeners/TMessageBox"));
const THotSpotMessage = React.lazy(() => import("./Listeners/THotSpotMessage"));
const TGlobalLoading = React.lazy(() => import("./Utilities/TGlobalLoading"));
const TDynamicLoader = React.lazy(() => import("./TDynamicLoader"));


const UnloginedLayout = React.lazy(() => import("./Pages/UnloginedPages/Containers/UnloginedLayout"));


class TApp extends Component {
  constructor(props) {
    super();

    this.GetModal = this.GetModal.bind(this);
    this.GetLayout = this.GetLayout.bind(this);
    this.GetTheme = this.GetTheme.bind(this);

    window.App.App = this;
    this.state = {
      ...this.state,
    };
    WebGraph.Init();
    GenericWebGraph.Init();
  }

  GetModal()
  {
    return null;
  }

  GetLayout() 
  {
    if (Pages.Routes.length > 0 && window.App.Checked)
    {
      return <UnloginedLayout {...this.props}  />;      
    }
    else
    {
      return <TLoading />;
    }
  }

  GetTheme() {
    return window.Themes.DefaultTheme;
  }

  render() 
  {
      return (
        <div style={{ fontFamily: "Montserrat" }}>
          <React.Suspense fallback={<TLoading />}>
          <ThemeProvider theme={this.GetTheme()}>
            <TDynamicLoader />
            <THotSpotMessage />
            
            <TGlobalLoading />
            <TMessageBox />

            {this.GetModal()}
            <Routes>
              <Route path="*" name="Ana Sayfa" element={
                     <React.Suspense fallback={<TLoading />}>
                         {this.GetLayout()}
                     </React.Suspense>
                 } />
            </Routes>
          </ThemeProvider>
          </React.Suspense>
        </div>
      );
  }
}

export default withRouter(TApp)

