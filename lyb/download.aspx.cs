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

namespace MessageBor.MessageBoard
{
	/// <summary>
	/// download ��ժҪ˵����
	/// </summary>
	public class download : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				string __Path=string.Empty;
				__Path = Request.Params["path"].ToString().Trim();
				//string ss=Request.MapPath("")+"\\"+"lyword\\20050523221539202.jpg";
				System.IO.FileInfo file=new System.IO.FileInfo(Request.MapPath("")+"\\"+__Path);
				Response.Clear();
				Response.Charset="GB2312";
				Response.ContentEncoding=System.Text.Encoding.UTF8;
				Response.AddHeader("Content-Disposition","attachment;filename="+Server.UrlEncode(file.Name));
				Response.AddHeader("Content-Length",file.Length.ToString());
				Response.ContentType="application/octet-stream";
				Response.WriteFile(file.FullName);
				Response.End();
			}
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
