$(document).ready(function () {
    let ajaxURL = "http://localhost:55601/"

    getStocksAjax();


    function getStocksAjax() {
        $.ajax({
            url: ajaxURL + "api/ListOfAvailableStocks",
            type: "GET",
            dataType: "json"
            

        }).done(function (data) {
            for (let i = 0; i < data._stocks.length; i++) {
                let stockBlock = $("<div>").addClass("row");
                let stockSymbol = $("<div>").text(data._stocks[i].Symbol).addClass("col");
                let companyName = $("<div>").text(data._stocks[i].CompanyName).addClass("col");
                let price = $("<div>").text(data._stocks[i].CurrentPrice).addClass("col");
                stockBlock.append(stockSymbol);
                stockBlock.append(companyName);
                stockBlock.append(price);
                $("#stocks").append(stockBlock);

                
            }
        });

    }
})