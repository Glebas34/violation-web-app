// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMasks() {
    cardNumberMask = IMask($cardNumberField, {
        mask: binking.defaultResult.cardNumberMask
    })
    monthMask = IMask($monthField, {
        mask: IMask.MaskedRange,
        from: 1,
        to: 12,
        maxLength: 2,
        autofix: true
    })
    yearMask = IMask($yearField, {
        mask: '00'
    })
    codeMask = IMask($codeField, {
        mask: '0000'
    })
}