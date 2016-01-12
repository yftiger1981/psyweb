tPopWait=30;
showPopStep=20;
popOpacity=60;

sPop=null;
curShow=null;
tFadeOut=null;
tFadeIn=null;
tFadeWaiting=null;

document.write("<style type='text/css'id='defaultPopStyle'>");
document.write(".cPopText { font-family: Verdana, Tahoma; background-color: #DDEEFF; border: 1px #666666 solid; font-size: 11px; padding-right: 6px; padding-left: 6px; Height: 20px; padding-top: 4px; padding-bottom: 3px; filter: Alpha(Opacity=0)}");

document.write("</style>");
document.write("<div id='popLayer' style='position:absolute;z-index:1000;' class='cPopText'></div>");


function showPopupText(){
	var o=event.srcElement;
	MouseX=event.x;
	MouseY=event.y;
	if(o.alt!=null && o.alt!="") { o.pop=o.alt;o.alt="" }
        if(o.title!=null && o.title!=""){ o.pop=o.title;o.title="" }
        if(o.pop) { o.pop=o.pop.replace("\n","<br>"); o.pop=o.pop.replace("\n","<br>"); }
	if(o.pop!=sPop) {
		sPop=o.pop;
		clearTimeout(curShow);
		clearTimeout(tFadeOut);
		clearTimeout(tFadeIn);
		clearTimeout(tFadeWaiting);	
		if(sPop==null || sPop=="") {
			popLayer.innerHTML="";
			popLayer.style.filter="Alpha()";
			popLayer.filters.Alpha.opacity=0;	
		} else {
			if(o.dyclass!=null) popStyle=o.dyclass 
			else popStyle="cPopText";
			curShow=setTimeout("showIt()",tPopWait);
		}
	}
}

function showIt() {
	popLayer.className=popStyle;
	popLayer.innerHTML=sPop;
	popWidth=popLayer.clientWidth;
	popHeight=popLayer.clientHeight;
	if(MouseX+12+popWidth>document.body.clientWidth) popLeftAdjust=-popWidth-24
		else popLeftAdjust=0;
	if(MouseY+12+popHeight>document.body.clientHeight) popTopAdjust=-popHeight-24
		else popTopAdjust=0;
	popLayer.style.left=MouseX+12+document.body.scrollLeft+popLeftAdjust;
	popLayer.style.top=MouseY+12+document.body.scrollTop+popTopAdjust;
	popLayer.style.filter="Alpha(Opacity=0)";
	fadeOut();
}

function fadeOut(){
	if(popLayer.filters.Alpha.opacity<popOpacity) {
		popLayer.filters.Alpha.opacity+=showPopStep;
		tFadeOut=setTimeout("fadeOut()",1);
	}
}

function ctlent(obj) {
	if((event.ctrlKey && window.event.keyCode == 13) || (event.altKey && window.event.keyCode == 83)) {
		if ((this.document.input.postcopy) && (this.document.input.postcopy.checked == true)) 
{ 
var tempval=eval(this.document.input.message) 
tempval.focus(); 
tempval.select(); 
therange=tempval.createTextRange(); 
therange.execCommand("Copy"); 
} 
		//if(validate(this.document.input)) 
		this.document.input.submit();
	}
}

function checkall(form) {
	for(var i = 0;i < form.elements.length; i++) {
		var e = form.elements[i];
		if (e.name != 'chkall') {
			e.checked = form.chkall.checked;
		}
	}
}

function findobj(n, d) {
	var p,i,x; if(!d) d=document;
	if((p=n.indexOf("?"))>0 && parent.frames.length) {
		d=parent.frames[n.substring(p+1)].document;
		n=n.substring(0,p);
	}
	if(!(x=d[n])&&d.all) {
		x=d.all[n];
	}
	for(i=0;!x && i<d.forms.length;i++) {
		x=d.forms[i][n];
	}
	for(i=0;!x && d.layers&&i>d.layers.length;i++) {
		x=MM_findObj(n,d.layers[i].document);
	}
	return x;
}

function copycode(obj) {
	var rng = document.body.createTextRange();
	rng.moveToElementText(obj);
	rng.scrollIntoView();
	rng.select();
	rng.execCommand("Copy");
	rng.collapse(false);
}
<!-- 
var h;
var l;
var t;
var isvisible;
function HideMenu() 
{
var mX;
var mY;
var vDiv;
    if (isvisible == true)
{
        vDiv = document.all("menuDiv");
        mX = window.event.clientX + document.body.scrollLeft;//����������ƶ�����X���꣫���������λ��
        mY = window.event.clientY + document.body.scrollTop;//����������ƶ�����Y���꣫���������λ��
        if ((mX < parseInt(vDiv.style.left)) || (mX > parseInt(vDiv.style.left)+vDiv.offsetWidth) || (mY < parseInt(vDiv.style.top)-h) || (mY > parseInt(vDiv.style.top)+vDiv.offsetHeight)){//�򵥵�˵�����ǱȽ��������ֵ���Ӳ˵�����ֵ
            vDiv.style.visibility = "hidden";//ֻҪ����������һ�������Ӳ˵�������
            isvisible = false;//����һ������ֵ�����
        }
    }
}

function ShowMenu(vMnuCode) {
    vSrc = window.event.srcElement;//�����¼��Ķ�����������Ǽ���������Ƶ������϶��ҵ����˵�Ԫ��
    vMnuCode = "<DIV style='PADDING-RIGHT: 1px; FILTER: shadow(color=#CCCCCC,direction=100); PADDING-BOTTOM: 6px;width:60px'><table border=0 cellspacing=1 cellpadding=3 bgcolor=#698CC3 style='line-height:18px' width='60' align='left'><tr bgcolor='#ffffff'><td>" + vMnuCode + "</td></tr></table></div>";//�õ��Ӳ˵���HTML�ı�����������Զ����Ӳ˵������
    h = vSrc.offsetHeight + 4;//���˵�����߶�+���˵�Ԫ�ر���߶�+4������ͨ�����������ֿ������˵����Ӳ˵��ļ�࣬��ֵ����Ҫ����Ϊ��HideMenu()��Ҫ���ã������Ҫ��ֵ���Ͳ�����t������ӣ������ʹ�˵���ʾ������
    l = vSrc.offsetLeft + 0;//���˵���X���꣭10������ͨ�����������ֿ������˵����Ӳ˵�������
    t = vSrc.offsetTop + h;//���˵���Y����
    vParent = vSrc.offsetParent;//����Ԫ�ص�����ϵͳ
    while (vParent.tagName.toUpperCase() != "BODY")//��vParentԪ�صı�Ƿ��Ǵ�дBODY֮ǰѭ��,��Ϊ�ں�������innerHTML����ʱ���������ĵ�����ʱ����
    {
        l += vParent.offsetLeft;//+=���������������������͸�ֵ����������
        t += vParent.offsetTop;
        vParent = vParent.offsetParent;//���¶���Ԫ�ص�����ϵͳ
    }

    menuDiv.innerHTML = vMnuCode;//interHTML:Ԫ�ذ�����HTML�ı���������ָ����HTML�ı��滻Ԫ�ص����ݣ�������������������Ӳ˵���HTML�ı�����menuDIV����ı�Ƿ��ڡ�
    menuDiv.style.top = t;//����topֵ
    menuDiv.style.left = l;//����leftֵ
    menuDiv.style.visibility = "visible";//������ʾ
    isvisible = true;//����һ����³ֵ���Ա�HideMenu()����
    
}
//--> 
document.onmouseover=showPopupText;