/*khoi tao thanh theo doi chung khoan*/
try {
    var container = document.getElementById('macktheodoi');
    if (container) {
        var danh_sach_ma_chung_khoan_theo_doi = new CafeF_StockSymbolSlide('danh_sach_ma_chung_khoan_theo_doi');
        danh_sach_ma_chung_khoan_theo_doi.InitScript('macktheodoi');
        if (container.delay) {
            setTimeout('danh_sach_ma_chung_khoan_theo_doi.LoadSymbolData(true)', 30000);
            //        setTimeout('danh_sach_ma_chung_khoan_theo_doi.LoadSymbolData(true)', parseInt(container.delay));
        }
        else {
            danh_sach_ma_chung_khoan_theo_doi.LoadSymbolData(true);
        }
    }
    var CafeF_StockSymbolSlide_IsWindowFocus = true;
    window.onfocus = function() {
        CafeF_StockSymbolSlide_IsWindowFocus = true;
    }
    window.onblur = function() {
        CafeF_StockSymbolSlide_IsWindowFocus = false;
    }
} catch (e) { }
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
 function loopdate() {
     GetDate();
     setTimeout('loopdate()', 15000);
 }
 setTimeout('loopdate()', 15000);
