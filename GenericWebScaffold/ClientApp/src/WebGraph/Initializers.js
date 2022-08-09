import "../WebGraph/TagComponents/Utilities/History";
import "../WebGraph/Enums/Enums";
import $ from 'jquery';


if (!Array.prototype.includes)
{
  Object.defineProperty(Array.prototype, "includes", {
    enumerable: false,
    value: function (obj)
    {
      var newArr = this.filter(function (el)
      {
        return el == obj;
      });
      return newArr.length > 0;
    }
  });
}

if (!Array.prototype.unique)
{
  Object.defineProperty(Array.prototype, "unique", {
    enumerable: false,
    value: function (_function)
    {
      var a = this.concat();
      for (var i = 0; i < a.length; ++i)
      {
        for (var j = i + 1; j < a.length; ++j)
        {
          if (_function(a[i], a[j]))
          {
            a.splice(j--, 1);
          }
        }
      }

      return a;
    }
  });

}
String.prototype.left = function(count) {
  return this.slice(0, count);
}

String.prototype.right = function(count) {
  return this.slice(-count);
}

String.prototype.format = function ()
{
  var __Formatted = this;
  for (var i = 0; i < arguments.length; i++)
  {
    var regexp = new RegExp("\\{" + i + "\\}", "gi");
    __Formatted = __Formatted.replace(regexp, arguments[i]);
  }
  return __Formatted;
};

String.prototype.trim = function ()
{
  return this.replace(/^\s+|\s+$/g, "");
};


String.prototype.turkishToLower = function ()
{
  var result = this.replace(/I/g, "ı").toLocaleLowerCase();
  result = result.replace("ü", "u");
  result = result.replace("ı", "i");
  result = result.replace("ş", "s");
  result = result.replace("ğ", "g");
  result = result.replace("ö", "o");
  result = result.replace("ç", "c");
  return result;
};
String.prototype.htmlDecode = function ()
{
  return this.replace(/&([a-z]+);/ig, (match, entity) =>
  {
    const entities = { amp: '&', apos: '\'', gt: '>', lt: '<', nbsp: '\xa0', quot: '"' };
    entity = entity.toLowerCase();
    if (entities.hasOwnProperty(entity))
    {
      return entities[entity];
    }
    return match;
  });
};

if (!String.prototype.startsWith)
{
  try
  {
    Object.defineProperty(String.prototype, "startsWith", {
      value: function (search, pos)
      {
        pos = !pos || pos < 0 ? 0 : +pos;
        return this.substring(pos, pos + search.length) === search;
      },
    });
  } catch (_Ex) { }
}

if (typeof String.prototype.endsWith !== 'function')
{
  try
  {
    String.prototype.endsWith = function (suffix)
    {
      return this.indexOf(suffix, this.length - suffix.length) !== -1;
    };
  } catch (_Ex) { }
}


Array.prototype.sortBy = function (p)
{
  return this.slice(0).sort(function (a, b)
  {
    return a[p] > b[p] ? 1 : a[p] < b[p] ? -1 : 0;
  });
};
Array.prototype.sortByDesc = function (p)
{
  return this.slice(0).sort(function (a, b)
  {
    return a[p] > b[p] ? -1 : a[p] < b[p] ? 1 : 0;
  });
};
const realError = console.error;
console.error = (...x) =>
{

  if (
    x[0] ===
    "Warning: The tag <%s> is unrecognized in this browser. If you meant to render a React component, start its name with an uppercase letter."
  )
  {
    return;
  }

  if (
    x[0] ===
    "Warning: The tag <main> is unrecognized in this browser. If you meant to render a React component, start its name with an uppercase letter."
  )
  {
    return;
  }

  if (
    x[0] ===
    "The tag <main> is unrecognized in this browser. If you meant to render a React component, start its name with an uppercase letter."
  )
  {
    return;
  }
  if (
    x[0] ===
    "Warning: Failed prop type: Invalid prop`elevation` of type`string` supplied to`ForwardRef(Paper)`, expected`number`."
  )
  {
    return;
  }

  if (
    x[0].startsWith(
      "Material-UI: Expected spacing argument to be a number, got"
    )
  )
  {
    return;
  }

  if (x.length > 1 && x[1] == 'main')
  {
    return;
  }

  realError(...x);
};

