$(document).ready(function () {
    let ajaxURL = "http://localhost:55601/"
    //let ajaxURL = "http://192.168.51.117/SMGame/"
    //var UserNumber = GetUserNumber();

    var UserNumber = $("#PlayerUsername").data().player;

    AddUserToGame();

    getStocksAjax();

    GetUserHoldings();


    ReloadPage();


    function AddUserToGame() {
        $.ajax({
            url: ajaxURL + "/api/AddUserToGame",
            type: "POST",
            dataType: "json",
            data: {
                userId: Number(UserNumber),
            }
        }).done(function (data){
        });
    }
    function ReloadPage(){
        setInterval(function () {  UpdateStocks(); }, 1257);
    }

    function GetUserHoldings() {
        $.ajax({
            url: ajaxURL + "/api/UserStocks",
            type: "GET",
            dataType: "json",
            data: {
                userId: UserNumber,
            }

        }).done(function (data) {
            for (let i = 0; i < data._userStocks.length; i++) {
                if (data._userStocks[i].Shares > 0) {
                    $("#sharesOf" + data._userStocks[i].UserStock.StockID).text(data._userStocks[i].Shares + "($" + data._userStocks[i].PurchasePrice.toFixed(2) + ")");
                }
                else {
                    $("#sharesOf" + data._userStocks[i].UserStock.StockID).text("");
                }
            }
        });

    }

    function getStocksAjax() {
        $.ajax({
            url: ajaxURL + "api/ListOfAvailableStocks",
            type: "GET",
            dataType: "json"
        }).done(function (data) {
            GetAvailableStocks(data);
        });

    }

    function BuyStock(id, userID) {

        let sharesToBuy = $("#stockID" + id).val();
        if (sharesToBuy > 0) {
            let currPrice = Number($("#priceOf" + id).text())

            $.ajax({
                url: ajaxURL + "/api/BuyStock",
                type: "POST",
                dataType: "json",
                data: {
                    userId: userID,
                    stockId: id,
                    shares: sharesToBuy
                }

            }).done(function (data) {
                UpdateAvailableStockPrice(data);
           });
         }
    }

    function SellStock(id, userID) {

        let sharesToSell = $("#stockID" + id).val();
        if (Number(sharesToSell) > 0) {
            sharesToSell = Number(Number(sharesToSell) * -1);

            $.ajax({
                url: ajaxURL + "/api/BuyStock",
                type: "POST",
                dataType: "json",
                data: {
                    userId: userID,
                    stockId: id,
                    shares: sharesToSell
                }

            }).done(function (data) {
                UpdateAvailableStockPrice(data);
            });
        }
    }

    function UpdateStocks() {

        $.ajax({
            url: ajaxURL + "/api/GetCashBalances",
            type: "GET",
            dataType: "json"
        }).done(function (data) {
            UpdateCashBalances(data);
        });


        $.ajax({
            url: ajaxURL + "/api/Update",
            type: "GET",
            dataType: "json"
        }).done(function (data) {
            UpdateAvailableStockPrice(data);
        });
    }

    function UpdateAvailableStockPrice(data) {
        for (let i = 1; i < data._stocks.length + 1; i++) {
            $("#priceOf" + i).text("$" + data._stocks[i-1].CurrentPrice.toFixed(2));
        }
        GetUserHoldings();
    }

    function UpdateCashBalances(data) {
        for (let i = 0; i < data.length; i++) {
            if (data[i].IdOfUser == UserNumber) {
                $("#portfolioValue").text("Current $" + data[i].CurrentCash.toFixed(2).toLocaleString());
            }
        }
    }

    function GetAvailableStocks(data) {

        $("#stockTable").empty();

        for (let i = 0; i < data._stocks.length; i++) {

            let stockTableRow = $("<tr>");
            let stockShares = $('<td>').attr("id","sharesOf" + data._stocks[i].StockID);
            let stockSymbol = $("<td>").html('<button type="button" class="btn btn-link stockSymbol">' + data._stocks[i].Symbol + '</button>');
            let price = $("<td>").text("$" + data._stocks[i].CurrentPrice.toFixed(2)).attr("id", "priceOf" + data._stocks[i].StockID);
            var sharesToBuySell = document.createElement('input');
            sharesToBuySell.type = "text";
            sharesToBuySell.id = "stockID" + data._stocks[i].StockID;
            sharesToBuySell.setAttribute("size", 4);
            sharesToBuySell.setAttribute("maxlength", 5);
            let sharesInput = $("<td>");
            sharesInput.append(sharesToBuySell);
            var buyButton = document.createElement('button');
            buyButton.id = "buyStockId" + data._stocks[i].StockID;
            buyButton.innerText = "Buy";
            buyButton.onclick = function () {
                BuyStock(data._stocks[i].StockID, UserNumber);
            }
            $(buyButton).addClass("btn").addClass("btn-success").addClass("btn-sm");
            let bButtonCol = $("<td>")
            bButtonCol.append(buyButton);
            var sellButton = document.createElement('button');
            sellButton.id = "sellStockId" + data._stocks[i].StockID;
            sellButton.innerText = "Sell";
            sellButton.onclick = function () {
                SellStock(data._stocks[i].StockID, UserNumber);
            }
            $(sellButton).addClass("btn").addClass("btn-danger").addClass("btn-sm");
            let sButtonCol = $("<td>");
            sButtonCol.append(sellButton);

            let avgCol = $("<td>").attr("id", "costBasisOf" + data._stocks[i].StockID);
            let gainLoss = $("<td>").attr("id", "gainLossOf" + data._stocks[i].StockID);

            stockTableRow.append(stockSymbol);
            stockTableRow.append(price);
            stockTableRow.append(stockShares);
            stockTableRow.append(avgCol);
            stockTableRow.append(gainLoss);
            stockTableRow.append(bButtonCol);
            stockTableRow.append(sButtonCol);
            stockTableRow.append(sharesInput);

            $("#stockTable").append(stockTableRow);
        }

    }
})