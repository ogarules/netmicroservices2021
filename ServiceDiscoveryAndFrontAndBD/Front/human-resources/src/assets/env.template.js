(function(window) {
    window["env"] = window["env"] || {};

    // Environment variables
    window["env"]["_HR_hostApi"] = "${HRhostApi}";
    window["env"]["_HR_oktaIssuer"] = "${HRoktaIssuer}";
    window["env"]["_HR_oktaClient"] = "${HRoktaClient}";
})(this);