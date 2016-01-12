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
using System.IO ;

namespace MessageBor
{
	/// <summary>
	/// Adminhf ��ժҪ˵����
	/// </summary>
	public class Adminhf : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell TD_header;
		protected System.Web.UI.HtmlControls.HtmlTextArea T_hfnr;
		protected System.Web.UI.HtmlControls.HtmlInputFile upFile;
		protected System.Web.UI.HtmlControls.HtmlTableCell TD_foot;
		string _lyfilesname=string.Empty ;
		protected System.Web.UI.WebControls.Button bt_save;
		protected System.Web.UI.WebControls.Button bt_return;
		string _hfwordbs="1";
		object obj,objs;
	static	string _Cuid=string.Empty ;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��

			if(!IsPostBack)
			{ 
				clsMain.WriterHFinfo(TD_header,TD_foot);
				objs=Session["admin"];
				if(objs!=null && objs.ToString ().Trim ().CompareTo ("admin")==0)
				{
					if(objs!=null)
					{
						obj=Request.Params ["cuid"];
						if(obj!=null)
							_Cuid=obj.ToString ();
						if(!UserParameters.IsNumbers(_Cuid))
						{
							Response.Write("<font color='Red' size='9'>�����ˡ���</font>");
							Response.End();
						}
					}
				}
				else
				{
					Response.Write("��û�лظ����Ե�Ȩ��,�������Ա��ϵ��");
					Response.Redirect ("login_M.aspx");		
				}
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
			this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
			this.bt_return.Click += new System.EventHandler(this.bt_return_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		private void File_upDownd()//�ϴ��ļ�
		{
			
			_lyfilesname = upFile.PostedFile.FileName;
			_lyfilesname = Path.GetFileName(_lyfilesname);//����ļ�����			
		int	Ll_FileLength = upFile.PostedFile.ContentLength;//����ļ��ֽڴ�С
			//Ls_FileContentType = upFile.PostedFile.ContentType;//�ļ�����
			
			if(Ll_FileLength > 2000*1024)
				Response.Write("<script language='JavaScript'>alert('�ϴ����ļ�̫��!')</script>");
			else
			{
				try
				{
					upFile.PostedFile.SaveAs(HttpRuntime.AppDomainAppPath +@"hfword\"+_lyfilesname);				
					_hfwordbs = "2";//�ٴγ�ʼ��ʶ
				}
				catch(Exception ex)
				{
					_hfwordbs = "1";//�ٴγ�ʼ��ʶ
					Session["errors"]=Session["errors"].ToString()+ex.Message.ToString()+"<br>";
				}						
			}			
		}
		private void SaveUpdateInfo()
		{
			//zhsp_UpdataeMsgInfo
			if(T_hfnr.Value .Trim ().Length ==0)
				return;
			string _hfrq=UserParameters.CurrentDate ();
			string _hfword =@"hfword\"+_lyfilesname;
			string[] MParametes={"@hfnr","@hfrq","@hfword","@hfwordbs"};
			//if(ZhNetLibrary.IsHtml(T_hfnr.Value.Trim ()))
			string[] MPtext={UserParameters.ToNewString2(ZhNetLibrary.FiltrationStr(T_hfnr.Value.Trim ())),_hfrq,_hfword,_hfwordbs};//_Cuid
			string	__SqlStr="UPDATE Msg_book  SET hfnr=@hfnr,hfrq=@hfrq,hfword=@hfword,hfwordbs=@hfwordbs WHERE id=@id";
			try
			{
				OleDbSaveParametersCommand(clsMain.__GetConn(),__SqlStr,MParametes,MPtext);
				Response.Redirect ("home.aspx");
			}
			catch(OleDbException ex)
			{
				Session["errors"]=Session["errors"].ToString()+ex.Message.ToString()+"<br>";
			}
		}

		private void bt_save_Click(object sender, System.EventArgs e)
		{
			//if(upFile.Value .Trim ().Length >0)
				//File_upDownd();
			//-----------
				SaveUpdateInfo();
		}

		private void bt_return_Click(object sender, System.EventArgs e)
		{
			Response.Write ("<script>document.location.href='home.aspx';</script>");
		}

		public static int OleDbSaveParametersCommand(OleDbConnection zhConn,string zhSql,string[] zhParameters,string[] zhText )
		{
			/*
			 * ����:zhConn���Ӷ���
			 *		zhParameters�洢���̲�������
			 *		zhText����ֵ����
			 * 
			 * */
			int row =0;
			OleDbCommand zhComm = new OleDbCommand(zhSql,zhConn);
			//////zhComm.CommandType = CommandType.StoredProcedure;

			for(int p=0; p<zhParameters.Length; p++)
				zhComm.Parameters.Add(zhParameters[p],zhText[p]);
				zhComm.Parameters.Add("@id",Int32.Parse(_Cuid.Trim()));
			try
			{
				if(zhConn.State == ConnectionState.Closed)
					zhConn.Open();
				row = zhComm.ExecuteNonQuery();
			}
			catch(Exception Ex)
			{
				string aaa=Ex.Message.ToString();
				string bb=aaa;
			}
			finally
			{
				if(zhConn!=null)
					zhConn.Dispose();				
			}
			if(row > 0)
				return row;
			else
				return 0;
		}
	}
}
