using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using System.Runtime .InteropServices ;
using System.Collections;
using System.Xml;
using System.Web.SessionState;


namespace MessageBor
{
	
	/// <summary>
	/// ZhNetLibrary ��ժҪ˵����
	/// </summary>
	public class ZhNetLibrary
	{
		public ZhNetLibrary()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private static string ConnDataConfigKey="";		
		/// <summary>
		/// WebConfig�ļ����������ݿ��ļ��ļ�ֵ
		/// </summary>
		public static string DataBaseConfigKey
		{
			get
			{
				return ConnDataConfigKey;
			}
			set
			{
				ConnDataConfigKey=value;
			}
		}
		/// <summary>
		/// �����������ݿ������ʹ��ǰ����������DataBaseConfigKey���Ը�ֵ
		/// </summary>
		/// <param name="Cukey">WebConfig�ļ����������ݿ��ļ��ļ�ֵ</param>
		/// <returns>�������ݿ����</returns>
		public static SqlConnection	connection()
		{			
			SqlConnection conn = null;
			try
			{	
				if(ConnDataConfigKey.Trim().Length==0)
					conn = new SqlConnection(ConfigurationSettings.AppSettings["SHUIFANG"]);
				else
					conn = new SqlConnection(ConfigurationSettings.AppSettings[ConnDataConfigKey]);
				
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			return conn;
		}
		/// <summary>
		/// �������ݿ�
		/// </summary>
		/// <param name="ConnKeys">�����ַ���</param>
		/// <returns>�������Ӷ���</returns>
		public static SqlConnection Connection(string ConnKeys)
		{
			SqlConnection _conn=null;
			
			try
			{
				_conn = new SqlConnection(ConnKeys);
			}
			catch(Exception  Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			return _conn;
		}
		/// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="SqlString">֧�ִ���ͼ��SQL���</param>
		/// <param name="TableName">��ͼ��ʶ</param>
		/// <returns>���ػ���SqlString��������ͼ����</returns>
		public static DataView dataview(string SqlString ,string TableName)
		{			
			SqlConnection cc=connection();			
			SqlDataAdapter pter = null;
			DataSet ds=new DataSet();			
			DataView dv = null;
			try
			{
				pter = new SqlDataAdapter(SqlString,cc);
				ds=new DataSet();
				pter.Fill(ds,TableName);
				dv = new DataView(ds.Tables[TableName]);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			finally
			{
				if(cc!=null)	cc.Dispose();
				if(pter!=null)	pter.Dispose();
				if(ds!=null)	ds.Dispose();
			}
			return dv;
		}
		/// <summary>
		/// ���ַ�������
		/// </summary>
		/// <param name="jmstring">Ҫ���ܵ��ַ���</param>
		/// <returns>���ܺ��ַ���</returns>
		public static string ReturnPasswordString(string jmstring)
		{
			string NewPassWord=string.Empty;
			MD5CryptoServiceProvider HashMD5 = null;
			try
			{
				HashMD5 = new MD5CryptoServiceProvider();
				NewPassWord = ASCIIEncoding.ASCII.GetString(HashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(jmstring.Trim())));							
			}
			catch(Exception _Ex)
			{
				throw new Exception(_Ex.Message.ToString());
			}
			finally
			{
				if(HashMD5!=null)
					HashMD5.Clear();
			}
			return NewPassWord;
		}
		/// <summary>
		/// �������ݵ����ݿ�
		/// </summary>		
		/// <param name="zhSql">Ҫִ�еĴ洢�����޷��ز���</param>
		/// <param name="zhParameters">�洢���̲�������</param>
		/// <param name="zhText">�洢���̲�����Ӧֵ����</param>
		/// <returns>ִ�гɹ�����Ӱ�����ݿ��������ʧ�ܷ��� 0 </returns>
		public static int SaveParametersCommand(SqlConnection zhConn,string zhSql,string[] zhParameters,string[] zhText )
		{
			/*
			 * ����:zhConn���Ӷ���
			 *		zhParameters�洢���̲�������
			 *		zhText����ֵ����
			 * 
			 * */
			int row =0;			
			SqlCommand zhComm = null;			
			try
			{
				zhComm = new SqlCommand(zhSql,zhConn);
				zhComm.CommandType = CommandType.StoredProcedure;
				for(int p=0; p<zhParameters.Length; p++)
					zhComm.Parameters.Add(zhParameters[p],zhText[p]);
				if(zhConn.State == ConnectionState.Closed)
					zhConn.Open();
				row = zhComm.ExecuteNonQuery();
			}
			catch(Exception Ex)
			{	
				throw new Exception(Ex.Message.ToString());
			}
			finally
			{
				if(zhConn!=null) zhConn.Dispose();
				if(zhComm!=null) zhComm.Dispose();				
			}
			if(row > 0)
				return row;
			else
				return 0;
		}
		/// <summary>
		/// ��ָ���ַ�������ָ��������'0'�ַ�
		/// </summary>
		/// <param name="Ls_FirstBm">Ҫ���䡮0�����ַ���</param>
		/// <param name="ZeroNum">��0���ĸ���</param>
		/// <returns>���ش������ַ���</returns>
		public static  string AutoBm(string Ls_FirstBm,int ZeroNum)
		{	
			string Ls_Year = DateTime.Now.Year.ToString();//��
			string Ls_Month = DateTime.Now.Month.ToString().Trim();//��
			Ls_Month=(Ls_Month.Length <2)?"0"+Ls_Month:Ls_Month;
			string Ls_Day = DateTime.Now.Day.ToString().Trim();//��
			Ls_Day = (Ls_Day.Length <2)?"0"+Ls_Day:Ls_Day;
			string Ls_BmHeard = Ls_Year+Ls_Month+Ls_Day;//����ͷ��			
			string ls_year2 = Ls_FirstBm.Substring(0,4);
			Ls_FirstBm = Ls_FirstBm.Substring(8,4);
			if(Convert.ToInt32(Ls_Year)>Convert.ToInt32(ls_year2))
				Ls_FirstBm = "0001";
			int li=Convert.ToInt32(Ls_FirstBm.Trim())+1;
			string Ls_FirstBms =li.ToString();
			Ls_FirstBm = li.ToString();
			for(int i=ZeroNum;i>Ls_FirstBms.Length;i--)
				Ls_FirstBm="0"+Ls_FirstBm;	
			return Ls_BmHeard+Ls_FirstBm;
		}
		/// <summary>
		/// ��HH��MM��SS��ʽ���ص�ǰϵͳʱ��
		/// </summary>
		/// <returns></returns>
		public static string CurrentTime()
		{
			return DateTime.Now.Hour.ToString()+"��"+DateTime.Now.Minute.ToString()+"��"+DateTime.Now.Second.ToString();
		}
		/// <summary>
		/// �������ݼ�����
		/// </summary>
		/// <param name="SqlString">֧�����ݼ���SQL���</param>
		/// <param name="TableName">���ݼ���ʶ</param>
		/// <returns>���ڴ�SQL�������ݼ�����</returns>
		public static DataSet dataset(string SqlString ,string TableName)
		{			
			SqlConnection conn=connection();
			SqlDataAdapter pter = null;			
			DataSet ds=null;
			try
			{
				pter = new SqlDataAdapter(SqlString,conn);
				ds=new DataSet();
				pter.Fill(ds,TableName);
			}			
			catch(Exception EX)
			{
				throw new Exception(EX.Message.ToString());
			}
			finally
			{
				if(conn!=null) conn.Dispose();
				if(pter!=null) pter.Dispose();
			}			
			return ds;
		}
		/// <summary>
		/// �򵥼���
		/// </summary>
		/// <param name="strIn">Ҫ���ܵ��ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public static string Password(string strIn)//�򵥼����뺯��
		{
			strIn = strIn.Trim();
			string str = "plokm9ijn8uhb7ygv6tfc5rdx4esz12q3waQAZWSXEDCRFVTGBYHNUJMIKOLP";
			int iLen = strIn.Length;
			int iLenexp=str.Length;
			string strOne,strPassone,strPassword;
			int iFind,iMod;
			strPassword = "";
			try
			{
				for (int i=0;i<iLen;i++)
				{
					strOne = strIn.Substring(i,1);
					iFind = str.IndexOf(strOne);
					iFind ++;
					if (iFind == 0)
						return "0";
					iFind = (iFind + 2) * 3;
					iMod = iFind % iLenexp;
					if (iMod == 0)
						strPassone = str.Substring(iLenexp - 1,1);
					else
						strPassone = str.Substring(iMod - 1 ,1);
					strPassword += strPassone;
				}
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			return strPassword;
		}
		/// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="Conn">�������ݿ����</param>
		/// <param name="SqlString">֧����ͼ��SQL���</param>
		/// <param name="TableName">��ͼ��ʶ</param>
		/// <returns>���ڴ�SQL����ͼ</returns>
		public static DataView dataview_2(SqlConnection Conn,string SqlString ,string TableName)
		{					
			SqlDataAdapter pter = null;
			DataView dv=null;
			DataSet ds=new DataSet();
			try
			{
				pter = new SqlDataAdapter(SqlString,Conn);
				pter.Fill(ds,TableName);
				dv = new DataView(ds.Tables[TableName]);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			finally
			{
				if(Conn!=null)	Conn.Dispose();
				if(pter!=null)	pter.Dispose();
				if(ds!=null)	ds.Dispose();
			}			
			return dv;
		}
		/// <summary>
		/// �������ݼ�����
		/// </summary>
		/// <param name="Conn">�������ݿ����</param>
		/// <param name="SqlString">֧�����ݼ���SQL���</param>
		/// <param name="TableName">���ݼ���ʶ</param>
		/// <returns>���ڴ�SQL�����ݼ�����</returns>
		public static DataSet dataset_2(SqlConnection Conn,string SqlString ,string TableName)
		{
			SqlDataAdapter pter = null;
			DataSet ds=new DataSet();
			try
			{
				pter = new SqlDataAdapter(SqlString,Conn);
				pter.Fill(ds,TableName);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			finally
			{
				if(Conn!=null)
					Conn.Dispose();
				if(pter!=null)
					pter.Dispose();
			}
			return ds;
		}
		/// <summary>
		/// �õ�-- StrLenght --���ȵ���-- Str --�ַ�������ַ���
		/// </summary>
		/// <param name="InitiallStr">��������ַ���</param>
		/// <param name="StrLenght">Ҫ���ص��ַ����ĳ���</param>
		/// <param name="Str">���ڲ�����ַ�</param>
		/// <returns>�������ַ���</returns>
		public static string GetStr_Lenght(string InitiallStr,int StrLenght,string Str)
		{			
			int j=InitiallStr.Length;
			try
			{
				for(int i=0;	i	<	StrLenght	-	j;	i++)
					InitiallStr=Str+InitiallStr;
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			return InitiallStr;
		}
		/// <summary>
		/// ��������ʱ���
		/// </summary>
		/// <param name="date1">��ʼʱ��</param>
		/// <param name="date2">��ֹʱ��</param>
		/// <param name="bs">��ʶ 1 ���ض��ַ�����������, 2 ���ش����ʱ��� </param>
		/// <returns>���ɹ����� null</returns>
		public static string DateSpan(DateTime date1,DateTime date2,string bs)
		{
			if(date1.ToString().Trim().Length==0 || date2.ToString().Trim().Length==0)
				return null;
			else
			{
				TimeSpan date3=date2.Subtract(date1);
				//�����Է�������
				if(bs.Trim()=="1")
					return date3.Hours.ToString()+"Сʱ"+date3.Minutes.ToString()+"����"+date3.Seconds.ToString()+"��";
				else if(bs.Trim()=="2")
					return date3.Days.ToString()+"��"+date3.Hours.ToString()+"Сʱ"+date3.Minutes.ToString()+"����"+date3.Seconds.ToString()+"��";
				else
					return null;
			}
		}
		/// <summary>
		/// �õ�ʱ���ֵ
		/// </summary>
		/// <param name="cudate">��ʼʱ��(?:?:?),��ֹʱ���ǵ�ǰʱ��</param>
		/// <returns>ʱ��?ʱ?��?��</returns>
		public static string GetSjC(string cudate)
		{
			string[] yea=DateTime.Now.Date.ToShortDateString().Split(new char[]{'-'});;
			string[] aaa=cudate.Split(new char[]{':'});
			string[] bbb=DateTime.Now.ToLongTimeString().Split(new char[]{':'});
			DateTime date1 = new DateTime(Int32.Parse(yea[0]),Int32.Parse(yea[1]),Int32.Parse(yea[2]),Int32.Parse(aaa[0]),Int32.Parse(aaa[1]),Int32.Parse(aaa[2]));//�ꡢ�¡��ա�ʱ���֡���
			DateTime date2 = new DateTime(Int32.Parse(yea[0]),Int32.Parse(yea[1]),Int32.Parse(yea[2]),Int32.Parse(bbb[0]),Int32.Parse(bbb[1]),Int32.Parse(bbb[2]));//�ꡢ�¡��ա�ʱ���֡���
			return DateSpan(date1,date2,"1");
		}
		/// <summary>
		/// ������ǰ���ڱ�ʾ
		/// </summary>
		/// <returns>����������ڱ�ʾ</returns>
		public static string CurrentWeek()
		{
			string ss=DateTime.Now.DayOfWeek.ToString();

			switch(ss)
			{
				case "Sunday":
					ss= "������";
					break;
				case "Monday":
					ss= "����һ";
					break;
				case "Tuesday":
					ss= "���ڶ�";
					break;
				case "Wednesday":
					ss= "������";
					break;
				case "Thursday":
					ss= "������";
					break;
				case "Friday":
					ss= "������";
					break;
				case "Saturday":
					ss= "������";
					break;
			}			
			return ss;
		}
		/// <summary>
		/// ������վ������Ϣ
		/// </summary>
		/// <param name="_ErrorInfo">������Ϣ</param>
		/// <param name="_ErrorLx">������Ϣ����</param>
		public static void SaveErrorInfo(string _ErrorInfo,string _ErrorLx)
		{
			SqlConnection _Conn = connection();
			string[] _Params = {"@error","@lx"};
			string[] _Ptexts = {_ErrorInfo,_ErrorLx};
			SaveParametersCommand(_Conn,"zhsp_saveerrors",_Params,_Ptexts);
		}
		/// <summary>
		/// ��� DropDownList �ؼ�
		/// </summary>
		/// <param name="_CuDDL">DropDownList�ؼ�ID</param>
		/// <param name="_SqlStr">֧�ֵ�SQL���</param>
		/// <param name="_Table_Name">��ͼ��ʶ</param>
		/// <param name="_Value">�����ֶ�</param>
		/// <param name="_Mc">��ʾ�ֶ�</param>
		/// <param name="_ErrorsWZ">��¼�����е�λ��</param>
		public static void Fill_DropDownList(SqlConnection _Conn,DropDownList _CuDDL,string _SqlStr,string _Table_Name,string _Value,string _Mc,string _ErrorsWZ)
		{
			//SqlConnection _Conn = new SqlConnection(ConfigurationSettings.AppSettings["YJH"]);
			try
			{
				_CuDDL.DataSource =dataview_2 (_Conn,_SqlStr,_Table_Name);
				_CuDDL.DataTextField =_Mc;
				_CuDDL.DataValueField = _Value;
				_CuDDL.DataBind ();
			}
			catch(Exception _ex)
			{
				throw new Exception(_ex.Message.ToString());
			}			
			finally
			{
				if(_Conn!=null) _Conn.Dispose ();
			}			
		}
		/// <summary>
		/// �õ����һ��ƥ����ַ���������ַ�
		/// </summary>
		/// <param name="_TheString">�����ַ���</param>
		/// <param name="_PPstr">ƥ���ַ�</param>
		/// <returns>���һ��ƥ����ַ���������ַ�</returns>
		public static string GetString(string _TheString,string _PPstr)
		{
			if(_PPstr.Trim().Length >0 && _TheString.Trim ().Length >1)
			{
				int i = _TheString.Trim ().LastIndexOf (_PPstr)+1;
				return _TheString.Trim ().Substring (i,_TheString.Trim ().Length -i);
			}
			else
				return "";
		}
		/// <summary>
		/// ����ҩ������վ�õ����ݿ��е��Ѻ���������(��html��ʽ��ʾ���ַ���)
		/// </summary>
		/// <returns>����HTML�ַ���</returns>
		public static string FriendConnection()
		{
			/*
			 * ��ȡ��������Ϣ
			 * */
			SqlConnection _Conn=connection ();
			StringBuilder lsb_cl = new StringBuilder ("<font COLOR='BLUE'>�Ѻ����ӣ�</font>&nbsp;");
			DataSet F_ds = dataset_2 (_Conn,"SELECT friendmc,url FROM hy_yjh_friendconnection Where bs='1'","Friendcl");
			foreach(DataRow CL in F_ds.Tables ["Friendcl"].Rows )
				lsb_cl.Append ("&nbsp;<a href='"+CL["url"].ToString ()+"' target='_blank' class='cslist'>"+CL["friendmc"].ToString ()+"</a>");
			return lsb_cl.ToString ();
		}
		/// <summary>
		/// �ϴ��ļ�
		/// </summary>
		/// <param name="upFile">HtmlInputFile�ؼ�ID</param>
		/// <param name="_lyfilesname">�����ļ�����</param>
		/// <param name="Ll_FileLength">�����ļ���С</param>
		/// <param name="Ls_FileContentType">�����ļ�����</param>
		/// <param name="_ML">�ϴ��ļ��ķ�����Ŀ¼(����Ŀ¼)</param>
		/// <param name="_FileSize">�ϴ��ļ����ֵ��KB��</param>
		/// <returns>����0��ʶ�ɹ�</returns>
		public static string File_upDownd(HtmlInputFile upFile, out string _lyfilesname,out int Ll_FileLength,out string Ls_FileContentType,string _ML,int _FileSize)
		{
			string _bs=string.Empty;
			_lyfilesname = upFile.PostedFile.FileName;
			_lyfilesname = Path.GetFileName(_lyfilesname);//����ļ�����
			Ll_FileLength = upFile.PostedFile.ContentLength;//����ļ��ֽڴ�С
			Ls_FileContentType = upFile.PostedFile.ContentType;//�ļ�����

			if(Ll_FileLength > _FileSize*1024)
			{				
				throw new Exception("�ϴ��ļ�̫��");
			}
			else
			{
				try
				{
					upFile.PostedFile.SaveAs(HttpRuntime.AppDomainAppPath +_ML+@"\"+_lyfilesname);
					_bs="0";//�ϴ��ɹ�
					
				}
				catch(Exception ex)
				{						
					throw new Exception(ex.Message.ToString());
				}						
			}
			return _bs;//����0��ʶ�ɹ�
		}
		/// <summary>
		/// �õ���ǰϵͳ���ڡ����� �ַ���
		/// </summary>
		/// <returns>string</returns>
		public static string DataWeek()
		{
			string ls_Year=string.Empty ,ls_Month=string.Empty ,ls_Day=string.Empty ,ls_Week=string.Empty ;
			int li_Time=0;
			li_Time = Convert.ToInt32(DateTime.Now.Hour.ToString());	//��ǰʱ��
			ls_Week = System.DateTime.Today.DayOfWeek.ToString();		//��ǰ����
			ls_Year = DateTime.Now.Year.ToString()+"��";
			ls_Month = DateTime.Now.Month.ToString()+"��";
			ls_Day = DateTime.Now.Day.ToString()+"��";
			switch (ls_Week)
			{
				case "Monday":
					ls_Week = "����һ";break;
				case "Tuesday":
					ls_Week = "���ڶ�";break;
				case "Wednesday":
					ls_Week = "������";break;
				case "Thursday":
					ls_Week = "������";break;
				case "Friday":
					ls_Week = "������";break;
				case "Saturday":
					ls_Week = "������";break;
				case "Sunday":
					ls_Week = "������";break;
			}
			return "&nbsp;"+ls_Year+ls_Month+ls_Day+" <font color='Red'>��</font>"+ls_Week+"<font color='Red'>��</font> &nbsp;&nbsp;��<a href=''>��½</a>��<a href=''>ע��</a>��<a href=''>��Աר��</a>��";
			
		}
		/// <summary>
		/// ���DataList���
		/// </summary>
		/// <param name="CuDL">��ǰDataList</param>
		/// <param name="SqlStr">SQL����ַ���</param>
		/// <param name="TableName">����</param>
		public static void FillDList(DataList CuDL,string SqlStr,string TableName,string Wz)
		{
			if(SqlStr.Trim().Length==0 || TableName.Trim().Length==0)
				return;
			SqlConnection _Conn=connection();
			try
			{
				CuDL.DataSource=dataset_2(_Conn,SqlStr,TableName);
				CuDL.DataBind();
			}
			catch(Exception _Ex)
			{
				throw new Exception(_Ex.Message.ToString());
			}
			finally
			{
				if(_Conn!=null)
					_Conn.Dispose();
			}
		}
		/// <summary>
		/// ���DataGrid���
		/// </summary>
		/// <param name="CuDL">��ǰDataGrid</param>
		/// <param name="SqlStr">SQL����ַ���</param>
		/// <param name="TableName">����</param>
		public static void FillDataGrid(SqlConnection _Conn,DataGrid CuDL,string SqlStr,string TableName)
		{
			if(SqlStr.Trim().Length==0 || TableName.Trim().Length==0)
				return;
			//SqlConnection _Conn=connection();
			try
			{
				CuDL.DataSource=dataset_2(_Conn,SqlStr,TableName);
				CuDL.DataBind();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			finally
			{
				if(_Conn!=null)
					_Conn.Dispose();
			}
		}
		/// <summary>
		/// ���LISTBOX�ؼ�����
		/// </summary>
		/// <param name="CuLB">��ǰListBox�ؼ�</param>
		/// <param name="SqlStr">SQL���</param>
		/// <param name="TableName">����</param>
		/// <param name="TextField">Ҫ��ʾ���ֶ�</param>
		/// <param name="ValueField">�����ֶ�</param>
		public static void Fill_ListBox(ListBox CuLB,string SqlStr,string TableName,string TextField,string ValueField)
		{
			SqlConnection _Conn=connection();
			try
			{
				CuLB.DataSource=dataset_2(_Conn,SqlStr,TableName);
				CuLB.DataTextField = TextField;
				CuLB.DataValueField= ValueField;
				CuLB.DataBind();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			finally
			{
				if(_Conn!=null)
					_Conn.Dispose();
			}
		}
		/// <summary>
		/// ListBox�б�ؼ�֮���ƶ��б���
		/// </summary>
		/// <param name="CurrentListBoxFrom">��ListBox</param>
		/// <param name="CurrentListBoxTo">��ListBox</param>
		public static void CurrentLB_Choose(ListBox CurrentListBoxFrom,ListBox CurrentListBoxTo)
		{
			int i=0,j=0;
			j=CurrentListBoxFrom.Items.Count;
			ListItem[] CuChoose=new ListItem[j];
			foreach(ListItem CuListitem in CurrentListBoxFrom.Items)
				if(CuListitem.Selected==true)					
					if(CurrentListBoxTo.Items.FindByValue(CuListitem.Value)==null)
					{						
						CurrentListBoxTo.Items.Add(CuListitem);
						CuChoose[i]=CuListitem;
						i++;
					}
			foreach(ListItem Citm in CuChoose)
				CurrentListBoxFrom.Items.Remove(Citm);
			CurrentListBoxTo.ClearSelection();
		}
		/// <summary>
		/// ��һ��LISTBOX�е��б���ȫ���ƶ�����һ��LISTBOX�б�����
		/// </summary>
		/// <param name="CurrentListBoxFrom">From�б�ؼ�</param>
		/// <param name="CurrentListBoxTo">To�б�ؼ�</param>
		/// <param name="istrue">ȫ���ƶ���ʶ</param>
		public static void CurrentLB_Choose(ListBox CurrentListBoxFrom,ListBox CurrentListBoxTo,bool istrue)
		{
			int i=0,j=0;
			j=CurrentListBoxFrom.Items.Count;
			ListItem[] CuChoose=new ListItem[j];
			foreach(ListItem CuListitem in CurrentListBoxFrom.Items)
				if(CurrentListBoxTo.Items.FindByValue(CuListitem.Value)==null)
				{						
					CurrentListBoxTo.Items.Add(CuListitem);
					CuChoose[i]=CuListitem;
					i++;
				}
			foreach(ListItem Citm in CuChoose)
				CurrentListBoxFrom.Items.Remove(Citm);
			CurrentListBoxTo.ClearSelection();			
		}
		/// <summary>
		/// �Ѵ���\r\n���ַ���ת��Ϊ���Զ��ֶεı�׼�ַ���(�μ��ϴ�)
		/// </summary>
		/// <param name="OldString">Ҫת�����ַ���</param>
		/// <returns>�µ��ַ���</returns>
		public static string ToNewString(string OldString)
		{
			if(OldString.Trim().Length==0)	return "";
			
			OldString="<p>&nbsp;&nbsp;&nbsp;&nbsp;"+OldString.Trim(new char[]{'\t'});
			OldString=OldString.Replace("\r\n","</p><p><br>&nbsp;&nbsp;&nbsp;&nbsp;");
			OldString=OldString+"</p>";
			return OldString;
		}
		/// <summary>
		/// �Ѵ���\r\n���ַ���ת��Ϊ���Զ��ֶεı�׼�ַ���(��׼�μ��)
		/// </summary>
		/// <param name="OldString">Ҫת�����ַ���</param>
		/// <returns>�µ��ַ���</returns>
		public static string ToNewString2(string OldString)
		{
			if(OldString.Trim().Length==0)	return "";
			
			OldString="<p>&nbsp;&nbsp;&nbsp;&nbsp;"+OldString.Trim(new char[]{'\t'});
			OldString=OldString.Replace("\r\n","<br>&nbsp;&nbsp;&nbsp;&nbsp;");
			OldString=OldString+"</p>";
			return OldString;
		}
		/// <summary>
		/// �Ѵ���\r\n���ַ���ת��Ϊ���Զ��ֶεı�׼�ַ���(��׼�μ��)
		/// </summary>
		/// <param name="OldString">Ҫת�����ַ���</param>
		/// <returns>�µ��ַ���</returns>
		public static string ToNewString3(string OldString)
		{
			if(OldString.Trim().Length==0)	return "";
			
			OldString="<p>"+OldString.Trim(new char[]{'\t'});
			OldString=OldString.Replace("\r\n","<br>&nbsp;&nbsp;");
			OldString=OldString+"</p>";
			return OldString;
		}
		/// <summary>
		/// �Ѵ���\r\n���ַ���ת��Ϊ���Զ��ֶεı�׼�ַ���(��׼�μ��)
		/// </summary>
		/// <param name="OldString">Ҫת�����ַ���</param>
		/// <returns>�µ��ַ���</returns>
		public static string ToNewString4(string OldString)
		{
			if(OldString.Trim().Length==0)	return "";
			
			OldString=""+OldString.Trim(new char[]{'\t'});
			OldString=OldString.Replace("\r\n","<br>&nbsp;&nbsp;");
			OldString=OldString+"";
			return OldString;
		}
		/// <summary>
		/// ִ��Sql��䣬�����ض����ݿ��Ӱ������
		/// </summary>
		/// <param name="SqlString">Ҫִ�е�SQL���</param>
		/// <param name="wz">ִ�д�SQL���ĳ���λ��</param>
		/// <returns> 0 ִ��ʧ�� 1 ִ�гɹ�</returns>
		public static int BeginSqlCommand(string SqlString,string wz)
		{
			if(SqlString.Trim().Length==0) return 0;
			SqlConnection _Conn = null;
			SqlTransaction _Tran= null;
			SqlCommand		_Comm=null;
			int Rows=0;
			try
			{
				_Conn=connection();
				_Conn.Open();
				_Tran =_Conn.BeginTransaction();
				_Comm = new SqlCommand(SqlString,_Conn,_Tran);
				Rows=_Comm.ExecuteNonQuery();
				_Tran.Commit();
			}
			catch(Exception _Ex)
			{
				_Tran.Rollback();
				throw new Exception(_Ex.Message.ToString());
			}
			finally
			{
				if(_Conn!=null) _Conn.Dispose();
				if(_Comm!=null) _Comm.Dispose();
				if(_Tran!=null) _Tran.Dispose();
			}
			return Rows;
		}
		/// <summary>
		/// �ж���ǰ�ַ����Ƿ�Ϊ����,�����ַ���TRUE ����ΪFALSE
		/// </summary>
		/// <param name="CuStr">Ҫ��֤���ַ���</param>
		/// <returns>�����ַ���TRUE ����ΪFALSE</returns>
		public static bool IsNumbers(string CuStr)
		{
			if(CuStr.Trim().Length==0) return false;
			bool _bs=true;
			for(int i=0;i<CuStr.Trim().Length;i++)
				if(!(char.IsNumber(CuStr,i)))
				{
					_bs=false;
					break;
				}
			return _bs;
		}
		/// <summary>
		/// ���ArrayList�еĿ�ö�ٵĳ�Ա
		/// </summary>
		/// <param name="myList">����ö�����ӿ�</param>
		public static void PrintValues( IEnumerable myList )  
		{
			System.Collections.IEnumerator myEnumerator = myList.GetEnumerator();
			while ( myEnumerator.MoveNext() )
				Console.Write( "\t{0}", myEnumerator.Current );
			Console.WriteLine();
		}
		/// <summary>
		/// [DaArray]����̬����
		/// </summary>
		public IList DyArray
		{
			get{return dyarray;}

		}private ArrayList dyarray=new ArrayList();
		/// <summary>
		/// �ܿ�<>�ڵ����ݣ������滻
		/// </summary>
		/// <param name="CuHtmlStr">Ҫ�����HTML�ִ�</param>
		/// <param name="OldStr">Ҫ���ҵ��ַ���</param>
		/// <param name="NewStr">�滻����</param>
		/// <returns>�����µ�HTML�ַ���</returns>
		public static string OverReplace(string CuHtmlStr,string OldStr,string NewStr)
		{
			/*
			 * ָ��˼����E�̿����ĵ�
			 * */
			if(CuHtmlStr.Trim().Length==0) return null;
			if(OldStr.Trim().Length>0)
			{
				if(CuHtmlStr.Trim().IndexOf("<",0,CuHtmlStr.Trim().Length)==-1)	//��HTML�ַ���				
					return CuHtmlStr.Replace(OldStr,NewStr);
				string[] HtmlArrar=CuHtmlStr.Split(new char[]{'<'});
				for(int i=0;i<HtmlArrar.Length;i++)
				{
					if(HtmlArrar[i].IndexOf(">",0,HtmlArrar[i].Trim().Length)>=0)
						HtmlArrar[i]=HtmlArrar[i].Split(new char[]{'>'})[1];
					if(HtmlArrar[i].Length>0)
						CuHtmlStr=CuHtmlStr.Replace(HtmlArrar[i],HtmlArrar[i].Replace(OldStr,NewStr));
				}
				return CuHtmlStr;
			}
			else
				return CuHtmlStr;		
		}
		/// <summary>
		/// ��� CheckBoxList �ؼ�
		/// </summary>
		/// <param name="_CuDDL">CheckBoxList�ؼ�ID</param>
		/// <param name="_SqlStr">֧�ֵ�SQL���</param>
		/// <param name="_Table_Name">��ͼ��ʶ</param>
		/// <param name="_Value">�����ֶ�</param>
		/// <param name="_Mc">��ʾ�ֶ�</param>
		/// <param name="_ErrorsWZ">��¼�����е�λ��</param>
		public static void Fill_ChecBoxList(CheckBoxList _CuDDL,string _SqlStr,string _Table_Name,string _Value,string _Mc,string _ErrorsWZ)
		{
			SqlConnection _Conn = new SqlConnection(ConfigurationSettings.AppSettings["YJH"]);
			try
			{
				_CuDDL.DataSource =dataview_2 (_Conn,_SqlStr,_Table_Name);
				_CuDDL.DataTextField =_Mc;
				_CuDDL.DataValueField = _Value;
				_CuDDL.DataBind ();
			}
			catch(Exception _ex)
			{
				throw new Exception(_ex.Message.ToString());
			}			
			finally
			{
				if(_Conn!=null) _Conn.Dispose ();
			}			
		}
		/// <summary>
		/// ��֤���崮���Ƿ����HTML����
		/// </summary>
		/// <param name="ValidatedStr">����֤���崮</param>
		/// <returns>����HTML����TRUE ���򷵻�FALSE	</returns>
		public static bool IsHtml(string ValidatedStr)
		{
			if(ValidatedStr.Trim().Length==0)
				return true;
			// validate <> script /
			int validateBs_1=-1,validateBs_2=-1;
			validateBs_1=ValidatedStr.IndexOf("<");			
			validateBs_2=ValidatedStr.IndexOf(">");

			if(validateBs_2>0 && validateBs_1>=0)
				return true;
			validateBs_1=-1;
			validateBs_2=-1;

			validateBs_1=ValidatedStr.IndexOf("script");
			validateBs_2=ValidatedStr.IndexOf("SCRIPT");

			if(validateBs_2>0 && validateBs_1>=0)
				return true;
			else
				return false;
		}
		public static string CurrentDateTime()	
		{
			return DateTime.Now.ToString();
		}
		public static string CurrentYear()
		{
			return DateTime.Now.Year.ToString();
		}
		public static string CurrentMonth()
		{
			return DateTime.Now.Month.ToString();
		}
		public static string CurrentDay()
		{
			return DateTime.Now.Day.ToString();
		}
		public static string CurrentDate()
		{
			return DateTime.Today.ToShortDateString();
		}
		public static string CurrentTime2()
		{
			return DateTime.Now.Hour.ToString()+"��"+DateTime.Now.Minute.ToString()+"��"+DateTime.Now.Second.ToString();
		}
		public static string Login_Users()
		{
			string _UserNumbers=string.Empty;
			try
			{
				_UserNumbers=dataset("SELECT count(*) FROM Exam_ksrs","usernumbers").Tables["usernumbers"].Rows[0][0].ToString();
			}
			catch
			{			
			}
			if(_UserNumbers.Trim().Length >0)
				_UserNumbers=(Convert.ToInt32(_UserNumbers)+2000).ToString();
			return _UserNumbers;
		}
		/// <summary>
		/// �����໰
		/// </summary>
		/// <param name="_BadStr">Ҫ���˵Ĵ�</param>
		/// <returns></returns>
		public static string FiltrationStr(string _BadStr)
		{
			if(_BadStr.Trim().Length==0 ) return "";
			string Custr=string.Empty;
			Custr=_BadStr;
			string[] BadLanguage={"�������","��������","��","����","�Ҳ�","�����","�ҿ�","fuck","bitch","�����","�԰�","���ֹ�","falundafa","falun","������","������","����Ƭ","sex","��","��","�","����","��","��","�","��","��ͷ","��","�P","��","�H","��","��"};
			foreach(string i in BadLanguage)
			Custr	=Custr.Replace(i.Trim(),"��Ҫ˵�໰");
			return Custr;
		}
		/// <summary>
		/// �õ�����������
		/// </summary>
		/// <param name="ExamLb">�������</param>
		/// <returns></returns>
		public static string GetDm(string ExamLb)
		{
			if(ExamLb.Trim().Length==0) return null;
			string LbDm=string.Empty;

			switch (ExamLb.Trim())
			{
				case "IELTS":
					LbDm="05";
				break;
				case "TOEFL":
					LbDm="04";
					break;
				case "GRADUATE":
					LbDm="03";
					break;
				case "FOURSIX":
					LbDm="02|01";
					break;
			}
			return LbDm;
		}
		public static string __GetName(string __AS_id,string __AS_Sql)//select ypmc from hh_dm_ypmc 
		{
			string __Name=string.Empty;
			SqlCommand __Comm= null;
			SqlConnection	__Conn =null;
			try
			{
				__Conn = ZhNetLibrary.connection();
				__Comm = new SqlCommand(__AS_Sql+"  WHERE uid="+__AS_id ,__Conn);
				__Conn.Open();
				__Name	=	__Comm.ExecuteScalar().ToString();
			}
			catch(Exception Ex)
			{
				string aa=Ex.Message.ToString();
			}
			finally
			{
				if(__Conn!=null)
					__Conn.Close();
				__Conn.Dispose();
				if(__Comm!=null)
					__Comm.Dispose();
			}
			return __Name;
		}
		/// <summary>
		/// �õ�����ID�ַ���
		/// </summary>
		/// <param name="__AS_Column">���� "ypmc"</param>
		/// <param name="__AS_Sql">SQL���"select id from HH_dm_ypmc"</param>
		/// <param name="__AS_Contain">��ѯ��������"����Ƭ"</param>
		/// <returns>������ "id=1 or id=2 "��</returns>
		public static string __GetID(string	__AS_Column,string	__AS_Sql,string	__AS_Contain)
		{
			if(__AS_Column.Trim().Length==0	|| __AS_Sql.Trim().Length==0)
				return null;
			SqlCommand	__Comm	=	null;
			SqlConnection	__Conn	=	null;
			string	__WhereStr	=	string.Empty;
			try
			{
				__Conn	=	ZhNetLibrary.connection();				
				__Comm	=	new SqlCommand(__AS_Sql+" WHERE "+__AS_Column+"  like '%"+__AS_Contain+"%'",__Conn);
				__Conn.Open();
				SqlDataReader	__DataReader	=	__Comm.ExecuteReader();

				while(__DataReader.Read())
					__WhereStr += __AS_Column+" = '"+__DataReader.GetInt32(0).ToString().Trim()+"' or ";
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message.ToString());
			}
			finally
			{
				if(__Conn!=null)
					__Conn.Close();
				__Conn.Dispose();
				if(__Comm!=null)
					__Comm.Dispose();
			}
			if(__WhereStr.Trim().Length>0)
				__WhereStr=__WhereStr.Trim().TrimEnd(new char[]{'r'}).TrimEnd(new char[]{'o'});

			return __WhereStr;
		}
		/// <summary>
		/// ����һ��Ư���Ŀؼ�
		/// </summary>
		/// <param name="__As_Width">�ؼ����ܿ��</param>
		/// <param name="__As_BarHeight">�������ĸ߶�</param>
		/// <param name="__As_ItemWidth">��Ŀ�Ŀ��</param>
		/// <param name="__As_ItemHeight">��Ŀ�ĸ߶�</param>
		/// <param name="__As_BarImagPath">����������ͼƬ·��</param>
		/// <param name="__As_ItemImagPath">��ĿͼƬ·��</param>
		/// <param name="__As_BarFeilds">�������ֶ�</param>
		/// <param name="__As_BarSql">���������ݿ�SQL���</param>
		/// <param name="__As_ItemFeilds">��Ŀ�ֶ�</param>
		/// <param name="__As_ItemSql">��Ŀ���ݿ�SQL���</param>
//		public	static	void	__CreateWebBar(int	__As_Width,int	__As_BarHeight,int	__As_ItemWidth,int	__As_ItemHeight,
//			string	__As_BarImagPath,string	__As_ItemImagPath,string	__As_BarFeilds,string	__As_BarSql,
//			string	__As_ItemFeilds,string	__As_ItemSql)
//		{
//			StringBuilder	__CreateString	=	new StringBuilder("");
//			
//			DataView	__BarDv	=null;
//			DataView	__ItemDv=null;
//
//			__BarDv	=	ZhNetLibrary.dataview(__As_BarSql,"bardv");
//			if(__BarDv.Count==0)
//			{
//				throw new Exception("�������ݿ�ʧ�ܣ�");
//				return;
//			}
//			__ItemDv	=	ZhNetLibrary.dataview(__As_ItemSql,"itemdv");
//
//			__CreateString.Append("<TABLE id='Table2' style='BORDER-RIGHT: #7aa4d4 1px solid; BORDER-LEFT: #7aa4d4 1px solid'"+
//									" cellSpacing='0' cellPadding='0' width='166' border='0' height='500' bgColor='#7aa4d4'> <TR> "+
//									"<TD align='center' vAlign='middle'>  ");
//
//		}
	}
}
