+ACQ-(document).ready(function()+AHs-
    
    (function(+ACQ-) +AHs-
        +ACI-use strict+ACIAOw-

    
    jQuery.validator.addMethod('answercheck', function (value, element) +AHs-
        return this.optional(element) +AHwAfA- /+AF4AXA-bcat+AFw-b+ACQ-/.test(value)
    +AH0-, +ACI-type the correct answer -+AF8--+ACI-)+ADs-

    // validate contactForm form
    +ACQ-(function() +AHs-
        +ACQ-('+ACM-contactForm').validate(+AHs-
            rules: +AHs-
                name: +AHs-
                    required: true,
                    minlength: 2
                +AH0-,
                phone: +AHs-
                    required: true,
                    minlength: 11
                +AH0-,
                //number: +AHs-
                //    required: true,
                //    minlength: 5
                //+AH0-,
                email: +AHs-
                    required: true,
                    email: true
                +AH0-,
                message: +AHs-
                    required: true,
                    minlength: 5000
                +AH0-
            +AH0-,
            messages: +AHs-
                name: +AHs-
                    required: +ACI-Vui l+API-ng nh+Hq0-p t+AOo-n+ACI-,
                    minlength: +ACI-T+AOo-n ch+Huk-a +AO0-t nh+HqU-t 2 k+AP0- t+HvEAIg-
                +AH0-,
                phone: +AHs-
                    required: +ACI-Vui l+API-ng nh+Hq0-p s+HtE- +ARE-i+Hsc-n tho+HqE-i+ACI-,
                    minlength: +ACIAIg-
                +AH0-,
                //number: +AHs-
                //    required: +ACI-come on, you have a number, don't you?+ACI-,
                //    minlength: +ACI-your Number must consist of at least 5 characters+ACI-
                //+AH0-,
                email: +AHs-
                    required: +ACI-Vui l+API-ng nh+Hq0-p email+ACI-
                +AH0-,
                message: +AHs-
                    required: +ACI-Chia s+Hrs- +AP0- ki+Hr8-n v+AOA-o +APQ- tr+HtE-ng n+AOA-y+ACI-,
                    minlength: +ACI-chia s+Hrs- th+AOo-m +ARE-i +AF4AXgAi-
                +AH0-
            +AH0-,
            submitHandler: function(form) +AHs-
                +ACQ-(form).ajaxSubmit(+AHs-
                    type:+ACI-POST+ACI-,
                    data: +ACQ-(form).serialize(),
                    url:+ACI-contact+AF8-process.php+ACI-,
                    success: function() +AHs-
                        +ACQ-('+ACM-contactForm :input').attr('disabled', 'disabled')+ADs-
                        +ACQ-('+ACM-contactForm').fadeTo( +ACI-slow+ACI-, 1, function() +AHs-
                            +ACQ-(this).find(':input').attr('disabled', 'disabled')+ADs-
                            +ACQ-(this).find('label').css('cursor','default')+ADs-
                            +ACQ-('+ACM-success').fadeIn()
                            +ACQ-('.modal').modal('hide')+ADs-
		                	+ACQ-('+ACM-success').modal('show')+ADs-
                        +AH0-)
                    +AH0-,
                    error: function() +AHs-
                        +ACQ-('+ACM-contactForm').fadeTo( +ACI-slow+ACI-, 1, function() +AHs-
                            +ACQ-('+ACM-error').fadeIn()
                            +ACQ-('.modal').modal('hide')+ADs-
		                	+ACQ-('+ACM-error').modal('show')+ADs-
                        +AH0-)
                    +AH0-
                +AH0-)
            +AH0-
        +AH0-)
    +AH0-)
        
 +AH0-)(jQuery)
+AH0-)