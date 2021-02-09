export function OnObservationTypeChange(executionContext) {
    var formContext = executionContext.getFormContext();

    var selectedObservationType =
        formContext.getAttribute("tt_observationtype").getValue();

    if (selectedObservationType == 206340002) {
        formContext.ui.setFormNotification("Please provide a detailed summary of your findings", "INFO", "5001");

        formContext.getControl("tt_summary").setNotification("Please provide a detailed summary of your findings", "6001");
    }
    else {
        formContext.ui.clearFormNotification("5001");

        formContext.getControl("tt_summary").clearNotification("6001");
    }
}