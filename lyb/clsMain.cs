using System;
using System.Data;
using System.Data.OleDb;
using System.Web.SessionState;

namespace MessageBor
{
	/// <summary>
	/// clsMain ��ժҪ˵����
	/// </summary>
	public class clsMain
	{
		public clsMain()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static void WriterHFinfo( System.Web.UI.HtmlControls.HtmlTableCell TD_header,System.Web.UI.HtmlControls.HtmlTableCell TD_foot) 
		{
			TD_header.InnerHtml ="<span><table><tr><td  vAlign='middle' align='center'><img src='images/publish.gif'></td><td vAlign='middle' align='center'>���԰�</td></tr></table></span>";
			TD_foot.InnerHtml ="<HR width='100%' SIZE='1'><br>��Ȩ����Copyright (c)����֮��<BR><span title='�ҵ�QQ����671220162' style='CURSOR: hand'><a target=blank href=http://wpa.qq.com/msgrd?V=1&Uin=671220162&Site=http://3128.it50.net&Menu=yes><img border='0' SRC=http://wpa.qq.com/pa?p=1:671220162:8 alt='���������ҷ���Ϣ'></a></span>&nbsp;<IMG title='��ӭ��ϵ��' height='16' src='images/emailtofriend.gif' width='16' border='0'><A onmouseover="+"\"window.status='��ϵ���� Asp.Net ֪ʶ��ȫ��վ';return true;"+"\" title='��ӭ��ϵ����' onmouseout="+"\"window.status=''"+"\"href='mailto:eye18soft@163.com'><SPAN style='CURSOR: hand'><FONT style='color='#0000ff'>������ϵ</FONT></SPAN></A>";
		}

		public static  OleDbConnection __GetConn()
		{

            string __ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath(@".\App_Data\#Message DB.mdb");			
            //string __ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"D:\wwwroot\zhaohui0312\databases\#Message DB.mdb";
			OleDbConnection __OleConn=new OleDbConnection(__ConnStr);
			return __OleConn;			
		}

		public static DataView __GetDV(OleDbConnection __Conn,string __SqlStr,string __TableName)
		{
			OleDbDataAdapter	__Pter=null;
			DataSet				__DS=	new DataSet();
			DataView			__DV=null;
			try
			{
				__Pter=new OleDbDataAdapter(__SqlStr,__Conn);
				__Pter.Fill(__DS,__TableName);
				__DV=__DS.Tables[__TableName].DefaultView;

			}
			catch(OleDbException Ex)
			{
				//Session["errors"]=Session["errors"].ToString()+Ex.Message.ToString()+"<br>";
			}
			finally
			{
				if(__Pter!=null)		__Pter.Dispose();
				if(__DS!=null)			__DS.Dispose();
				if(__Conn!=null)		__Conn.Dispose();
			}
			return __DV;
		}
	}
}
