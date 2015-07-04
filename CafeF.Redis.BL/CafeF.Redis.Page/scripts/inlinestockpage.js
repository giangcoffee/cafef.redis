/*-- Same Category --*/
//function ViewPageNextPreviousSameCategory(index)
//{ var txtIdxSameCategory = document.getElementById('txtIdxSameCategory'); var currSameCategoryIdx = parseInt(txtIdxSameCategory.value) + parseInt(index); if (currSameCategoryIdx < 1) return; var iTotalPageSameCategory = TotalPage; if (currSameCategoryIdx > iTotalPageSameCategory) return; var preIndex = txtIdxSameCategory.value; var preObject = document.getElementById("aSameCategory" + preIndex); var currObject = document.getElementById("aSameCategory" + currSameCategoryIdx); preObject.className = ""; currObject.className = "current"; txtIdxSameCategory.value = currSameCategoryIdx; LoadSameCategory(symbol, currSameCategoryIdx, 10); }
//function ViewPageSameCategoryByIndex(index) {
//    //    var txtIdxSameCategory = document.getElementById('txtIdxSameCategory'); var preIndex = txtIdxSameCategory.value; var preObject = document.getElementById("aSameCategory" + preIndex); var currObject = document.getElementById("aSameCategory" + index); preObject.className = ""; currObject.className = "current"; txtIdxSameCategory.value = index; LoadSameCategory(symbol, index, 10);
//   // alert(index);
//}
//function LoadSameCategory(symbol, index, size) {
//    document.getElementById("pagingnote").innerHTML = "Trang " + index + "/" + TotalPage;
//    $.ajax({ type: "GET", url: "/Ajax/CungNganh/SameCategory.aspx", data: "symbol=" + symbol + "&PageIndex=" + index + "&PageSize=" + size, success: function(msg) { document.getElementById("tblCTCN").innerHTML = msg; } });
//}
$(document).ready(function() {
    $('.congtycungnganh .paging a').click(function(e) {
        e.preventDefault();
        $('.congtycungnganh .paging a').removeClass('current');
        var index = $(this).attr('rel');
        if (index == 'prev') index = parseInt($('#txtIdxSameCategory').val()) - 1;
        else if (index == 'next') index = parseInt($('#txtIdxSameCategory').val()) + 1;
        if (index <= 0) index = 1;
        if (index > TotalPage) index = TotalPage;
        $('.congtycungnganh .paging a[rel="'+index+'"]').addClass('current');
        $('#txtIdxSameCategory').val(index);
        $("#pagingnote").html("Trang " + index + "/" + TotalPage);
        $.ajax({ type: "GET", url: "/Ajax/CungNganh/SameCategory.aspx", data: "symbol=" + symbol + "&PageIndex=" +index + "&PageSize=" + 10, success: function(msg) { $("#tblCTCN").html(msg); } });
    });
});
/*-- CompanyInfo --*/
/*
var strThongTinChung = ""; var strBanLanhDaoVaSoHuu = ""; var strCongTyCon = ""; var strBaoCaoTaiChinh = ""; var divStart = document.getElementById("divStart"); var divAjax = document.getElementById("divAjax"); var tab1CT = document.getElementById("lsTab1CT"); var tab2CT = document.getElementById("lsTab2CT"); var tab3CT = document.getElementById("lsTab3CT"); var tab4CT = document.getElementById("lsTab4CT"); var liTabCongTy1CT = document.getElementById("liTabCongTy1CT"); var liTabCongTy2CT = document.getElementById("liTabCongTy2CT"); var liTabCongTy3CT = document.getElementById("liTabCongTy3CT"); var liTabCongTy4CT = document.getElementById("liTabCongTy4CT"); var sym = '<%= Symbol %>'; divAjax.style.display = "none"; tab1CT.style.color = "#cc0000"; */
function changeTabCongTy(index) {
    document.getElementById('divStart2').style.display = 'none';
    divStart.style.display = "none"; divAjax.style.display = "none"; tab1CT.style.color = ""; tab2CT.style.color = ""; tab3CT.style.color = ""; tab4CT.style.color = ""; switch (index) {
        case 1: divStart.style.display = ""; tab1CT.style.color = "#cc0000"; liTabCongTy1CT.className = "active"; liTabCongTy2CT.className = ""; liTabCongTy3CT.className = ""; liTabCongTy4CT.className = ""; break; case 2: divAjax.style.display = ""; tab2CT.style.color = "#cc0000"; liTabCongTy1CT.className = ""; liTabCongTy2CT.className = "active"; liTabCongTy3CT.className = ""; liTabCongTy4CT.className = ""; if (strThongTinChung == "")
                LoadThongTinChung(sym); else
                divAjax.innerHTML = strThongTinChung; break; case 3: divAjax.style.display = ""; tab3CT.style.color = "#cc0000"; liTabCongTy1CT.className = ""; liTabCongTy2CT.className = ""; liTabCongTy3CT.className = "active"; liTabCongTy4CT.className = ""; if (strBanLanhDaoVaSoHuu == "")
                LoadBanLanhDaoVaSoHuu(sym); else
                divAjax.innerHTML = strBanLanhDaoVaSoHuu; break; case 4: divAjax.style.display = ""; tab4CT.style.color = "#cc0000"; liTabCongTy1CT.className = ""; liTabCongTy2CT.className = ""; liTabCongTy3CT.className = ""; liTabCongTy4CT.className = "active"; if (strCongTyCon == "")
                LoadCongTyCon(sym); else
                divAjax.innerHTML = strCongTyCon; break; case 5: divAjax.style.display = ""; if (strBaoCaoTaiChinh == "")
                LoadBaoCaoTaiChinh(sym); else
                divAjax.innerHTML = strBaoCaoTaiChinh; break; default: break;
    }
}
function LoadThongTinChung(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/CongTy/ThongTinChung.aspx", data: "sym=" + sym, success: function(msg) { strThongTinChung = msg; divAjax.innerHTML = msg; hideLoading(); } }); }
function LoadBanLanhDaoVaSoHuu(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/CongTy/BanLanhDao.aspx", data: "sym=" + sym, success: function(msg) { strBanLanhDaoVaSoHuu = msg; divAjax.innerHTML = msg; hideLoading(); } }); }
function LoadCongTyCon(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/CongTy/CongTyCon.aspx", data: "sym=" + sym, success: function(msg) { strCongTyCon = msg; divAjax.innerHTML = msg; hideLoading(); } }); }
function LoadBaoCaoTaiChinh(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/CongTy/BaoCaoTaiChinh.aspx", data: "sym=" + sym, success: function(msg) { strBaoCaoTaiChinh = msg; divAjax.innerHTML = msg; hideLoading(); } }); }
function changeTabBanLanhDao(index) {
    switch (index)
    { case 1: document.getElementById("divBanLanhDao").className = "BanLanhDaoCoCuSoHuu_Sel"; document.getElementById("divCoDongLon").className = "BanLanhDaoCoCuSoHuu_UnSel"; document.getElementById("divViewBanLanhDao").style.display = ""; document.getElementById("divViewCoDongLon").style.display = "none"; break; case 2: document.getElementById("divBanLanhDao").className = "BanLanhDaoCoCuSoHuu_UnSel"; document.getElementById("divCoDongLon").className = "BanLanhDaoCoCuSoHuu_Sel"; document.getElementById("divViewBanLanhDao").style.display = "none"; document.getElementById("divViewCoDongLon").style.display = ""; break; default: break; }
}
//function showLoading() { $("#loading").show(); }
//function hideLoading() { $("#loading").hide(); }
/*-- SameEPS/PE --*/
/*
var strPE = ""; var strPEPaging = ""; var pagingPE = document.getElementById("pagingPE"); var divData1EPS = document.getElementById("divData1EPS"); var divData2EPS = document.getElementById("divData2EPS"); var tab1EPS = document.getElementById("lstab1EPS"); var tab2EPS = document.getElementById("lstab2EPS"); divData2EPS.style.display = "none"; document.getElementById('txtIdxSameEPS').value = 1; document.getElementById('txtIdxSamePE').value = 1; */
function changeTabEPS(index) {
    divData1EPS.style.display = "none"; divData2EPS.style.display = "none"; tab1EPS.style.color = ""; tab2EPS.style.color = ""; switch (index) {
        case 1: divData1EPS.style.display = ""; tab1EPS.style.color = "#cc0000"; LoadSamePE(symbol, 1, 10); break; case 2: divData2EPS.style.display = ""; tab2EPS.style.color = "#cc0000"; if (strPEPaging == "")
                LoadPaging(sym); else
                pagingPE.innerHTML = strPEPaging; break; default: break;
    }
}
function LoadSameEPS(symbol, index, size)
{ document.getElementById("pagingnoteeps").innerHTML = "Trang " + index + "/" + TotalPagePE + "(Tổng số " + TotalItemPE + " công ty)"; $.ajax({ type: "GET", url: "/Ajax/CungNganh/SameEPS.aspx", data: "symbol=" + symbol + "&PageIndex=" + index + "&PageSize=" + size, success: function(msg) { document.getElementById("divData1EPSInner").innerHTML = msg; } }); }
/*var symbol = '<%= Symbol %>'; */
function ViewPageNextPreviousEPS(index)
{ var txtIdxSameEPS = document.getElementById('txtIdxSameEPS'); var currSameEPSIdx = parseInt(txtIdxSameEPS.value) + parseInt(index); if (currSameEPSIdx < 1) return; var iTotalPageSameEPS = TotalPagePE; if (currSameEPSIdx > iTotalPageSameEPS) return; var preIndex = txtIdxSameEPS.value; var preObject = document.getElementById("aSameEPS" + preIndex); var currObject = document.getElementById("aSameEPS" + currSameEPSIdx); preObject.className = ""; currObject.className = "current"; txtIdxSameEPS.value = currSameEPSIdx; LoadSameEPS(symbol, currSameEPSIdx, 10); }
function ViewPageSameEPSByIndex(index) { var txtIdxSameEPS = document.getElementById('txtIdxSameEPS'); var preIndex = txtIdxSameEPS.value; var preObject = document.getElementById("aSameEPS" + preIndex); var currObject = document.getElementById("aSameEPS" + index); preObject.className = ""; currObject.className = "current"; txtIdxSameEPS.value = index; LoadSameEPS(symbol, index, 10); }
function LoadSamePE(symbol, index, size)
{ $.ajax({ type: "GET", url: "/Ajax/CungNganh/SamePE.aspx", data: "symbol=" + symbol + "&PageIndex=" + index + "&PageSize=" + size, success: function(msg) { document.getElementById("divData2EPSInner").innerHTML = msg; } }); }
function LoadPaging(symbol)
{ $.ajax({ type: "GET", url: "/Ajax/CungNganh/GenPagingSamePE.aspx", data: "symbol=" + symbol, success: function(msg) { document.getElementById("pagingPE").innerHTML = msg; } }); }
function ViewPageNextPreviousPE(index)
{ var txtIdxSamePE = document.getElementById('txtIdxSamePE'); var currSamePEIdx = parseInt(txtIdxSamePE.value) + parseInt(index); if (currSamePEIdx < 1) return; var iTotalPageSamePE = document.getElementById('txtTotalPagePE').value; if (currSamePEIdx > iTotalPageSamePE) return; var preIndex = txtIdxSamePE.value; var preObject = document.getElementById("aSamePE" + preIndex); var currObject = document.getElementById("aSamePE" + currSamePEIdx); preObject.className = ""; currObject.className = "current"; txtIdxSamePE.value = currSamePEIdx; LoadSamePE(symbol, currSamePEIdx, 10); document.getElementById('pagingnoteepspe').innerHTML = "Trang " + index + "/" + document.getElementById('txtTotalPagePE').value + "(Tổng số " + document.getElementById('txtTotalItemPE').value + " công ty)"; }
function ViewPageSamePEByIndex(index) { var txtIdxSamePE = document.getElementById('txtIdxSamePE'); var preIndex = txtIdxSamePE.value; var preObject = document.getElementById("aSamePE" + preIndex); var currObject = document.getElementById("aSamePE" + index); preObject.className = ""; currObject.className = "current"; txtIdxSamePE.value = index; LoadSamePE(symbol, index, 10); document.getElementById('pagingnoteepspe').innerHTML = "Trang " + index + "/" + document.getElementById('txtTotalPagePE').value + "(Tổng số " + document.getElementById('txtTotalItemPE').value + " công ty)"; }
/*-- TradeHistory --*/
/*var strNDTNNN = ""; var strKLDT = ""; var divData1LichSu = document.getElementById("divData1LichSu"); var divData2LichSu = document.getElementById("divData2LichSu"); var tab1LichSu = document.getElementById("aLSGD"); var tab2LichSu = document.getElementById("aTKDL"); var tab3LichSu = document.getElementById("aNDTNN"); divData2LichSu.style.display = "none"; tab1LichSu.style.color = "#cc0000"; */
function changeTabLichSu(index) {
    divData1LichSu.style.display = "none"; divData2LichSu.style.display = "none"; tab1LichSu.style.color = ""; tab2LichSu.style.color = ""; tab3LichSu.style.color = ""; switch (index) {
        case 1: divData1LichSu.style.display = ""; tab1LichSu.style.color = "#cc0000"; break; case 2: divData2LichSu.style.display = ""; tab2LichSu.style.color = "#cc0000"; if (strKLDT == "")
                LoadTKDL(sym); else
                divData2LichSu.innerHTML = strKLDT; break; case 3: divData2LichSu.style.display = ""; tab3LichSu.style.color = "#cc0000"; if (strNDTNNN == "")
                LoadNDTNN(sym); else
                divData2LichSu.innerHTML = strNDTNNN; break; default: break;
    }
}
function LoadNDTNN(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/NDTNN.aspx", data: "sym=" + sym, success: function(msg) { strNDTNNN = msg; divData2LichSu.innerHTML = msg; hideLoading(); } }); }
function LoadTKDL(sym)
{ showLoading(); $.ajax({ type: "GET", url: "/Ajax/TKDL.aspx", data: "sym=" + sym, success: function(msg) { strKLDT = msg; divData2LichSu.innerHTML = msg; hideLoading(); } }); }
function showLoading() { $("#loading").show(); }
function hideLoading() { $("#loading").hide(); }