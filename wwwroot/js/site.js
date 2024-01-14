// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showCreateImportForm() {

    var importsControl = document.getElementById('imports-control');
    $(importsControl).empty();
        
    $.ajax({
        url: 'GetCreateImportForm',
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            $(importsControl).html(result);
        }
    });

}

function addImport() {

    var productSelect = document.getElementById("product-select");
    var quantityInput = document.getElementById("quantity-input");

    var importData = {
        "ProductID": productSelect.value,
        "Quantity": quantityInput.value
    };

    console.log(importData);

    var importsControl = document.getElementById('imports-control');

    $.ajax({
        url: "UpdateImports",
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'html',
        data: JSON.stringify(importData),
        success: function (result) {
            $(importsControl).html(result);
        }
        
    });

}
