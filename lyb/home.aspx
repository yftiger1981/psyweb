<%@ Page language="c#" Codebehind="home.aspx.cs" AutoEventWireup="false" Inherits="MessageBor.home" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title> ���԰� </title>
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=1, transition=23)">
		<Meta http-equiv="Page-Exit" Content="revealTrans(duration=1, transition=23)">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<Meta name="Description" Content="Ϊasp.net��ѧ�߼�����asp.net�����ĳ���Ա�ṩ������֪ʶ�����ݣ�Ϊwebserviceѧϰ���ṩѧϰ˼�룬�����ṩ���Իظ��ķ�ʽ���н�����asp.net��asp��c# ѧϰ֮�� aspnet.yjok.com">
		<Meta name="Robots" Content="index">
		<meta name="Keywords" Content="asp.netѧϰ,aspnetѧϰ,aspѧϰ���ϴ�ȫ,asp.net��ѧ��,asp.net����,asp.net����,C#ѧϰ��,C#����,��ѧ��֮·,asp.net֮��">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="Author" Content="�Ի�,zhprogrammer312@163.com">
		<Meta name="Copyright" Content="��ҳ��Ȩ�� �Ի� ���С�All Rights Reserved">
		<Meta name="Generator" Content="asp.net">
		<LINK href="message.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="����">
				<DIV id="floater" style="Z-INDEX: 1; LEFT: 0%; WIDTH: 120px; POSITION: absolute; TOP: 0px; HEIGHT: 80px; hand:"
					align="center">
					<SCRIPT language="JavaScript">
	self.onError=null;
	currentX = currentY = 0;  
	whichIt = null;   
        	lastScrollX = 0; lastScrollY = -90;
	NS = (document.layers) ? 1 : 0;
	IE = (document.all) ? 1: 0;
	<!-- STALKER CODE -->
	function heartBeat() {
		if(IE) { diffY = document.body.scrollTop; diffX = document.body.scrollLeft; }
	    if(NS) { diffY = self.pageYOffset; diffX = self.pageXOffset; }
		if(diffY != lastScrollY) {	                percent = .1 * (diffY - lastScrollY);
	                if(percent > 0) percent = Math.ceil(percent);
	                else percent = Math.floor(percent);					if(IE) document.all.floater.style.pixelTop += percent;
					if(NS) document.floater.top += percent; 	                lastScrollY = lastScrollY + percent;
	    }
		if(diffX != lastScrollX) {
			percent = .1 * (diffX - lastScrollX);
			if(percent > 0) percent = Math.ceil(percent);
			else percent = Math.floor(percent);
			if(IE) document.all.floater.style.pixelLeft += percent;
			if(NS) document.floater.left += percent;
			lastScrollX = lastScrollX + percent;
		}	
	}	<!-- /STALKER CODE -->
	<!-- DRAG DROP CODE -->
	function checkFocus(x,y) { 
	        stalkerx = document.floater.pageX;
	        stalkery = document.floater.pageY;
	        stalkerwidth = document.floater.clip.width;
	        stalkerheight = document.floater.clip.height;
	        if( (x > stalkerx && x < (stalkerx+stalkerwidth)) && (y > stalkery && y < (stalkery+stalkerheight))) return true;
	        else return false;
	}	function grabIt(e) {
		if(IE) {
			whichIt = event.srcElement;
			while (whichIt.id.indexOf("floater") == -1) {
				whichIt = whichIt.parentElement;
				if (whichIt == null) { return true; }
		    }
			whichIt.style.pixelLeft = whichIt.offsetLeft;
		    whichIt.style.pixelTop = whichIt.offsetTop;
			currentX = (event.clientX + document.body.scrollLeft);
	   		currentY = (event.clientY + document.body.scrollTop); 	
		} else { 
	        window.captureEvents(Event.MOUSEMOVE);
	        if(checkFocus (e.pageX,e.pageY)) { 
	                whichIt = document.floater;
	                StalkerTouchedX = e.pageX-document.floater.pageX;
	                StalkerTouchedY = e.pageY-document.floater.pageY;
	        } 		}
	    return true;
	}
	function moveIt(e) {
		if (whichIt == null) { return false; }
		if(IE) {
		    newX = (event.clientX + document.body.scrollLeft);
		    newY = (event.clientY + document.body.scrollTop);
		    distanceX = (newX - currentX);    distanceY = (newY - currentY);		    currentX = newX;    currentY = newY;
		    whichIt.style.pixelLeft += distanceX;		    whichIt.style.pixelTop += distanceY;
			if(whichIt.style.pixelTop < document.body.scrollTop) whichIt.style.pixelTop = document.body.scrollTop;			if(whichIt.style.pixelLeft < document.body.scrollLeft) whichIt.style.pixelLeft = document.body.scrollLeft;
			if(whichIt.style.pixelLeft > document.body.offsetWidth - document.body.scrollLeft - whichIt.style.pixelWidth - 20) whichIt.style.pixelLeft = document.body.offsetWidth - whichIt.style.pixelWidth - 20;
			if(whichIt.style.pixelTop > document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5) whichIt.style.pixelTop = document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5;
			event.returnValue = false;
		} else {
			whichIt.moveTo(e.pageX-StalkerTouchedX,e.pageY-StalkerTouchedY);
	        if(whichIt.left < 0+self.pageXOffset) whichIt.left = 0+self.pageXOffset;
	        if(whichIt.top < 0+self.pageYOffset) whichIt.top = 0+self.pageYOffset;
	        if( (whichIt.left + whichIt.clip.width) >= (window.innerWidth+self.pageXOffset-17)) whichIt.left = ((window.innerWidth+self.pageXOffset)-whichIt.clip.width)-17;
	        if( (whichIt.top + whichIt.clip.height) >= (window.innerHeight+self.pageYOffset-17)) whichIt.top = ((window.innerHeight+self.pageYOffset)-whichIt.clip.height)-17;
	        return false;
		}
	    return false;
	}
	function dropIt() {
		whichIt = null;
	    if(NS) window.releaseEvents (Event.MOUSEMOVE);
	    return true;
	}
	<!-- DRAG DROP CODE -->
	if(NS) {
		window.captureEvents(Event.MOUSEUP|Event.MOUSEDOWN);
		window.onmousedown = grabIt;
	 	window.onmousemove = moveIt;
		window.onmouseup = dropIt;
	}
	if(IE) {
		document.onmousedown = grabIt;
	 	document.onmousemove = moveIt;
		document.onmouseup = dropIt;
	}
	if(NS || IE) action = window.setInterval("heartBeat()",1);
					</SCRIPT>
					<!--  End -->
				</DIV>
				<!--=================================���԰���ҳ =============================================---->
				<!-- Keywords -->
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" height="100%"
					cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="left">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="760" border="0">
								<TR>
									<TD class="cellheard" id="TD_header" vAlign="middle" align="center" height="36" runat="server"></TD>
								</TR>
								<tr>
									<td height="60">
										<table height="22" cellSpacing="0" cellPadding="0" width="760" border="0">
											<tr>
												<td class="cellfoot" align="left" width="86"><asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="Addmsg.aspx" ImageUrl="images/write.gif"
														ToolTip="��Ҫ��������">����</asp:hyperlink></td>												
                                                     <td align="center" width="84"><asp:hyperlink id="HyperLink3" runat="server" NavigateUrl="../soulcastlefrontpage.html" 
														ToolTip="��Ҫ����ҳ" ImageUrl="~/lyb/images/gohome.gif"><p style="font-size:large">����ҳ</p></asp:hyperlink></td>
		                                            <td align="right" width="84"> <asp:hyperlink id="HyperLink2" runat="server" NavigateUrl="home.aspx" ImageUrl="images/read.gif"
														ToolTip="��Ҫ�������">�������</asp:hyperlink></td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- Keywords -->
								<!--asp.netѧϰ,aspnetѧϰ,aspѧϰ���ϴ�ȫ,asp.net��ѧ��,asp.net����,asp.net����,C#ѧϰ��,C#����,��ѧ��֮·,asp.net֮��-->
								<TR>
									<TD><asp:datagrid id="Msgbook" runat="server" PageSize="3" AllowPaging="True" AutoGenerateColumns="False"
											ShowHeader="False" Width="760px" DataKeyField="id" BorderStyle="None" BorderWidth="0px" GridLines="None">
											<ItemStyle Height="300px"></ItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="lywordbs"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="hfwordbs"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="adminbs"></asp:BoundColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<table class="Gtableone" cellpadding="0" cellspacing="0" border="0">
															<tr>
																<td class="Gcell1">
																	<table>
																		<tr>
																			<td class="FontCenter9pt">&nbsp;<br>
																				��������Ϣ<br>
																				&nbsp;<br>
																				<img alt="" border="0" height =60 src='<%#DataBinder.Eval(Container.DataItem,"img")%>'></td>
																		</tr>
																		<tr>
																			<td height="10">&nbsp;</td>
																		</tr>
																		<tr>
																			<td class="FontLeft9pt">�ǳƣ�<%#DataBinder.Eval(Container.DataItem,"username")%><br>
																				<img src="images/flower.gif">���֣�<font color="Red"><strong></strong></font><br>
																				IP��<%#DataBinder.Eval(Container.DataItem,"ip")%><br>
																				�绰��<%#DataBinder.Eval(Container.DataItem,"telephone")%><br>
																				�ֻ���<%#DataBinder.Eval(Container.DataItem,"sj")%><br>
																				<a target=blank href='<%#"http://wpa.qq.com/msgrd?V=1&Uin="+(DataBinder.Eval(Container.DataItem,"oicq")).ToString()+"&Site=http://aspnet.yjok.com/&Menu=yes"%>'>
																					<img border="0" SRC='<%#"http://wpa.qq.com/pa?p=1:"+(DataBinder.Eval(Container.DataItem,"oicq")).ToString()+":10"%>' alt="��ϵ�Ұ�"></a>
																				<br>
																				����ʱ�䣺<%#DataBinder.Eval(Container.DataItem,"lyrq","{0:d}")%><br>
																				&nbsp;<br>
																				<span id="lyrword" runat="server" style="color='#ff0000'"><a href='<%# "download.aspx?path="+(DataBinder.Eval(Container.DataItem,"lyword")).ToString()%>' title ="����Ҽ������ĵ�����">
																						����ĵ�</a></span></td>
																		</tr>
																	</table>
																</td>
																<!--asp.netѧϰ,aspnetѧϰ,aspѧϰ���ϴ�ȫ,asp.net��ѧ��,asp.net����,asp.net����,C#ѧϰ��,C#����,��ѧ��֮·,asp.net֮��-->
																<td class="Gcell3" valign="top">
																	<table class="Gtabletwo" cellpadding="0" cellspacing="0" border="0">
																		<tr>
																			<td height="220" align="center">
																				<table class="Gcell5table" cellpadding="0" cellspacing="0" border="0" height="230">
																					<tr>
																						<td class="Gcell15" height="30">&nbsp;<br>
																							�����⡽��<strong><%#DataBinder.Eval(Container.DataItem,"zt")%></strong></td>
																					</tr>
																					<tr>
																						<td height="30">&nbsp;<img src="images/profile.gif"></td>
																					</tr>
																					<!-- Keywords -->
																					<tr>
																						<td class="Gcell12" style="word-break:break-all;DIRECTION:ltr;TEXT-INDENT:10pt; LINE-HEIGHT:20pt;"><p><%#DataBinder.Eval(Container.DataItem,"nr")%></p>
																						</td>
																					</tr>
																					<tr>
																						<td id="glyhf" class="Gcell13" runat="server"><p>&nbsp;<font color="#ff0000">���»ظ���</font>(<%#DataBinder.Eval(Container.DataItem,"hfrq","{0:d}")%>)&nbsp;<span id="hfrword" runat="server" style="color='#ff0000'"><a href='<%# "download.aspx?path="+(DataBinder.Eval(Container.DataItem,"hfword")).ToString()%>' title ="����Ҽ������ĵ�����">����ĵ�</a></span></p>
																						</td>
																					</tr>
																					<tr>
																						<td id="glyhfnr" runat="server" bgcolor="Seashell" class="Gcell14" style="word-break:break-all;DIRECTION:ltr;TEXT-INDENT:10pt; LINE-HEIGHT:20pt;"><p><%#DataBinder.Eval(Container.DataItem,"hfnr")%></p>
																						</td>
																					</tr>
																				</table>
																				<!--asp.netѧϰ,aspnetѧϰ,aspѧϰ���ϴ�ȫ,asp.net��ѧ��,asp.net����,asp.net����,C#ѧϰ��,C#����,��ѧ��֮·,asp.net֮��-->
																			</td>
																		</tr>
																		<tr>
																			<td height="30">
																				<table class="Gcell6table" cellpadding="0" cellspacing="0" border="0">
																					<tr>
																						<td class="Gcell15" align="right"><p>&nbsp;<span title="�������˵���վ" style="CURSOR: hand"><a onmouseover="window.status='�������˵���վ';return true;" title="�������˵���վ" onmouseout="window.status=''" href ='<%#DataBinder.Eval(Container.DataItem,"www")%>' target =_blank><img alt="" border="0" src="images/home.gif">��վ</a></span>&nbsp;<a onmouseover="window.status='�������߷���Ϣ';return true;" title="�������߷�E_mail" onmouseout="window.status=''" href ='mailto:<%#DataBinder.Eval(Container.DataItem,"E_mail")%>'><img alt="" border="0" src="images/mail.gif">���ҷ���</a>&nbsp;<span>
																									<asp:ImageButton ID="dele" ImageUrl="images/del.gif" ToolTip="ɾ����ǰ��������" Runat="server"></asp:ImageButton><asp:LinkButton ID="lkdelete" Text="ɾ������" ToolTip="ɾ����ǰ��������" OnClick="DeleteFilesButton" Runat="server"></asp:LinkButton></span>&nbsp;<a id="adminhf" runat="server" onmouseover="window.status='�������߻ظ�';return true;" title="�������߻ظ�"
																									onmouseout="window.status=''" href="Adminhf.aspx"><img alt="" border="0" src="images/replynow.gif">�ظ�</a>&nbsp;&nbsp;</p>
																						</td>
																					</tr>
																				</table>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
								<tr>
									<td vAlign="middle" align="right" height="26">
										<table id="TABLE_page" cellSpacing="0" cellPadding="0" border="0" runat="server">
											<tbody>
												<!--asp.netѧϰ,aspnetѧϰ,aspѧϰ���ϴ�ȫ,asp.net��ѧ��,asp.net����,asp.net����,C#ѧϰ��,C#����,��ѧ��֮·,asp.net֮��-->
												<tr>
													<td class="Gcell16"></td>
													<td class="Gcell16"></td>
													<td class="Gcell16"></td>
													<td class="Gcell16" align="right" width="60"></td>
													<td class="Gcell16" id="jlcount" align="right" width="150" runat="server">����<FONT color="#0000ff">20</FONT>������</td>
													<td class="Gcell16" align="right" width="66">��ǰҳΪ:</td>
													<td class="Gcell16" id="currentpage" align="center" width="60" runat="server"><FONT color="#0000ff">1</FONT>/12ҳ</td>
													<td class="Gcell16" align="center" width="50"><asp:linkbutton id="prev" onclick="PageButtonClick" CommandName="prev" CssClass="LINKBUTTON" Text="��һҳ"
															Runat="server">[��һҳ]</asp:linkbutton></td>
													<td class="Gcell16" align="center" width="50"><asp:linkbutton id="next" onclick="PageButtonClick" CommandName="next" CssClass="LINKBUTTON" Text="��һҳ"
															Runat="server">[��һҳ]</asp:linkbutton></td>
													<td class="Gcell16" align="right" width="60">
														<p><asp:dropdownlist id="gopage" Width="66px" CssClass="DDLIST" Runat="server" AutoPostBack="True" Font-Size="9pt"
																Font-Names="����">
																<asp:ListItem Value="1">��1ҳ</asp:ListItem>
																<asp:ListItem Value="2">��2ҳ</asp:ListItem>
															</asp:dropdownlist></p>
													</td>
												</tr>
											</tbody>
										</table>
									</td>
								</tr>
								<TR>
									<TD class="cellfoot" id="TD_foot" style="FONT-WEIGHT: normal; FONT-SIZE: 10pt; FONT-FAMILY: ����"
										vAlign="middle" align="center" height="80" runat="server">
			<FONT face="����">
				                                  
			</FONT>
		                                            </TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
