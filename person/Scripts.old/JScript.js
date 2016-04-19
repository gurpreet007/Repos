document.write('<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>')
document.write('<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>')

var CUR_YR = (new Date()).getFullYear();

var DT_PCKR_OPTS = { 
    dateFormat: "dd-M-yy", 
    changeMonth: true, 
    changeYear: true,
    yearRange: (CUR_YR - 10) + ':' + (CUR_YR + 1)
};

function doWaterMark(myid, wmtext) {
    var slctr = '#' + myid;
    $(slctr).focus(function () {
        if ($(slctr).hasClass('watermarked')) {
            $(slctr).removeClass('watermarked');
            if ($(slctr).val() == wmtext) {
                $(slctr).val('');
            }
        }
    });
    $(slctr).focusout(function () {
        if ($(slctr).val() == '') {
            $(slctr).addClass('watermarked');
            $(slctr).val(wmtext);
        }
        else if ($(slctr).val() == wmtext) {
            $(slctr).addClass('watermarked');
        }
    });
    $(slctr).change(function () {
        if ($(slctr).hasClass('watermarked')) {
            $(slctr).removeClass('watermarked');
        }
    });
    $(slctr).trigger('focusout');
}

function doAutoComp(myid, myurl) {
    var slctr = '#' + myid;
    $.ajax({
        type: "POST",
        url: myurl,
        dataType: "json",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var datafromServer = data.d.split(":");
            $(slctr).autocomplete({
                source: function (req, responseFn) {
                    var rep = req.term.replace(" ", '.*', "g");
                    var matcher = new RegExp(rep, "i");
                    var a = $.grep(datafromServer, function (item, index) {
                        return matcher.test(item);
                    });
                    responseFn(a);
                }
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus+" 123");
        }
    });
}