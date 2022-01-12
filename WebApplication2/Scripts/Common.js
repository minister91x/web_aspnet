window.Common = {
    ShowPoup: function (msgInput) {
        var data = {
            msg: msgInput
        };

        $.ajax({
            type: "GET", // GET / POST / DELETE
            cache: false,
            url: "/Common/PopupPartial",
            data: data,
            success: function (data) {
                $("#PopupDiv").html(data);
                $("#myModalPopup").modal("show");;
            }
        });
    }
};