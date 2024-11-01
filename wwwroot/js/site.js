﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addImport() {

    var productSelect = document.getElementById("product-select");
    var quantityInput = document.getElementById("quantity-input");

    var importData = {
        "ProductID": productSelect.value,
        "Quantity": quantityInput.value
    };

    console.log(importData);

    var importsControl = document.getElementById('imports-control');

    var createInvoiceButton = document.getElementById('create-invoice-btn');
    createInvoiceButton.removeAttribute("disabled");

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

function addCheckEntry() {

    let productSelect = document.getElementById('product-select');
    let quantityInput = document.getElementById('quantity-input');

    var checkEntryData = {
        "ProductID": productSelect.value,
        "Quantity": quantityInput.value
    };

    var importsControl = document.getElementById('imports-control');

    var createInvoiceButton = document.getElementById('create-invoice-btn');
    createInvoiceButton.removeAttribute("disabled");

    $.ajax({
        url: "UpdateEntries",
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'html',
        data: JSON.stringify(checkEntryData),
        success: function (result) {
            $(importsControl).html(result);
        }
    });

}
