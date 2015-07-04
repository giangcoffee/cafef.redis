$(document).ready(function() {
    /*Box search */
    var container = document.getElementById('CafeF_BoxSearch');
    var cafef_box_search;
    if (container) {
        cafef_box_search = new CafeF_BoxSearch('cafef_box_search');
        if (container.delay) {
            setTimeout('cafef_box_search.InitScript()', parseInt(container.delay));
        }
        else {
            cafef_box_search.InitScript();
        }
        if (!document.all) {
            setTimeout('cafef_box_search.InitAutoComplete()', 1000);
        }
    }
});