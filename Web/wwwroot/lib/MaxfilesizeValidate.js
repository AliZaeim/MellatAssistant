jQuery.validator.unobtrusive.adapters.addBool("maxfilesize");

jQuery.validator.addMethod("maxfilesize",
    function (value, element, param) {
        console.log("length : " + $(element)[0].files.length);
        console.log("element: " + $(element).attr("name"));
        console.log("param: " + param);
        if ($(element)[0].files.length > 0) {
            var fileSize = $(element)[0].files[0].size;
            var maxSize = 1048576;

            var validSize = fileSize < maxSize;
            console.log(validSize);
            return validSize;
        }
        else {
            return true;
        }
    });
//jQuery.validator.unobtrusive.adapters.add("requiredIf", ["otherproperty", "otherpropertyvalue"],
//    function (options) {
//        options.rules["requiredif"] = options.params;
//        options.messages["requiredif"] = options.message
//    });



