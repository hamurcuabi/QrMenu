$(function () {
    $('.select2').select2();
    var urunMedia = $("#UrnMedia");

    var gorsel = $("#gorselSec");
    urunMedia.change(function () {

        if (urunMedia.val() != "") {

            gorsel.text(urunMedia.val());
        } else {
            gorsel.text("Görsel Seç");
        }
    });

});