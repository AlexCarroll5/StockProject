$(document).ready(function () {
    let ajaxURL = "http://localhost:55601/"

    getStocksAjax();


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
            for (let i = 0; i < data._stocks.length; i++) {
                let stockBlock = $("<div>").addClass("row");
                let stockSymbol = $("<div>").text(data._stocks[i].Symbol).addClass("col");
                let companyName = $("<div>").text(data._stocks[i].CompanyName).addClass("col");
                let price = $("<div>").text(data._stocks[i].CurrentPrice).addClass("col");
                var sharesToBuySell = document.createElement('input');
                sharesToBuySell.type = "number"; 
                sharesToBuySell.id = "stockID" + data._stocks[i].StockID;
                var buyButton = document.createElement('button');
                buyButton.id = "buyStockId" + data._stocks[i].StockID;
                buyButton.innerText = "Buy " + data._stocks[i].Symbol;
                var sellButton = document.createElement('button');
                sellButton.id = "sellStockId" + data._stocks[i].StockID;
                sellButton.innerText = "Sell " + data._stocks[i].Symbol;
                stockBlock.append(stockSymbol);
                stockBlock.append(companyName);
                stockBlock.append(price);
                stockBlock.append(sharesToBuySell).addClass('col');
                stockBlock.append(buyButton).addClass('col');
                stockBlock.append(sellButton).addClass('col');

                $("#stocks").append(stockBlock);

                
            }
        });

    }
})