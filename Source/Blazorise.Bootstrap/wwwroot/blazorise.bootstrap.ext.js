window.blazoriseBootstrap.activateDatePicker = function (elementId, formatSubmit) {
    const element = $('#' + elementId);
    element.datepicker({
        uiLibrary: 'bootstrap4',
        iconsLibrary: 'fontawesome',
        format: 'mm-dd-yyyy',
        showOnFocus: true,
        showRightIcon: true,
        change: function (e, type) {
            // trigger input event on the DateEdit component
            // select didn't have the value yet in IE
            mutateDOMChange(elementId);
        }
    });
    return true;
}

function mutateDOMChange(id) {
    el = document.getElementById(id);
    ev = document.createEvent('Event');
    //If I triggered change, it would go infinite (by calling datepicker change event) and generate stackoverflow
    ev.initEvent('input', true, false);
    el.dispatchEvent(ev);
}