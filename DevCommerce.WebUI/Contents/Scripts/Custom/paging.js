(function ($) {
    $.fn.paging = function () {
        return $.paging;
    }
    $.paging = function () {

    };
    $.paging.Initialize = function () {

    };
    $.paging.getPage = function (pageNumber, categoryId, brandId) {
        var parameters = {
            pageNumber: pageNumber,
            categoryId: categoryId,
            brandId: brandId
        };

        $.devScript.post("/Product/ProductList/", parameters, function (response) {
            $(".products").html(response);
            $.devScript.scrollTo('products');
        }, "html", false, true, true);
    };
    $.paging.Initialize();
})(jQuery);

