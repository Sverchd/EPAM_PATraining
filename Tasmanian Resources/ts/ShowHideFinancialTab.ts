export function onLoad(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    console.log("This is fiddler!");
    formContext.data.process.addOnStageChange(onStageChange)

}

export function onStageChange(executionContext) {
    var formContext = executionContext.getFormContext();
    console.log("This is fiddler!");
    var stage = formContext.data.process.getSelectedStage();

    var stageName = stage.getName();

    if (stageName == "Watching" || stageName == "Planning") {
        formContext.ui.tabs.get("tab_Financials").setVisible(true);
    }
    else {
        formContext.ui.tabs.get("tab_Financials").setVisible(true);
    }
}