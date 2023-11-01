$(function () {
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

    jQuery.validator.unobtrusive.adapters.add("requiredif", ["otherproperty", "otherpropertyvalue"],
        function (options) {
            options.rules["requiredif"] = options.params;
            options.messages["requiredif"] = options.message;
        });
    jQuery.validator.addMethod("requiredif",
        function (value, element, parameters) {
            var targetId = parameters.otherproperty;
            var targetValue = parameters.otherpropertyvalue;
            var otherpropertyvalue = (targetValue == null || targetValue == undefined ? "" : targetValue).toString();
            var otherpropertyElement = $('#' + targetId);
            if (!value.trim() && otherpropertyElement.val() == otherpropertyvalue) {
                var isValid = $.validator.methods.required.call(this, value, element, parameters);
                return isValid;
            }
            return true;
        }
    );
    
    jQuery.validator.unobtrusive.adapters.addBool("filesize");
    jQuery.validator.addMethod("filesize",
        function (value, element, param) {            
            if ($(element)[0].files.length > 0) {
                var fileSize = $(element)[0].files[0].size;                
                var maxSize = 1048576;                
                var validSize = fileSize < maxSize;                
                return validSize;
            }
            else {
                return true;
            }
        });
    jQuery.validator.unobtrusive.adapters.addBool("ncformat");
    jQuery.validator.addMethod("ncformat",
        function (value, element, param) {
            if (value !== "") {
                if (value.legth >= 9) {
                    var res = vmsNationalCode((this).value);
                    return res;
                }
            }
        });
    jQuery.validator.unobtrusive.adapters.addBool("allowedextensions");
    jQuery.validator.addMethod("allowedextensions",
        function (value, element, param) {
            var fileExtensions = ['jpeg', 'jpg', 'png', 'gif', 'JPEG', "JPG", "PNG", "GIF"];
            if ($(element)[0].files.length > 0) {
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtensions) == -1) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;            
        });
}(jQuery));