var drawVehicleHUD = false;
var drawAnimationHUD = false;
var currentMoney = null;
var res = API.getScreenResolution();

API.onUpdate.connect(function (sender, args) {

    if (drawAnimationHUD !== null) {
        API.drawText("Press F to stop", res.Width - 30, res.Height - 100, 0.5, 255, 186, 131, 255, 4, 2, false, true, 0);
    }

    if (currentMoney !== null) {
        API.drawText("$" + currentMoney, res.Width - 15, 50, 1, 115, 186, 131, 255, 4, 2, false, true, 0);
    }
    var player = API.getLocalPlayer();
    var inVeh = API.isPlayerInAnyVehicle(player);

    if (inVeh) {
        var veh = API.getPlayerVehicle(player);
        var vel = API.getEntityVelocity(veh);
        var health = API.getVehicleHealth(veh);
        var maxhealth = API.returnNative("GET_ENTITY_MAX_HEALTH", 0, veh);
        var healthpercent = Math.floor((health / maxhealth) * 100);
        var speed = Math.sqrt(
            vel.X * vel.X +
            vel.Y * vel.Y +
            vel.Z * vel.Z
        );
        var displaySpeedMph = Math.round(speed * 2.23693629);
        API.drawText(`${displaySpeedMph}`, res_X - 60, res_Y - 200, 1, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`mph`, res_X - 20, res_Y - 180, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`Health:`, res_X - 70, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 255, 255, 255, 255, 4, 2, false, true, 0);
        if (healthpercent < 60) {
            API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 219, 122, 46, 255, 4, 2, false, true, 0);
        }
        if (healthpercent < 30) {
            API.drawText(`${healthpercent}%`, res_X - 20, res_Y - 225, 0.5, 219, 46, 46, 255, 4, 2, false, true, 0);
        }
    }

    
});

API.onServerEventTrigger.connect(function (eventName, args) {

    if (eventName === "update_money_display") {
        currentMoney = Number(args[0]).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    }
    else if (eventName === "animation_text") {
        drawAnimationHUD = !drawAnimationHUD;
    }
});