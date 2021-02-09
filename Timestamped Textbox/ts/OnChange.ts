/// <reference types="xrm" />
export function onChange(xrm, fieldName, textArea) {
    xrm.Page.getAttribute(fieldName).setValue(textArea.value);
}