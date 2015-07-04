var strThongTinChung="";var strBanLanhDaoVaSoHuu="";var strCongTyCon="";var strBaoCaoTaiChinh="";var divStart=document.getElementById("divStart");var divAjax=document.getElementById("divAjax");var tab1CT=document.getElementById("lsTab1CT");var tab2CT=document.getElementById("lsTab2CT");var tab3CT=document.getElementById("lsTab3CT");var tab4CT=document.getElementById("lsTab4CT");var liTabCongTy1CT=document.getElementById("liTabCongTy1CT");var liTabCongTy2CT=document.getElementById("liTabCongTy2CT");var liTabCongTy3CT=document.getElementById("liTabCongTy3CT");var liTabCongTy4CT=document.getElementById("liTabCongTy4CT");var sym='<%= Symbol %>';divAjax.style.display="none";tab1CT.style.color="#cc0000";function changeTabCongTy(index)
{divStart.style.display="none";divAjax.style.display="none";tab1CT.style.color="";tab2CT.style.color="";tab3CT.style.color="";tab4CT.style.color="";switch(index)
{case 1:divStart.style.display="";tab1CT.style.color="#cc0000";liTabCongTy1CT.className="active";liTabCongTy2CT.className="";liTabCongTy3CT.className="";liTabCongTy4CT.className="";break;case 2:divAjax.style.display="";tab2CT.style.color="#cc0000";liTabCongTy1CT.className="";liTabCongTy2CT.className="active";liTabCongTy3CT.className="";liTabCongTy4CT.className="";if(strThongTinChung=="")
LoadThongTinChung(sym);else
divAjax.innerHTML=strThongTinChung;break;case 3:divAjax.style.display="";tab3CT.style.color="#cc0000";liTabCongTy1CT.className="";liTabCongTy2CT.className="";liTabCongTy3CT.className="active";liTabCongTy4CT.className="";if(strBanLanhDaoVaSoHuu=="")
LoadBanLanhDaoVaSoHuu(sym);else
divAjax.innerHTML=strBanLanhDaoVaSoHuu;break;case 4:divAjax.style.display="";tab4CT.style.color="#cc0000";liTabCongTy1CT.className="";liTabCongTy2CT.className="";liTabCongTy3CT.className="";liTabCongTy4CT.className="active";if(strCongTyCon=="")
LoadCongTyCon(sym);else
divAjax.innerHTML=strCongTyCon;break;case 5:divAjax.style.display="";if(strBaoCaoTaiChinh=="")
LoadBaoCaoTaiChinh(sym);else
divAjax.innerHTML=strBaoCaoTaiChinh;break;default:break;}}
function LoadThongTinChung(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/CongTy/ThongTinChung.aspx",data:"sym="+sym,success:function(msg){strThongTinChung=msg;divAjax.innerHTML=msg;hideLoading();}});}
function LoadBanLanhDaoVaSoHuu(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/CongTy/BanLanhDao.aspx",data:"sym="+sym,success:function(msg){strBanLanhDaoVaSoHuu=msg;divAjax.innerHTML=msg;hideLoading();}});}
function LoadCongTyCon(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/CongTy/CongTyCon.aspx",data:"sym="+sym,success:function(msg){strCongTyCon=msg;divAjax.innerHTML=msg;hideLoading();}});}
function LoadBaoCaoTaiChinh(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/CongTy/BaoCaoTaiChinh.aspx",data:"sym="+sym,success:function(msg){strBaoCaoTaiChinh=msg;divAjax.innerHTML=msg;hideLoading();}});}
function changeTabBanLanhDao(index)
{switch(index)
{case 1:document.getElementById("divBanLanhDao").className="BanLanhDaoCoCuSoHuu_Sel";document.getElementById("divCoDongLon").className="BanLanhDaoCoCuSoHuu_UnSel";document.getElementById("divViewBanLanhDao").style.display="";document.getElementById("divViewCoDongLon").style.display="none";break;case 2:document.getElementById("divBanLanhDao").className="BanLanhDaoCoCuSoHuu_UnSel";document.getElementById("divCoDongLon").className="BanLanhDaoCoCuSoHuu_Sel";document.getElementById("divViewBanLanhDao").style.display="none";document.getElementById("divViewCoDongLon").style.display="";break;default:break;}}
function showLoading(){$("#loading").show();}
function hideLoading(){$("#loading").hide();}