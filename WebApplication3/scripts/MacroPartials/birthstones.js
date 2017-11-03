(function ($) {
    $(".birthstone-name > a").on("click", function () {
        var $this = $(this);
        var $parent = $this.parent();
        var $panel = $(".birthstone-description." + $this.text());
        $parent.siblings().removeClass("active");
        $parent.addClass("active");
        $panel.siblings().removeClass("active");
        $panel.addClass("active");
    }).first().trigger("click");
})(jQuery)