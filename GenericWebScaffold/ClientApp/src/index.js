
//import "./assets/css/reset.css";
import "./WebGraph/Initializers";
import "./WebGraph/App";
//import './WebGraph/GenericWebController/Managers/LanguageManager/Language';
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import CssBaseline from '@mui/material/CssBaseline';

//import App from './App';
import TApp from "./WebGraph/TagComponents/TApp";

import * as Sentry from "@sentry/react";
import { Integrations } from "@sentry/tracing";

/*import TagManager from "react-gtm-module";

const tagManagerArgs = {
  gtmId: "GTM-MD5BWWW",
};

TagManager.initialize(tagManagerArgs);*/

Sentry.init({
  dsn: "https://512835e45e63454983feef026f495374@o1052346.ingest.sentry.io/6036012",
  integrations: [new Integrations.BrowserTracing()],

  // Set tracesSampleRate to 1.0 to capture 100%
  // of transactions for performance monitoring.
  // We recommend adjusting this value in production
  tracesSampleRate: 1.0,
  beforeSend: (event) => {
    if (window.GetHostName() === "localhost") {
      return null;
    }
    return event;
  },
});

ReactDOM.render(
  <BrowserRouter history={window.History}>
    <CssBaseline />
    <TApp />
  </BrowserRouter>,
  document.getElementById("root")
);
