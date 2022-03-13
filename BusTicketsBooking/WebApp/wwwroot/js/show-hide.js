$(function () {
    $('#toggle_btn').click(function () {
        var icon = $('#toggle_eye');
        icon.toggleClass('fa-eye-slash fa-eye');
        var attrType = icon.hasClass('fa-eye') ? 'text' : 'password';
        $('#toggle_fld').attr('type', attrType);
    })
});