window.GetUrlParams = function ()
{
  var __Params = window.location.search ? window.location.search : "";
  var __Params = __Params.split("?");
  if (__Params.length > 1)
  {
    return "?" + __Params[1];
  }
  return "";
};

window.GetUrl = function ()
{
  var __Result = window.location.pathname;
  if (__Result.startsWith("/"))
  {
    var __Result = __Result.substring(1);
  }
  return __Result;
};
window.GetUrlPage = function ()
{
  var __Result="";
  if ((window.GetUrl() + window.GetUrlParams()).left(3).right(1) == "/") {
    var __UrlLength = (window.GetUrl() + window.GetUrlParams()).length;
    __Result = (window.GetUrl() + window.GetUrlParams()).right(__UrlLength - 3);
  } else {
    __Result =(window.GetUrl() + window.GetUrlParams());
  }
  return __Result;
};
window.GetLanguageCodeFromUrl = function ()
{
  var __UrlPaths = window.GetUrl().split('/');
  if (__UrlPaths[0].length == 2)
  {
    ///Dil listesinden kontrol edilecek
    if (__UrlPaths[0] == "tr" || __UrlPaths[0] == "en")
    {
      return __UrlPaths[0];
    }
  }
  
  return "tr";
};

window.GetUrlWithoutLanguage = function ()
{
  var __UrlPaths = window.GetUrl().split('/');
  if (__UrlPaths[0].length == 2)
  {
    ///Dil listesinden kontrol edilecek
    if (__UrlPaths[0] == "tr" || __UrlPaths[0] == "en")
    {
      __UrlPaths.splice(0, 1);
      return __UrlPaths.join('/') ;
    }
  }
  
  return __UrlPaths.join('/');
};



window.SetUrl = function (_Url, _First = false)
{
  /*
  var __OrgUrl = _Url;
  _Url = _Url.replace(/(__ref\=(\d)*)\&/, "");
  _Url = _Url.replace(/\&(__ref\=(\d)*)/, "");
  _Url = _Url.replace(/\?(__ref\=(\d)*)/, "");

  if (_Url.endsWith("/"))
  {
    _Url = _Url.substring(0, _Url.length - 1);
  }

  if (_Url.indexOf("?") > -1)
  {
    _Url += "&__ref=" + new Date().getTime();
  } else
  {
    _Url += "?__ref=" + new Date().getTime();
  }*/

  if (_Url.startsWith("/"))
  {
    _Url = _Url.substring(1);
  }

  if (_Url.endsWith("/"))
  {
    _Url = _Url.substring(0, _Url.length - 1);
  }

  if (_First)
  {
    if (_Url == "")
    {
      window.History.replace(_Url, { firstPage: true });
    }
    else
    {
      window.History.replace("/" + _Url, { firstPage: true });
    }
    //window.History.push("/" + _Url);
  }
  else
  {
    if (_Url == "")
    {
      window.History.push(_Url);
    }
    else
    {
      window.History.push("/" + _Url);
    }

  }

};

/*
window.GetHash = function ()
{
  var __Hash = window.location.hash;
  var __Result = __Hash.split('?')[0];
  if (__Result.startsWith("#"))
  {
    var __Result = __Result.substring(1);
  }
  if (__Result.startsWith("/"))
  {
    var __Result = __Result.substring(1);
  }
  if (__Result.startsWith("#"))
  {
    var __Result = __Result.substring(1);
  }
  return __Result;
}

window.GetUrl = function ()
{
  return window.GetHash();
}

window.SetUrl = function (_Url)
{
  var __Real = _Url.split('#')[0];
  var __Hash = "";
  try
  {
    __Hash = _Url.split('#')[1];
  }
  catch (_Ex)
  {
    __Hash = __Real;
  }

  __Hash = __Hash.replace(/(__ref\=(\d)*)\&/, "");
  __Hash = __Hash.replace(/\&(__ref\=(\d)*)/, "");
  __Hash = __Hash.replace(/\?(__ref\=(\d)*)/, "");

  if (__Hash.indexOf("?") > -1)
  {
    __Hash += "&__ref=" + (new Date()).getTime();
  }
  else
  {
    __Hash += "?__ref=" + (new Date()).getTime();
  }
  window.location.href = "#" + __Hash;
}
*/

