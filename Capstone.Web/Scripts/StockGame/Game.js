$(document).ready(function () {
    let ajaxURL = "http://localhost:55601/"

    getStocksAjax();

    GetUserHoldings();


    ReloadPage();



    function ReloadPage(){
        setInterval(function () {  UpdateStocks(); }, 1257);
    }

    function GetUserHoldings() {
        $.ajax({
            url: ajaxURL + "/api/UserStocks",
            type: "GET",
            dataType: "json",
            data: {
                userId: 1,
            }

        }).done(function (data) {
            for (let i = 0; i < data._userStocks.length; i++) {

                $("#sharesAndCBOf" + data._userStocks[i].UserStock.StockID).text(data._userStocks[i].Shares + "($" + data._userStocks[i].PurchasePrice.toFixed(2) + ")");
            }
        });

    }

    function getStocksAjax() {
        $.ajax({
            url: ajaxURL + "api/ListOfAvailableStocks",
            type: "GET",
            dataType: "json"


            //< tr >
            //<th scope="row">5($9.42)</th>
            //<td>SNAP</td>
            //<td>Snapchat</td>
            //<td>$10.53</td>
            //<td><button type="button" class="btn btn-success btn-sm">Buy</button></td>
            //<td><button type="button" class="btn btn-danger btn-sm">Sell</button></td>
            //<td>xxx</td>
            //    </tr>

        }).done(function (data) {
            //for (let i = 0; i < data._stocks.length; i++) {
                //let stockBlock = $("<div>").addClass("row");
                //let stockSymbol = $("<div>").text(data._stocks[i].Symbol).addClass("col");
                //let companyName = $("<div>").text(data._stocks[i].CompanyName).addClass("col");
                //let price = $("<div>").text(data._stocks[i].CurrentPrice).addClass("col");
                //var sharesToBuySell = document.createElement('input');
                //sharesToBuySell.type = "number"; 
                //sharesToBuySell.id = "stockID" + data._stocks[i].StockID;
                //var buyButton = document.createElement('button');
                //buyButton.id = "buyStockId" + data._stocks[i].StockID;
                //buyButton.innerText = "Buy " + data._stocks[i].Symbol;
                //var sellButton = document.createElement('button');
                //sellButton.id = "sellStockId" + data._stocks[i].StockID;
                //sellButton.innerText = "Sell " + data._stocks[i].Symbol;
                //stockBlock.append(stockSymbol);
                //stockBlock.append(companyName);
                //stockBlock.append(price);
                //stockBlock.append(sharesToBuySell).addClass('col');
                //stockBlock.append(buyButton).addClass('col');
                //stockBlock.append(sellButton).addClass('col');

                //$("#stocks").append(stockBlock);

                //let stockTableRow = $("<tr>");
                //let stockH = $('<th scope="row">');
                //let stockSymbol = $("<td>").text(data._stocks[i].Symbol);
                //let companyName = $("<td>").text(data._stocks[i].CompanyName);
                //let price = $("<td>").text(data._stocks[i].CurrentPrice);
                //var sharesToBuySell = document.createElement('input');
                //sharesToBuySell.type = "number";
                //sharesToBuySell.id = "stockID" + data._stocks[i].StockID;
                //let sharesInput = $("<td>").attr("width", 15);
                //sharesInput.append(sharesToBuySell);
                //var buyButton = document.createElement('button');
                //buyButton.id = "buyStockId" + data._stocks[i].StockID;
                //buyButton.innerText = "Buy";
                //buyButton.onclick = function () {
                //    BuyStock(data._stocks[i].StockID, 1);
                //}
                //$(buyButton).addClass("btn").addClass("btn-success").addClass("btn-sm");
                //let bButtonCol = $("<td>")
                //bButtonCol.append(buyButton);
                //var sellButton = document.createElement('button');
                //sellButton.id = "sellStockId" + data._stocks[i].StockID;
                //sellButton.innerText = "Sell";
                //$(sellButton).addClass("btn").addClass("btn-danger").addClass("btn-sm");
                //let sButtonCol = $("<td>");
                //sButtonCol.append(sellButton);

                //stockTableRow.append(stockH);
                //stockTableRow.append(stockSymbol);
                //stockTableRow.append(companyName);
                //stockTableRow.append(price);
                //stockTableRow.append(bButtonCol);
                //stockTableRow.append(sButtonCol);
                //stockTableRow.append(sharesInput);

                //$("#stockTable").append(stockTableRow);



            //}
            GetAvailableStocks(data);
        });

    }

    function BuyStock(id, userID) {

        let sharesToBuy = $("#stockID" + id).val();
        if (sharesToBuy > 0) {
            alert(id + sharesToBuy);
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
        if (sharesToSell > 0) {
            alert(id + sharesToSell);
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

    function GetAvailableStocks(data) {

        $("#stockTable").empty();

        for (let i = 0; i < data._stocks.length; i++) {

            let stockTableRow = $("<tr>");
            let stockH = $('<th scope="row">').attr("id","sharesAndCBOf" + data._stocks[i].StockID);
            let stockSymbol = $("<td>").text(data._stocks[i].Symbol);
            let companyName = $("<td>").text(data._stocks[i].CompanyName);
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
                BuyStock(data._stocks[i].StockID, 1);
            }
            $(buyButton).addClass("btn").addClass("btn-success").addClass("btn-sm");
            let bButtonCol = $("<td>")
            bButtonCol.append(buyButton);
            var sellButton = document.createElement('button');
            sellButton.id = "sellStockId" + data._stocks[i].StockID;
            sellButton.innerText = "Sell";
            sellButton.onclick = function () {
                SellStock(data._stocks[i].StockID, 1);
            }
            $(sellButton).addClass("btn").addClass("btn-danger").addClass("btn-sm");
            let sButtonCol = $("<td>");
            sButtonCol.append(sellButton);

            stockTableRow.append(stockH);
            stockTableRow.append(stockSymbol);
            stockTableRow.append(companyName);
            stockTableRow.append(price);
            stockTableRow.append(bButtonCol);
            stockTableRow.append(sButtonCol);
            stockTableRow.append(sharesInput);

            $("#stockTable").append(stockTableRow);
        }

    }
})