jQuery.validator.addMethod("filesize",
    function (value, element, param) {
        console.log($(element)[0].files.length);
        if ($(element)[0].files.length > 0) {
            var fileSize = $(element)[0].files[0].size;
            var maxSize = 104857.6;

            var validSize = fileSize*1024*1024 < maxSize;
            console.log(validSize);
            return validSize;
        }
        return false;
    });

jQuery.validator.unobtrusive.adapters.addBool("filesize");
