import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../../WebGraph/GenericCoreGraph/ClassFramework/Class"
import TBaseContainerLayout from "../../TBaseContainerLayout"

import {withStyles} from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";


const UnloginFooter = React.lazy(() => import( './UnloginFooter' ) );
const UnloginHeader = React.lazy(() => import( './UnloginHeader' ) );



var UnloginedLayout = Class( TBaseContainerLayout,
  {
    ObjectType: ObjectTypes.Get( "UnloginedLayout" )
    ,
    constructor: function ( _Props )
    {
      UnloginedLayout.BaseObject.constructor.call( this, _Props );
    }
    ,
    HandleGetAside: function ()
    {
      return null;
    }
    ,
    HandleGetFooter: function ()
    {
      var __This = this;
      return <UnloginFooter {...this.props} />
    }
    ,
    HandleGetHeader: function ()
    {
      return <UnloginHeader {...this.props} />;
    }
    ,
    Destroy: function ()
    {
      UnloginedLayout.BaseObject.Destroy.call( this );
    }
  }, {} );

export default withStyles(UnLoginStyles)(UnloginedLayout);


