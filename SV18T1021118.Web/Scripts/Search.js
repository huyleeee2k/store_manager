function doSearch(page) {
    var url = $("#searchInput").prop("action");
    var input = $("#searchInput").serializeArray();
    input.push({ "name": "page", "value": page });

    $.ajax({
        type: "POST",
        url: url,
        data: input,
        success: function (data) {
            $("#searchResult").html(data);
        }
    });
}

function doSearchProduct(page, categoryName, supplierName) {
    var url = $("#searchInput").prop("action");
    var input = $("#searchInput").serializeArray();
    input.push({ "name": "page", "value": page });
    input.push({ "name": "supplierName", "value": supplierName });
    input.push({ "name": "categoryName", "value": categoryName });

    $.ajax({
        type: "POST",
        url: url,
        data: input,
        success: function (data) {
            $("#searchResult").html(data);
        }
    });
}