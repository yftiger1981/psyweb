<%@ Page language="c#" Codebehind="Login_M.aspx.cs" AutoEventWireup="false" Inherits="MessageBor.Login_M" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���԰�-����Ա��¼</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="message.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="window.document.Form1.TB_username.focus()">
		<form id="Form1" method="post" runat="server">
			<FONT face="����">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
					cellPadding="0" width="100%" border="0" height="100%">
					<TR>
						<TD vAlign="top" align="center">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="666" border="0">
								<TR>
									<TD class="cellheard" id="TD_header" vAlign="middle" align="center" height="36" runat="server"></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="center" height="200">&nbsp;
										<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
											<TR>
												<TD style="FONT-WEIGHT: normal; FONT-SIZE: 9pt; FONT-FAMILY: ����" vAlign="middle" align="right"
													width="100" height="30">����Ա�û�����</TD>
												<TD vAlign="middle" align="left" width="200" height="30">
													<asp:TextBox id="TB_username" runat="server" MaxLength="30" ToolTip="���������Ա�û���" Width="155px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD style="FONT-WEIGHT: normal; FONT-SIZE: 9pt; FONT-FAMILY: ����" vAlign="middle" align="right"
													width="100" height="30">���룺</TD>
												<TD vAlign="middle" align="left" width="200" height="30">
													<asp:TextBox id="TB_password" runat="server" MaxLength="30" ToolTip="���������Ա����" TextMode="Password"
														Width="155px"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD width="100" height="42">
			<FONT face="����">
				                                    <asp:Button ID="ModifyButton" runat="server" Height="23px" OnClick="ModifyButton_Click" style="margin-left: 34px" Text="�޸�����" />
			</FONT>
		                                        </TD>
												<TD align="center" width="200" height="42">
													<P>
														<asp:Button id="BT_login" runat="server" Text="��¼" CssClass="Button" ToolTip="����Ա�û�Ȩ����֤"></asp:Button>&nbsp;
														<asp:Button id="BT_ease" runat="server" Text="ע��" CssClass="Button" ToolTip="ע���˳�ϵͳ" EnableViewState="true"
															CausesValidation="true"></asp:Button></P>
												</TD>
											</TR>
										</TABLE>
										&nbsp;
									</TD>
								</TR>
								<TR>
									<TD id="TD_foot" vAlign="middle" align="center" height="30" runat="server" class="cellfoot"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
