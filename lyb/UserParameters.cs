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
using System.Collections;
using System.ComponentModel;
using System.Web.SessionState;
using System.Drawing;
using System.Web.UI;
using System.Data.OleDb;

namespace MessageBor
{
	/// <summary>
	/// UserParameters ��ժҪ˵����
	/// </summary>
	public class UserParameters
	{
		public UserParameters()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static SqlConnection	connection()
		{
			/*����:�����������Ӷ���
			 * ����:���ݿ����Ӷ���
			 * */
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["OA"]);
			return conn;
		}
		public static DataView dataview(string SqlString ,string TableName)
		{
			/*����:����������ͼ����
			 * 
			 * ����:SqlString �������ݿ��SQL���
			 *	   TableName ���ݼ��еı���
			 * ����:������ͼ����
			 * */
			
			SqlDataAdapter pter = new SqlDataAdapter(SqlString,connection());
			DataSet ds=new DataSet();
			pter.Fill(ds,TableName);

			DataView dv = new DataView(ds.Tables[TableName]);
			return dv;

		}
		public static string ReturnPasswordString(string jmstring)
		{
			/*����:�������ַ������м���
			 * ����:Ҫ���ܵ��ַ���
			 *����:���ܺ���ַ���
			 * */
			
			MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();
			string NewPassWord = ASCIIEncoding.ASCII.GetString(HashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(jmstring.Trim())));							
			return NewPassWord;
		}
		//��Ӷ�̬������������������������COMMAND�����ύ���ݿ⺯��
		public static int SaveParametersCommand(SqlConnection zhConn,string zhSql,string[] zhParameters,string[] zhText )
		{
			/*
			 * ����:zhConn���Ӷ���
			 *		zhParameters�洢���̲�������
			 *		zhText����ֵ����
			 * 
			 * */
			int row =0;
			SqlCommand zhComm = new SqlCommand(zhSql,zhConn);
			zhComm.CommandType = CommandType.StoredProcedure;

			for(int p=0; p<zhParameters.Length; p++)
				zhComm.Parameters.Add(zhParameters[p],zhText[p]);
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
//				Response.Write("--SQLException--<br/>");
//				Response.Write("Message:");
//				Response.Write(ex.Message.Replace("\n","<br/>")+"<br>");
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
		public static  TextBox GetFootTextBox(DataGrid dg,string TextBoxId)
		{
			/*
			 * ����:�õ�DATAGRIDָ����Ŀ(Item,Footer)�е��ӿؼ�TextBox����
			 * ����:dg�����ҵ�DataGrid����
			 *		TextBoxId�ӿؼ���ID����
			 * */
			TextBox tb = null;
			foreach( DataGridItem i in dg.Controls[0].Controls)
			{
				if(i.ItemType == ListItemType.Footer)
				{
					tb = (TextBox)i.FindControl(TextBoxId);
					break;
				}
				
			}
			return tb;
		}
		public static  TextBox GetItemTextBox(DataGrid dg,string TextBoxId)
		{
			/*
			 * ����:�õ�DATAGRIDָ����Ŀ(Item,Footer)�е��ӿؼ�TextBox����
			 * ����:dg�����ҵ�DataGrid����
			 *		TextBoxId�ӿؼ���ID����
			 * */
			TextBox tb = null;
			foreach( DataGridItem i in dg.Controls[0].Controls)
			{
				if(i.ItemType == ListItemType.Item)
				{
					tb = (TextBox)i.FindControl(TextBoxId);
					break;
				}
				
			}
			return tb;
		}

		public static  string AutoBm(string Ls_FirstBm,int ZeroNum)
		{	//����Ϊ����ĳ�ʼֵ/��Ҫ�����0��������
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
			//return DateTime.Now.DayOfWeek.ToString();
			return ss;
		}
		public static string CurrentDate()
		{
			return DateTime.Today.ToShortDateString();
		}
		public static string CurrentTime()
		{
			return DateTime.Now.Hour.ToString()+"��"+DateTime.Now.Minute.ToString()+"��"+DateTime.Now.Second.ToString();
		}
		public static DataSet dataset(string SqlString ,string TableName)
		{
			/*����:�������ݼ�����
			 * 
			 * ����:SqlString �������ݿ��SQL���
			 *	   TableName ���ݼ��еı���
			 * ����:���ݼ�����
			 * */
			SqlConnection conn=connection();
			SqlDataAdapter pter = null;			
			DataSet ds=null;
			try
			{
				pter = new SqlDataAdapter(SqlString,connection());
				ds=new DataSet();
				pter.Fill(ds,TableName);
			}
			catch
			{
			}
			finally
			{
				if(conn!=null) conn.Dispose();
				if(pter!=null) pter.Dispose();

			}
			
			return ds;

		}
		public static string Password(string strIn)//�򵥼����뺯��
		{
			strIn = strIn.Trim();
			string str = "plokm9ijn8uhb7ygv6tfc5rdx4esz12q3waQAZWSXEDCRFVTGBYHNUJMIKOLP";
			int iLen = strIn.Length;
			int iLenexp=str.Length;
			string strOne,strPassone,strPassword;
			int iFind,iMod;
			strPassword = "";
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
			return strPassword;
		}
		public static DataView dataview_2(SqlConnection Conn,string SqlString ,string TableName)
		{
			/*����:����������ͼ����
			 * 
			 * ����:SqlString �������ݿ��SQL���
			 *	   TableName ���ݼ��еı���
			 * ����:������ͼ����
			 * */
			SqlDataAdapter pter=null;
			pter = new SqlDataAdapter(SqlString,Conn);
			DataSet ds=new DataSet();
			pter.Fill(ds,TableName);

			DataView dv = new DataView(ds.Tables[TableName]);
			if(pter!=null)
				pter.Dispose();
			if(ds!=null)
				ds.Dispose();
			return dv;

		}
		public static DataSet dataset_2(SqlConnection Conn,string SqlString ,string TableName)
		{
			/*����:�������ݼ�����
			 * 
			 * ����:SqlString �������ݿ��SQL���
			 *	   TableName ���ݼ��еı���
			 * ����:���ݼ�����
			 * */
			
			SqlDataAdapter pter = new SqlDataAdapter(SqlString,Conn);
			DataSet ds=new DataSet();
			pter.Fill(ds,TableName);
			
			return ds;

		}
		public static string GetStr_Lenght(string InitiallStr,int StrLenght,string Str)
		{
			/*
			 * ���ܣ�����-- StrLenght --���ȵ���-- Str --�ַ�������ַ���
			 * ������
			 * InitiallStr	����������ַ���;
			 * StrLenght	��Ҫ���ص��ַ����ĳ���
			 * Str			�����ڲ�����ַ�
			 * */
			int j=InitiallStr.Length;
			for(int i=0;	i	<	StrLenght	-	j;	i++)
				InitiallStr=Str+InitiallStr;
			return InitiallStr;
		}
		public static int GetUserRight(string UserId,string MkId,out string bs)
		{
			/*
			 * ����OAϵͳ��ȡ�û���ÿ��ģ���Ȩ��ֵ
			 * �õ����Group_user���顢�û���ϵ����oa_right��Ȩ�ޱ�
			 * ������
			 *		UserId �û�ID
			 *		MkId	ģ��ID
			 * */
			SqlConnection Rconn=connection();
			string RightString=string.Empty;			
			string Qsbs="SELECT bs FROM oa_user WHERE id="+UserId;
			string SQLstr="SELECT MAX(oa_right.fwjb)FROM oa_right INNER JOIN  Group_user ON oa_right.group_id = Group_user.group_id WHERE (oa_right.mk_id = "+MkId+") AND (Group_user.user_id = "+UserId+")";
			bs=dataset_2(Rconn,Qsbs,"qs").Tables["qs"].Rows[0][0].ToString();

			try
			{
				RightString=dataset_2(Rconn,SQLstr,"right").Tables["right"].Rows[0][0].ToString();
				
			}
			catch
			{
			}
			finally
			{
				if(Rconn!=null)	Rconn.Dispose();
			}
			if(RightString.Length==0)
				RightString="0";
            return Convert.ToInt32(RightString.Trim());
		}
	public static DateTime GetInt_Date(string DateStr)
	{
		//�Ѵ��������е��ꡢ�¡���ȡ�����������͵���ʽ���,���DateStr����ȷ�Ĵ������ڣ��������� 1 ���� ���� 0
		//��ȷ�Ĵ������ڣ�yyyy-mm-dd   yyyy-m-d(���¡����ǵ�ʱ)
		//string[] _tokens = _ClientCommand.Split(new Char[]{'|'});//�Ѹ������ӷ�������������
		string[] _Date = DateStr.Split (new char []{'-'});
		DateTime _NewDateTime = new DateTime (Convert.ToInt32 (_Date[0]),Convert.ToInt32 (_Date[1]),Convert.ToInt32 (_Date[2]));
			
		return _NewDateTime;

	}
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
		public static string GetString(string _TheString,string _PPstr)
		{
			//�õ����һ��ƥ����ַ���������ַ�
			if(_PPstr.Trim().Length >0 && _TheString.Trim ().Length >1)
			{
				int i = _TheString.Trim ().LastIndexOf (_PPstr)+1;
				return _TheString.Trim ().Substring (i,_TheString.Trim ().Length -i);
			}
			else
				return "";
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
	}
}
