$(document).ready(function () {
    $("ul > li.mt-list-item .list-item-content > h3 a").on("click", function () {
        var parent = $(this).parent().parent().parent();
        var divIcon = $(parent).find(".list-icon-container");
        var icon = $(divIcon).find("i");

        if ($(divIcon).hasClass("done")) {
            divIcon.removeClass("done")

            if ($(icon).hasClass("icon-check")) {
                $(icon).removeClass("icon-check");
                $(icon).addClass("icon-close");
            }
        }
        else {
            $(divIcon).addClass("done")
            if ($(icon).hasClass("icon-close")) {
                $(icon).removeClass("icon-close");
                $(icon).addClass("icon-check");
            }
        }
    });
})