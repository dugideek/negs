
var ContactForm = ContactForm || (function () {
    var contactForm;

    var mailOnBegin = function () {
        $.validator.unobtrusive.parse(contactForm);
        $("#SubmitContact").attr("disabled", true);
    };

    var mailOnSuccess = function (xhr) {
        $("#ContactUsForm").slideUp();
        $("#SubmitSuccess").show();
    };

    var mailOnFailure = function (xhr) {
        var response = xhr.responseJSON;
        if (response.status === 400 && response.message.indexOf("ReCaptcha") > -1) {
            $("#ReCaptchaValidateMessage").show();
            $("#SubmitContact").attr("disabled", false);
        }
        else {
            $("#SubmitFailure").show();
        }
    };

    var init = function () {
        contactForm = $("#ContactUsForm");
    };

    return {
        init: init,
        mailOnBegin: mailOnBegin,
        mailOnSuccess: mailOnSuccess,
        mailOnFailure: mailOnFailure
    };
})();

(function ($) {
    $(document).ready(function () {
        ContactForm.init();
    })
})(jQuery);

