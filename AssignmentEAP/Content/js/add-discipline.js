jQuery(document).ready(function () {
    jQuery('.add-discipline').click(function () {
        jQuery.ajax({
            method : 'GET',
            url: '/Discipline',
            async: false
        }).done(function (data) {
            var html = '';
            jQuery.each(data, function (index, value) {
                var x = `<option value=${value.id}>${value.name}</option>`;
                jQuery('.select-discipline').append(x);
            })
         
            jQuery('#Modal-Add-Discipline').modal();
        })
    })
})