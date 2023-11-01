$(function () {
    jQuery.fn.highlight = function (str) {
        var regex = new RegExp(str, "gi");
        return this.each(function () {
            this.innerHTML = this.innerText.replace(regex, function (matched) {
                return "<span class='mark'>" + matched + "</span>";
            });
        });
    },
        owltopblog(),
        owlsecondevent(),
        owltopnevis(),
        owltoparticle(),
        owltoppost(),
        owlslider(),
        owlComments(),
        $(document).find(".NCField").on("blur", function () {
            var res = vmsNationalCode($(this).val());
            if (res === false) {
                $(this).siblings(".ncValid").text('کد ملی وارد شده نامعتبر است !');
            }
            else {
                $(this).siblings(".ncValid").text('');
            }
        }),

        $(document).find("#inpSearch").on("keyup",function () {
            var srch = $(this).val();
            if (srch !== "") {
                if (srch.length > 1) {
                    $.ajax({
                        type: "GET",
                        url: "./Index?handler=SearchAct",
                        contentType: "application/json",
                        dataType: "json",
                        data: { search: srch },
                        "timeout": 5000,
                        success: function (response) {
                            $('#searchDiv').empty();
                            $.each(response, function (index, val) {
                                if (response.length > 0) {
                                    $("#searchDiv").css("display", "block");
                                    var group = "<h6 class='text-right zbold'>" + val[index].type + "</h6>";
                                    $('#searchDiv').append(group);

                                    if (val.length > 0) {
                                        $.each(val, function (index2, valu) {
                                            var tit = "<h6 class='text-right mr-3 stit' data-link='" + valu.link + "'>" + valu.title + "</h6>";
                                            $('#searchDiv').append(tit);
                                            var com = "<p class='text-justify p-0 mr-3 scom' data-link='" + valu.link + "'>" + valu.comment + "</p>";
                                            $('#searchDiv').append(com);
                                            $.each(valu.tags, function (index3, valu2) {
                                                $('#searchDiv').append("<span class='stag text-muted'>" + " " + valu2 + " " + "</span>");
                                                if (index3 != valu.tags.length - 1) {
                                                    $('#searchDiv').append("<span>|</span>");
                                                }
                                            });
                                            $('#searchDiv').append("<hr class='p-0' />");
                                            $(".stit").highlight(srch);
                                            $(".scom").highlight(srch);
                                            $(".stag").highlight(srch);
                                        });
                                    }

                                }
                            });
                            $(".stit").on("click",function () {
                                $("#inpSearch").val($(this).text());
                                $('#searchDiv').css("display", "none");
                                var link = $(this).attr("data-link");
                                $("#indexBtnSearch").attr("href", link);

                                $("#indexBtnSearch").trigger("click");

                            });
                            $(".scom").on("click",function () {
                                var tit = $(this).prev().text();
                                $("#inpSearch").val(tit);
                                $('#searchDiv').css("display", "none");
                                var link = $(this).attr("data-link");
                                $("#indexBtnSearch").attr("href", link);

                                $("#indexBtnSearch").trigger("click");
                            });
                        },
                        failure: function (xhr, status, error) {
                            alert(error.message);
                        },
                        error: function (xhr, status, error) {
                            alert(error.message);
                        }
                    });
                }
                else {
                    $('#searchDiv').empty();
                    $("#searchDiv").css("display", "none");
                }

            }
            else {
                $("#searchDiv").css("display", "none");
            }
        });
});