window.GetHost = function ()
{
  var __Url = window.location.href;
  var __NoHttp = __Url.split("//")[1];
  return __NoHttp.split("/")[0];
};

window.GetHostHttp = function ()
{
  var __Url = window.location.href;
  var __Http = __Url.split("//")[0];
  return __Http + "//";
};

window.GetHostName = function ()
{
  return window.location.hostname;
};

window.downloadBase64File = function (base64Data, filename)
{
  var element = document.createElement("a");
  element.setAttribute("href", base64Data);
  element.setAttribute("download", filename);
  element.style.display = "none";
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);
};

window.base64StringtoFile = function (base64String, filename)
{
  var arr = base64String.split(","),
    mime = arr[0].match(/:(.*?);/)[1],
    bstr = atob(arr[1]),
    n = bstr.length,
    u8arr = new Uint8Array(n);
  while (n--)
  {
    u8arr[n] = bstr.charCodeAt(n);
  }
  return new File([u8arr], filename, { type: mime });
};

window.extractImageFileExtensionFromBase64 = function (base64Data)
{
  return base64Data.substring(
    "data:image/".length,
    base64Data.indexOf(";base64")
  );
};

window.image64toCanvasRef = function (canvasRef, image64, pixelCrop)
{
  const canvas = canvasRef; // document.createElement('canvas');
  canvas.width = pixelCrop.width;
  canvas.height = pixelCrop.height;
  const ctx = canvas.getContext("2d");
  const image = new Image();
  image.src = image64;
  image.onload = function ()
  {
    ctx.drawImage(
      image,
      pixelCrop.x,
      pixelCrop.y,
      pixelCrop.width,
      pixelCrop.height,
      0,
      0,
      pixelCrop.width,
      pixelCrop.height
    );
  };
};

window.imgToBase64 = function (img)
{
  const canvas = document.createElement("canvas");
  const ctx = canvas.getContext("2d");
  canvas.width = img.width;
  canvas.height = img.height;

  // I think this won't work inside the function from the console
  img.crossOrigin = "anonymous";

  ctx.drawImage(img, 0, 0);

  return canvas.toDataURL();
};


window.getScrollbarWidth = function ()
{

  // Creating invisible container
  const outer = document.createElement('div');
  outer.style.visibility = 'hidden';
  outer.style.overflow = 'scroll'; // forcing scrollbar to appear
  outer.style.msOverflowStyle = 'scrollbar'; // needed for WinJS apps
  document.body.appendChild(outer);

  // Creating inner element and placing it in the container
  const inner = document.createElement('div');
  outer.appendChild(inner);

  // Calculating difference between container's full width and the child width
  const scrollbarWidth = (outer.offsetWidth - inner.offsetWidth);

  // Removing temporary elements from the DOM
  outer.parentNode.removeChild(outer);

  return scrollbarWidth;
}

window.isScrollVisible = function()
{
  return window.innerHeight < document.body.scrollHeight
}

window.GetPaddingRight = function ()
{
  var __ScrollWidth = window.getScrollbarWidth();

  if (window.isScrollVisible())
  {
    return "0px";
  }
  else
  {
    return window.getScrollbarWidth() + "px";
  }
}




window.getStylePixel = function (_Element)
{
  var __Style;
  if (window.getComputedStyle)
  {
    __Style = window.getComputedStyle(_Element, null);
  }
  else
  {
    __Style = _Element.currentStyle;
  }

  return __Style;
};

