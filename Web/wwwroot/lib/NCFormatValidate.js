jQuery.validator.addMethod("ncformat",
    function (value, element, param) {
        if (value !== "") {
            if (value.legth >= 9) {
                var res = vmsNationalCode((this).value);
                return res;
            }
        }
    });

jQuery.validator.unobtrusive.adapters.addBool("ncformat");