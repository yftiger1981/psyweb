using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb ;

namespace MessageBor
{
	/// <summary>
	/// Login ��ժҪ˵����
	/// </summary>
	public class Login_M : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell TD_header;
		protected System.Web.UI.WebControls.TextBox TB_username;
		protected System.Web.UI.WebControls.TextBox TB_password;
		protected System.Web.UI.WebControls.Button BT_login;
		protected System.Web.UI.WebControls.Button BT_ease;
		protected System.Web.UI.HtmlControls.HtmlTableCell TD_foot;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
				clsMain.WriterHFinfo(TD_header,TD_foot);
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.BT_login.Click += new System.EventHandler(this.BT_login_Click);
			this.BT_ease.Click += new System.EventHandler(this.BT_ease_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void WriterHFinfo()
		{
			TD_header.InnerText ="ASP.NETѧϰ|���԰�_IF_V3.2";
			TD_foot.InnerHtml ="<HR width='100%' SIZE='1'><br>��Ȩ����Copyright (c)2005-01 -- �Ի� ASP.NET �������з�<BR><span title='�ҵ�QQ����254212580' style='CURSOR: hand'><IMG alt='' src='images/oicq.gif'>254212580</span>&nbsp;<IMG title='��ӭ��ϵ����' height='16' src='images/emailtofriend.gif' width='16' border='0'><A onmouseover="+"\"window.status='��ϵ���� Asp.Net ֪ʶ��ȫ��վ';return true;"+"\" title='��ӭ��ϵ����' onmouseout="+"\"window.status=''"+"\"href='mailto:zhprogrammer312@163.com'><SPAN style='CURSOR: hand'><FONT style='FONT-SIZE: 9pt; FONT-FAMILY: ����' color='#0000ff'>������ϵ</FONT></SPAN></A>";
		}

		private void BT_ease_Click(object sender, System.EventArgs e)
		{
			Response.Write ("<script>document.location.href='home.aspx';</script>");
		}

		private void BT_login_Click(object sender, System.EventArgs e)
		{
			if(ISNULL().CompareTo ("YES")==0)
				AdminLogin();
			else
				Response.Write ("<script Language='JavaScript'>alert('����д������Ϣ����')</script>");
		}
		private string ISNULL()
		{
			if(TB_username.Text .Trim ().Length ==0 || TB_password.Text .Trim ().Length ==0)
				return "NO";
			else
				return "YES";
		}
		private void AdminLogin()
		{
			//��֤/��Session["admin"]="admin"/������Чʱ��
			DataView _Cudv = null;
			string _CurrentPage=string.Empty ;
			OleDbConnection _Conn = clsMain.__GetConn ();
			try
			{
                _Cudv = clsMain.__GetDV(_Conn, "SELECT id,UserName,PassWord,adminbs FROM Msg_users Where UserName='" + TB_username.Text.Trim() + "'", "login");
				_Cudv.Sort ="UserName";
				if(_Cudv.Find (TB_username.Text .Trim ())>=0)
				{
					//������
					//��������ܡ���
					_Cudv.Sort ="PassWord";
					if(_Cudv.Find (TB_password.Text .Trim ())>=0)
					{
						Session["admin"]="admin";
						Session.Timeout =2;
						_CurrentPage = Session["CurrentPage"].ToString ().Trim ();
						Session["username"]=TB_username.Text .Trim ();


						if(_CurrentPage.CompareTo ("ModifyPass.aspx")==0)
							Response.Redirect ("ModifyPass.aspx");
						else
							Response.Redirect ("home.aspx");
					}
					else
					{
						Response.Write ("<script Language='JavaScript'>alert('������󡭡�')</script>");
						return;
					}
				}
				else
				{
					Response.Write ("<script Language='JavaScript'>alert('�޴��û�������')</script>");
					return;
				}
			}
			catch(OleDbException _ex)
			{
				Session["errors"]=Session["errors"].ToString()+_ex.Message.ToString()+"<br>";
			}
			finally
			{
				if(_Conn!=null) _Conn.Dispose ();
				if(_Cudv!=null) _Cudv.Dispose ();
			}			
		}
        //private OleDbConnection __GetConnObject()
        //{

        //    string __ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(@"..\databases\#Message DB.mdb");
        //    string __ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"D:\wwwroot\zhaohui0312\databases\#Message DB.mdb";
        //    OleDbConnection __OleConn = new OleDbConnection(__ConnStr);
        //    return __OleConn;
        //}
        //private DataView __GetDV(OleDbConnection __Conn,string __SqlStr,string __TableName)
        //{
        //    OleDbDataAdapter	__Pter=null;
        //    DataSet				__DS=	new DataSet();
        //    DataView			__DV=null;
        //    try
        //    {
        //        __Pter=new OleDbDataAdapter(__SqlStr,__Conn);
        //        __Pter.Fill(__DS,__TableName);
        //        __DV=__DS.Tables[__TableName].DefaultView;

        //    }
        //    catch(OleDbException Ex)
        //    {
        //        Session["errors"]=Session["errors"].ToString()+Ex.Message.ToString()+"<br>";
        //    }
        //    finally
        //    {
        //        if(__Pter!=null)		__Pter.Dispose();
        //        if(__DS!=null)			__DS.Dispose();
        //        if(__Conn!=null)		__Conn.Dispose();
        //    }
        //    return __DV;
        //}

        protected void BT_ease_Click1(object sender, EventArgs e)
        {

        }

        protected void BT_login_Click1(object sender, EventArgs e)
        {

        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModifyPass.aspx");
        }
	}
}
