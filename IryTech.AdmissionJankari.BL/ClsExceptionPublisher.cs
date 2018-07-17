using System;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.IO;


namespace IryTech.AdmissionJankari.BL
{
/// <summary>
/// Summary description for ClsExceptionPublisher
/// </summary>
public class ClsExceptionPublisher
{
	public ClsExceptionPublisher()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public  void WriteLogFile(string str)
    {
         TextWriter tw = new StreamWriter("Error.log", true);
        tw.WriteLine("Exception at :- " + DateTime.Now.ToLongTimeString()  + " :: Exception Info  : - "  + str + "\n\r");
        tw.Close();

    }

    public void Publish(string exceptionInfo, string additionalInfo)
    {
        string connectString = System.Configuration.ConfigurationManager.AppSettings["DB_CON_STRING"].ToString();
        DbWrapper objDataWrapper = new DbWrapper(System.Configuration.ConfigurationManager.AppSettings["DB_CON_STRING"].ToString(), CommandType.StoredProcedure);


        try
        {
            objDataWrapper.AddParameter("@exceptionInfo", exceptionInfo);
            objDataWrapper.AddParameter("@additionalInfo", additionalInfo);
            int i = objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertException");
            
        }
        catch (Exception ex)
        {
            WriteLogFile(exceptionInfo + "," + additionalInfo);
        }
    }
}
}