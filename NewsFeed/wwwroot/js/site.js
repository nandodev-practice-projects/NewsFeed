// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //News Feed  partial
    GetSubscriptionsNews();
    $("#SeeAllNews").show();

    //Categories partial
    GetCategories();
});

function GetAllNews() {

    $.get('/News/GetAllNews', function (data) {
        $('#partial-feed').html(data);
    });

    $("#SeeSubscriptions").css("display", "inline");
    $("#SeeAllNews").css("display", "none");
}

function GetSubscriptionsNews() {

    $.get('/News/GetSubscriptionsNews', function (data) {
        $('#partial-feed').html(data);
    });

    $("#SeeSubscriptions").css("display","none");
    $("#SeeAllNews").css("display", "inline");
}

function GetCategories() {

    $.get('/Categories/GetAll', function (data) {
        $('#partial-categories').html(data);
    });
}

function ConfirmUnsubscribe(categoryID, categoryName) {

    $('#unsubscription-category-title').html(categoryName);
    $("#unsubscription").attr('data-id', categoryID);
    $('#confirm-unsubscription').modal('show');
}

function ConfirmSubscribe(categoryID, categoryName) {

    $('#subscription-category-title').html(categoryName);
    $("#subscription").attr('data-id', categoryID);
    $('#confirm-subscription').modal('show');
}

function Subscribe(categoryID) {

    $.ajax({
        url: "/Subscriptions/Subscribe",
        method: 'GET',
        data: {
            categoryID: $("#subscription").data("id")
        },
        success: function (result) {
            GetCategories();
            GetSubscriptionsNews();
            $('#confirm-subscription').modal('hide');
        },
        error: function (result) {
            $('#confirm-subscription').modal('hide');
            alert("The subscription could not be done");
        }
    });
}

function Unsubscribe(categoryID) {

    $.ajax({
        url: "/Subscriptions/Unsubscribe",
        method: 'GET',
        data: {
            categoryID: $("#unsubscription").data("id")
        },
        success: function (result) {
            GetCategories();
            GetSubscriptionsNews();
            $('#confirm-unsubscription').modal('hide');
        },
        error: function (result) {
            $('#confirm-unsubscription').modal('hide');
            alert("The subscription could not be done");
        }
    });
}

function Search() {

    $('#btn-search').html("Processing...");
    $('#btn-search').attr('disabled', true);
    $.ajax({
        url: '/News/Search',
        method: 'POST',
        data: {
            searchText: $('#text-search').val()
        },
        success: function (data) {
            $('#partial-feed').html(data);
            $('#btn-search').html("Search");
            $('#btn-search').attr('disabled', false);
            $("#text-search").val("");
        },
        error: function (data) {
            $('#btn-search').html("Search");
            $('#btn-search').attr('disabled', false);
            $("#text-search").val("");
            alert(data);
        }
    });
}

function AddNews() {

    var formData = serializeForm("#form-add-news");
    var _data = JSON.stringify(formData);

    $('#btn-add-news').html("Processing...");
    $('#btn-add-news').attr('disabled', true);
    $.ajax({
        url: '/News/AddNews',
        type: 'POST',
        contentType : 'application/json; charset=utf-8',
        data: _data,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            $('#btn-add-news').html("Search").attr('disabled', false);
            $("#form-add-news").get(0).reset();
            $('#form-result-message').show().html(data);
           
        },
        error: function (data) {
            $('#btn-add-news').html("Search").attr('disabled', false);
            $("#form-add-news").get(0).reset();
            $('#form-result-message').show().html("<p>The news couldn't be added</p>");
        }
    });
}

$('#add-news').on('show.bs.modal', function (e) {

    $('#form-result-message').hide();
    $("#form-add-news").get(0).reset();
    $("#add-news").modal("show");   
});

/*Helpers*/

/**
 * Get form value and convert it in a form object
 * @param {string} selector 
 * @returns {jsonValues} 
 */
function serializeForm(selector) {
    var formValues = $(selector).serializeArray();
    if (!formValues) return [];
    var objectValues = {};
    for (var i = 0; i < formValues.length; i++) {
        if (formValues[i].value) {
            //TODO: Remove this
            objectValues[formValues[i].name] = formValues[i].value;
        }
    }

    return objectValues;
}







