﻿function ViewPageNextPreviousSameCategory(index)
{var txtIdxSameCategory=document.getElementById('txtIdxSameCategory');var currSameCategoryIdx=parseInt(txtIdxSameCategory.value)+parseInt(index);if(currSameCategoryIdx<1)return;var iTotalPageSameCategory=TotalPage;if(currSameCategoryIdx>iTotalPageSameCategory)return;var preIndex=txtIdxSameCategory.value;var preObject=document.getElementById("aSameCategory"+preIndex);var currObject=document.getElementById("aSameCategory"+currSameCategoryIdx);preObject.className="";currObject.className="current";txtIdxSameCategory.value=currSameCategoryIdx;LoadSameCategory(symbol,currSameCategoryIdx,10);}
function ViewPageSameCategoryByIndex(index){var txtIdxSameCategory=document.getElementById('txtIdxSameCategory');var preIndex=txtIdxSameCategory.value;var preObject=document.getElementById("aSameCategory"+preIndex);var currObject=document.getElementById("aSameCategory"+index);preObject.className="";currObject.className="current";txtIdxSameCategory.value=index;LoadSameCategory(symbol,index,10);}
function LoadSameCategory(symbol,index,size)
{document.getElementById("pagingnote").innerHTML="Trang "+index+"/"+TotalPage;$.ajax({type:"GET",url:"/Ajax/CungNganh/SameCategory.aspx",data:"symbol="+_symbol+"&PageIndex="+index+"&PageSize="+size,success:function(msg){document.getElementById("tblCTCN").innerHTML=msg;}});}