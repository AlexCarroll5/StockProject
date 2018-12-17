var root = 'https://api.iextrading.com/1.0/stock/';

$(function () {
    //$(".stockSymbol").on("click", onSaveButtonClick);
    //$("#stockSymbol").on("click", onStockSymbolClick);
    $("#stockSymbol").on('click', function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        //(... rest of your JS code)
        onStockSymbolClick();
    });

});

$(".stockSymbol").on('click', function (event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    //(... rest of your JS code)
    onStockSymbolClick();
});

function onStockSymbolClick(e) {
    $("#exampleModalLabel").text("Stock Detail View");
    $("#id").val("");
    $("#name").val("");
    $("#username").val("");
    $("#email").val("");

    $("#id").data("adduser", "true");
    $('#myModal').modal();
}

function onRowClick(e) {
    $("#exampleModalLabel").text("Edit User");
    $("#id").data("adduser", "false");

    var id = $(this).children().eq(0).text();
    var name = $(this).children().eq(1).text();
    var username = $(this).children().eq(2).text();
    var email = $(this).children().eq(3).text();

    $("#id").val(id);
    $("#name").val(name);
    $("#username").val(username);
    $("#email").val(email);

    $('#myModal').modal();
}

function onSaveButtonClick(e) {
    var id = $("#id").val();
    var name = $("#name").val();
    var username = $("#username").val();
    var email = $("#email").val();

    let isAddUser = $("#id").data("adduser");

    if (isAddUser == "true") {
        // Add the User Object
        $.ajax({
            url: root + '/users',
            method: 'POST',
            data: {
                name: name,
                username: username,
                email: email
            }
        }).done(function (data) {
            $('#myModal').modal('hide');
            loadUsers();
        }).fail(function (xhr, status, error) {
            console.log(error);
            $('#myModal').modal('hide');
            loadUsers();
        });
    }
    else {
        // Update the User Object
        $.ajax({
            url: root + '/users/' + id,
            method: 'PUT',
            data: {
                name: name,
                username: username,
                email: email
            }
        }).done(function (data) {
            $('#myModal').modal('hide');
            loadUsers();
        }).fail(function (xhr, status, error) {
            console.log(error);
            $('#myModal').modal('hide');
            loadUsers();
        });
    }
}

function onDeleteUser(e) {
    let id = $(e.target).data("id");

    // Delete the User Object
    $.ajax({
        url: root + '/users/' + id,
        method: 'DELETE'
    }).done(function (data) {
        $('#myModal').modal('hide');
        loadUsers();
    }).fail(function (xhr, status, error) {
        console.log(error);
        $('#myModal').modal('hide');
        loadUsers();
    });

    return false;
}