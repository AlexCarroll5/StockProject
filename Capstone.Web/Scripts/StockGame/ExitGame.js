$(document).ready(function () {
    $("a#exit").click(DisplayResults);

    function DisplayResults() {

        $.ajax({
            url: ajaxURL + "/api/Update",
            type: "GET",
            dataType: "json"
        }).done(function (data) {
            console.log(data);
        });
    }
})