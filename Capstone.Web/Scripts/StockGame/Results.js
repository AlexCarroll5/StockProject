﻿$(document).ready(function () {
    //let ajaxURL = "http://localhost:55601/";
    //let ajaxURL = "http://192.168.51.117/SMGame/";
    let ajaxURL = "http://stocktycoon.apphb.com/"
    $(".new-game-button").click(NewGame);

    function NewGame() {
        $.ajax({
            url: ajaxURL + "api/CheckSetting",
            type: "GET",
            dataType: "json"
        }).done(function (data) {
            if (data.SettingValue == 0) {
                SwitchSetting();
                location.replace(ajaxURL + "StockGame/Settings");
            }
            else {
                location.replace(ajaxURL + "StockGame/Game");
            }
        });    
    }

    function SwitchSetting() {

        $.ajax({
            url: ajaxURL + "api/SwitchSettings",
            type: "POST",
            data: {
                switched: false,
            },
            dataType: "json"
        }).done(function (data) { });
    }
})