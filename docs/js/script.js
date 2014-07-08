var $ = function(id){
    return document.getElementById(id);
}
var c = function(cl,i){
    return document.getElementsByClassName(cl)[i];
}
function toHtml(md){
    marked.setOptions({
        highlight:function(code){
            return hljs.highlightAuto(code).value;
        }
    });
    return marked(md);
}
function loadDoc(f)
{
    var xmlhttp;
    if (window.XMLHttpRequest)
    {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp=new XMLHttpRequest();
    }
    else
    {// code for IE6, IE5
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
            c('inner',1).innerHTML=toHtml(xmlhttp.responseText);
        }
    }
    xmlhttp.open("GET","res/"+f,true);
    xmlhttp.send();
}