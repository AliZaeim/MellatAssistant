//popular doctors_index_page

function owlComments() {
    $('#owl-Comments').owlCarousel({
        rtl: true,
        loop: true,
        nav: true,
        autoplay: true,
        smartSpeed: 1000,
        autoplayTimeout: 4000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            1200: {
                items: 4
            },
            1367: {
                items: 5
            }
        }
    });
}
//popular doctors_index_page
function owltopblog() {
    jQuery('#owl-topblog').owlCarousel({
        rtl: true,
        center: true,
        loop: true,
        nav: true,
        autoplay: true,
        smartSpeed: 1000,
        autoplayTimeout: 4000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            //600: {
            //    items: 5,
            //},
            //700: {
            //    items: 6,
            //},
            //847: {
            //    items: 7,
            //},
            //992: {
            //    items: 5,
            //},
            1200: {
                items: 2
            },
            1367: {
                items: 3,
            }
        }
    });
}
function owlsecondevent() {
    jQuery('#owl-secondevent').owlCarousel({
        rtl: true,
        //center: true,
        loop: true,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        autoplayTimeout: 5000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 2,
            },
            768: {
                items: 3,
            },
            //600: {
            //    items: 5,
            //},
            //700: {
            //    items: 6,
            //},
            //847: {
            //    items: 7,
            //},
            //992: {
            //    items: 5,
            //},
            1200: {
                items: 4
            },
            1367: {
                items: 5,
            }
        }
    });
}
//popular doctors_index_page
function owltoparticle() {
    jQuery('#owl-toparticle').owlCarousel({
        rtl: true,
        loop: true,
        nav: true,
        autoplay: true,
        smartSpeed: 2000,
        autoplayTimeout: 6000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            //600: {
            //    items: 5,
            //},
            //700: {
            //    items: 6,
            //},
            //847: {
            //    items: 7,
            //},
            //992: {
            //    items: 5,
            //},
            1200: {
                items: 4
            },
            1367: {
                items: 4,
            }
        }
    });
};
//popular doctors_index_page
function owltopnevis() {
    jQuery('#owl-topnevis').owlCarousel({
        rtl: true,
        loop: true,
        nav: true,
        autoplay: false,
        smartSpeed: 2000,
        autoplayTimeout: 6000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 2,
            },
            768: {
                items: 3,
            },
            //600: {
            //    items: 5,
            //},
            //700: {
            //    items: 6,
            //},
            //847: {
            //    items: 7,
            //},
            //992: {
            //    items: 5,
            //},
            1200: {
                items: 5
            }
            , 1367: {
                items: 5,
            }
        }
    });
};
//popular doctors_index_page
function owltoppost() {
    jQuery('#owl-toppost').owlCarousel({
        rtl: true,
        center: true,
        loop: true,
        nav: true,
        autoplay: true,
        smartSpeed: 1000,
        autoplayTimeout: 4000,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            480: {
                items: 1,
            },
            768: {
                items: 2,
            },
            //600: {
            //    items: 5,
            //},
            //700: {
            //    items: 6,
            //},
            //847: {
            //    items: 7,
            //},
            //992: {
            //    items: 5,
            //},
            1200: {
                items: 3
            },
            1367: {
                items: 3,
            }
        }
    });
};
function owlslider() {
    jQuery('#owl-slider').owlCarousel({
        items: 1,
        loop: true,
        autoplay: true,
        nav: true,
        autoplayTimeout: 3000,
        animateOut: 'fadeOut',
    });
};
function vmsNationalCode(input) {
    if (!/^\d{10}$/.test(input)
        || input == '0000000000'
        || input == '1111111111'
        || input == '2222222222'
        || input == '3333333333'
        || input == '4444444444'
        || input == '5555555555'
        || input == '6666666666'
        || input == '7777777777'
        || input == '8888888888'
        || input == '9999999999')
        return false;
    var check = parseInt(input[9]);
    var sum = 0;
    var i;
    for (i = 0; i < 9; ++i) {
        sum += parseInt(input[i]) * (10 - i);
    }
    sum %= 11;
    return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
};

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

        $(document).find("#inpSearch").on("keyup", function () {
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
                            $(".stit").on("click", function () {
                                $("#inpSearch").val($(this).text());
                                $('#searchDiv').css("display", "none");
                                var link = $(this).attr("data-link");
                                $("#indexBtnSearch").attr("href", link);

                                $("#indexBtnSearch").trigger("click");

                            });
                            $(".scom").on("click", function () {
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




