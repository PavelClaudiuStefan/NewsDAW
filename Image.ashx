<%@ WebHandler Language="C#" Class="Image" %>

using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class Image : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        Int32 empno;
        string article_id = context.Request.QueryString["article_id"];
        string user_id = context.Request.QueryString["user_id"];
        string selectCommand = "SELECT thumbnail FROM ARTICLE WHERE id = @id";
        if (article_id != null)
        {
            empno = Convert.ToInt32(article_id);
            selectCommand = "SELECT thumbnail FROM ARTICLE WHERE id = @id";
        }
        else if (user_id != null)
        {
            empno = Convert.ToInt32(user_id);
            selectCommand = "SELECT image FROM USER WHERE id = @id";
        }
        else
        {
            return;
        }

        context.Response.ContentType = "image/jpeg";
        Stream strm = ShowEmpImage(empno, selectCommand);
        byte[] buffer = new byte[4096];
        int byteSeq = 0;
        try
        {
            byteSeq = strm.Read(buffer, 0, 4096);
        } catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        while (byteSeq > 0)
        {
            context.Response.OutputStream.Write(buffer, 0, byteSeq);
            byteSeq = strm.Read(buffer, 0, 4096);
        }
        //context.Response.BinaryWrite(buffer);
    }

    public Stream ShowEmpImage(int empno, string selectCommand)
    {
        string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(conn);
        SqlCommand cmd = new SqlCommand(selectCommand,connection);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@id", empno);
        connection.Open();
        object img = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])img);
        }
        catch
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}