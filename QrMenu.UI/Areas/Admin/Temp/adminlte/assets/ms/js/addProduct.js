class IsletmeUrunEkleModelData {
    constructor(Id, Fiyat, Ad) {
        this.Id = Id;
        this.Fiyat = Fiyat;
        this.Ad = Ad;
    }
}

var model = [];
var urunekle = $("#urunekle");
urunekle.change(function () {
    var secilenler = new Array();
    model = [];
    $("#fiyatlar").html('');
    secilenler.push($("#urunekle option:selected"));

    $.each(secilenler[0], function (k, v) {

        $("#fiyatlar").append('<label>' + v.innerText + ' için Fiyat</label> <div class="input-group"><div class="input-group-prepend"><span class="input-group-text">₺</span></div><input type="number" id=' + v.value + ' required class="form-control"></div>')
        model.push([{ "Ad": v.innerText, "Id": v.value, Fiyat: null }]);

    })
})

$("form").submit(function (event) {
    event.preventDefault();
    var model3 = {};
    var model4 = [];
    for (var i = 0; i < model.length; i++) {

        $('input[type=number]').each(function (k, v) {

            if (model[i][0]["Id"].includes(v.id)) {
                model[i][0]["Fiyat"] = v.value;

            }
        })
        var model2 = new IsletmeUrunEkleModelData(Id = model[i][0]["Id"], Fiyat = model[i][0]["Fiyat"], Ad = model[i][0]["Ad"]);
        model4[i] = model2;
    }
    model3["IsletmeUrunEkleModelList"] = model4;
    var obj = JSON.stringify(model3);
    $.ajax({
        url: "/Isletme/IsletmeUrunEkle",
        method: "POST",
        data: { model: obj },
        success: function () {
            location.reload();
        }

    })


});
