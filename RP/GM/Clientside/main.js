class WebBrowser {
    constructor(name, path) {
        this.cursor = false,
            this.open = false,
            this.external = false,
            this.headless = false,
            this.chat = false,
            this.hud = false,

            this.name = name,
            this.path = path;
    }

    load() {
        if (this.open) {
            return;
        }

        const resolution = API.getScreenResolution();

        this.browser = API.createCefBrowser(resolution.Width, resolution.Height, !this.external);
        API.setCefBrowserPosition(this.browser, 0, 0);
        API.setCefBrowserHeadless(this.browser, this.headless);
        API.loadPageCefBrowser(this.browser, this.path);
        //G:/Documents/GTANetwork/Server/resources/RP/Clientside/Resources/boilerplate.html

        if (!this.chat) { API.setCanOpenChat(false); }
        if (!this.hud) { API.setHudVisible(false); }
        if (this.cursor) { API.showCursor(true); }
        this.setOpen(true);
    }

    destroy() {
        API.destroyCefBrowser(this.browser);

        if (!this.chat) { API.setCanOpenChat(true); }
        if (!this.hud) { API.setHudVisible(true); }
        if (this.cursor) { API.showCursor(false); }

        this.setOpen(false);
    }

    eval(evalString) {
        this.browser.eval(evalString);
    }

    setExternal(newValue) { this.external = newValue;}
    setHeadless(newValue) { this.headless = newValue; }
    setCursorVisible(newValue) { this.cursor = newValue; }
    setChatVisible(newValue) { this.chat = newValue; }
    setHudVisible(newValue) { this.hud = newValue; }

    setOpen(newValue) { this.open = newValue; }
}

var cef = new CefHelper('G:/Documents/GTANetwork/Server/resources/RP/Clientside/Resources/boilerplate.html');

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case "loginscript_loginsuccess":
            API.setHudVisible(true);
            API.setChatVisible(true);
            API.setGameplayCameraActive();
            CEF.destroy();
            break;
        case "loginscript_show":
            var playerPos = new Vector3(690, 933.82, 371.64);
            var playerRot = new Vector3(-20, 0, -8.86);
            let newCamera = API.createCamera(playerPos, playerRot);
            API.setChatVisible(false);
            API.setHudVisible(false);
            API.setActiveCamera(newCamera);
            cef.show();
            break;
        case "loginscript_loginfailed":
            cef.eval("wrong_login();");
            break;
    }
});

API.onServerEventTrigger.connect(function (eventName, args) {

    if (eventName === "CEFDestroy") {

        CEF.destroy();
    }
    else if (eventName === "CEFController") {

        CEF.show(false, args[0]);
        API.sendNotification("~r~Press F4 to hide the interface.");
    }

    else if (eventName === "CEFController_CharMenu") {

        CEF.show(false, args[0], true, false);
        API.sendNotification("Press F1 to toggle the menu.\nPress F2 to see your mouse.\n~r~Press F4 to hide the interface.");
    }
});

function ulogin(email, password) {
    API.triggerServerEvent("loginscript_login", email, password);
}