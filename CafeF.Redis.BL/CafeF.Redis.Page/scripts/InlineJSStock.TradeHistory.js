var strNDTNNN="";var strKLDT="";var divData1LichSu=document.getElementById("divData1LichSu");var divData2LichSu=document.getElementById("divData2LichSu");var tab1LichSu=document.getElementById("aLSGD");var tab2LichSu=document.getElementById("aTKDL");var tab3LichSu=document.getElementById("aNDTNN");divData2LichSu.style.display="none";tab1LichSu.style.color="#cc0000";function changeTabLichSu(index)
{divData1LichSu.style.display="none";divData2LichSu.style.display="none";tab1LichSu.style.color="";tab2LichSu.style.color="";tab3LichSu.style.color="";switch(index)
{case 1:divData1LichSu.style.display="";tab1LichSu.style.color="#cc0000";break;case 2:divData2LichSu.style.display="";tab2LichSu.style.color="#cc0000";if(strKLDT=="")
LoadTKDL(sym);else
divData2LichSu.innerHTML=strKLDT;break;case 3:divData2LichSu.style.display="";tab3LichSu.style.color="#cc0000";if(strNDTNNN=="")
LoadNDTNN(sym);else
divData2LichSu.innerHTML=strNDTNNN;break;default:break;}}
function LoadNDTNN(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/NDTNN.aspx",data:"sym="+sym,success:function(msg){strNDTNNN=msg;divData2LichSu.innerHTML=msg;hideLoading();}});}
function LoadTKDL(sym)
{showLoading();$.ajax({type:"GET",url:"/Ajax/TKDL.aspx",data:"sym="+sym,success:function(msg){strKLDT=msg;divData2LichSu.innerHTML=msg;hideLoading();}});}
function showLoading(){$("#loading").show();}
function hideLoading(){$("#loading").hide();}