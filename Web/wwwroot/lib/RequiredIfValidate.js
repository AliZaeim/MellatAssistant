
$(function () {
    jQuery.validator.unobtrusive.adapters.add("requiredif", ["otherproperty", "otherpropertyvalue"],
        function (options) {
            options.rules["requiredif"] = options.params;
            options.messages["requiredif"] = options.message
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
}(jQuery));