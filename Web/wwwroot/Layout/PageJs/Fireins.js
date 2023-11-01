function fileValidation() {
    var fileInput =
        document.getElementById('file');
    var filePath = fileInput.value;
    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.gif||\.png|\.pdf)$/i;

    if (!allowedExtensions.exec(filePath)) {

        Swal.fire({
            icon: "success",
            title: 'لطفا فایل از نوع تصویر یا pdf انتخاب کنید !',
            icon: "danger"
        });
        fileInput.value = '';
        return false;
    }
}

document.querySelectorAll('.uploadFile').forEach(function (el) {
    el.addEventListener('change', function () {
        var parent = this.parentElement;
        parent.querySelector('.valid').innerText = '';        
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return;
        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file
            reader.onloadend = function () { // set image data as background of div               
                parent.querySelector('.imagePreview').style.backgroundImage = "url(" + this.result + ")";
            }
        }
    });
});



function validateImageInput(inpId, inpValidId) {
    var inp = document.getElementById(inpId);
    var inpValid = document.getElementById(inpValidId);
    if (inp !== null && typeof (inp) !== "undefined") {
        if (inp.files.length == 0) {
            if (inpValid !== null && typeof (inpValid) !== "undefined") {
                inpValid.innerText = "لطفا تصویر را انتخاب کنید !";
            }
        }
    }
}
function Submit_FireIns_Step1() {
    var vlid = true;
    
    var hidden_nc_image = document.getElementById("InsurerNCImage");
    var hidden_AttributedLetterImage = document.getElementById("AttributedLetterImage");
    var hidden_PayrollDeductionImage = document.getElementById("PayrollDeductionImage");
    var nc_image_upload_el = document.getElementById("fireinsurernc-image-upload");
    var pd_image_upload_el = document.getElementById("pd-image-upload");
    var al_image_upload_el = document.getElementById("al-image-upload");
    if (nc_image_upload_el !== null && typeof (nc_image_upload_el) !== "undefined") { 
        if (hidden_nc_image.value === null) {
            if (nc_image_upload_el.files.length == 0) {
                document.getElementById("fireinsurerNCImageValid").innerText = "لطفا تصویر کارت ملی را انتخاب کنید !";
                vlid = false;
            }
        }
        
    }
    
    if (al_image_upload_el !== null && typeof (al_image_upload_el) !== "undefined") {
        var al_valid_el = document.getElementById("alImageValid");
        if (al_valid_el !== null && typeof (al_valid_el) !== "undefined") {
            if (hidden_AttributedLetterImage === null) {
                if (al_image_upload_el.files.length == 0) {
                    al_valid_el.innerText = "لطفا تصویر معرفی نامه منتسب را انتخاب کنید !";
                    vlid = false;
                }
            }
            
        }
    }
    var payinstall = document.getElementById('payininstallment');
    if (payinstall !== null && typeof (payinstall) !== "undefined") {
        if (payinstall.checked) {
            if (pd_image_upload_el !== null && typeof (pd_image_upload_el) !== "undefined") {
                var pd_valid_el = document.getElementById("pdimageValid");
                if (pd_valid_el !== null && typeof (pd_valid_el !== "undefined")) {
                    if (hidden_PayrollDeductionImage === null) {
                        if (pd_image_upload_el.files.length == 0) {
                            pd_valid_el.innerText = "لطفا تصویر رضایت کسر از حقوق را انتخاب کنید !";
                            vlid = false;
                        }
                    }
                }
            }
        }        
    }
    
    if (vlid === false) {
        return false;
    }
}

async function fetchText(element) {   
    let response = await fetch('/ThirdParty/GetSalesExCodeUserInfo?code=' + sellercode_input.value);

    console.log(response.status); // 200
    console.log(response.statusText); // OK

    if (response.status === 200) {
        
        let data = await response.json();
        element.innerText = data.info;
        // handle data
    }
}
var btnviewseller = document.getElementById("btnViewSeller");
var sellercode_input = document.getElementById("SellerCode");
btnviewseller.addEventListener("click", function () {
    var sellerinfo_span = document.getElementById("sellerinfo");
    sellerinfo_span.innerText = "";
    fetchText(sellerinfo_span);
});
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
}

