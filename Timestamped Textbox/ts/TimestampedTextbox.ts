/// <reference types="xrm" />
import { getQueryParams } from './utils'
import { onFocus } from './OnFocus'
import { onChange } from './OnChange'

var xrm;
var fieldName;
var textArea;

module.exports = {

  onLoad: function () {
    textArea = document.getElementById("timestampedTextarea");
    const params = getQueryParams(window.location.search);
    console.log(params);
    if (!params['data']) {
      textArea.value = "No Name for field found in parameters";
      return;
    }

    fieldName = params['data'];
    xrm = window.parent.Xrm;
    textArea.value = xrm.Page.getAttribute(fieldName).getValue();

    textArea.addEventListener("focus", function () { onFocus(textArea) });
    textArea.addEventListener("change", function () { onChange(xrm, fieldName, textArea) });
  },

  testFunc: function () {
    console.log("This is testfunc");
  }
}
