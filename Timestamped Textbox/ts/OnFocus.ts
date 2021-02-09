import { getDateTime } from './utils'

export function onFocus(textArea) {
    let dateTimeText = getDateTime();
    let index = dateTimeText.length;

    textArea.value = dateTimeText + "\n" + textArea.value;
    textArea.selectionStart = index;
    textArea.selectionEnd = index;
}