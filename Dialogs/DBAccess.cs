using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class DBAccess
    {

        string channel = "";
        DataTable dtResult;
        

        public void Update_Flag()
        {
            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);
                
                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
         
                    sb.Append("update dbo.atnt_bot set flag=1 where User_Name='Sales_TeamLead_Mark_EMPxxx123'");
                    // Trace.TraceInformation("IN DB Access" + channel);
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //dtResult = new DataTable();
                        //dtResult.Load(command.ExecuteReader());
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        






        public DataTable Select_Flag()
        {


            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);
                
                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select top 1 flag from dbo.atnt_bot order by Date_Time desc");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        dtResult = new DataTable();
                        dtResult.Load(command.ExecuteReader());
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return dtResult;
        }


        



        public void AppendData(string result)
        {

            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);
                channel = result;
                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into dbo.cssagent(USERID,PERSON,DESIGNATION,EMAILID,TRAINING_MATERIAL,TOTAL_TIME,PASSWORD_USER,COURSE_DATE) values('MS000123', 'RAHUL', 'JUNIOR AGENT', 'RAHUL@microsoft.com', 'AZURE IOT', '200', 'password', CURRENT_TIMESTAMP) ");
                    // Trace.TraceInformation("IN DB Access" + channel);
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", channel);
                        dtResult = new DataTable();
                        dtResult.Load(command.ExecuteReader());
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        public void insertTicket()
        {

            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);

                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into dbo.bok_ticket(Ticket,username) values('BoK001785','Priya')");
                    // Trace.TraceInformation("IN DB Access" + channel);
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        dtResult = new DataTable();
                        dtResult.Load(command.ExecuteReader());
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        public DataTable SelectTicket()
        {

            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);

                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("select Ticket from dbo.bok_ticket where User_Name='Priya' ");
                        // Trace.TraceInformation("IN DB Access" + channel);
                        String sql = sb.ToString();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            dtResult = new DataTable();
                            dtResult.Load(command.ExecuteReader());
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return dtResult;

        }


        //public DataTable SelectData(string result)
        //{

        //    try
        //    {
        //        //context.ConversationData.TryGetValue<string>("channel",out channel);
        //        channel = result;
        //        //string channel = "Milestones";
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        var cs = ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString))
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //            {
        //                connection.Open();
        //                StringBuilder sb = new StringBuilder();
        //                sb.Append("select TRAINING_MATERIAL from dbo.CSSAGENT where person='PRIYA'");
        //                // Trace.TraceInformation("IN DB Access" + channel);
        //                String sql = sb.ToString();
        //                using (SqlCommand command = new SqlCommand(sql, connection))
        //                {

        //                    dtResult = new DataTable();
        //                    dtResult.Load(command.ExecuteReader());
        //                }
        //                connection.Close();
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //    return dtResult;

        //}

        //public DataTable select_date()
        //{

        //    try
        //    {
        //        //context.ConversationData.TryGetValue<string>("channel",out channel);
        //        //channel = result;
        //        //string channel = "Milestones";
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        var cs = ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString))
        //        {
        //            connection.Open();
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("select COURSE_DATE from dbo.cssagent");
        //            // Trace.TraceInformation("IN DB Access" + channel);
        //            String sql = sb.ToString();
        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                dtResult = new DataTable();
        //                dtResult.Load(command.ExecuteReader());
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    return dtResult;
        //}

        //public DataTable select_user()
        //{

        //    try
        //    {
        //        //context.ConversationData.TryGetValue<string>("channel",out channel);
        //        //channel = result;
        //        //string channel = "Milestones";
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        var cs = ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cssAgent"].ConnectionString))
        //        {
        //            connection.Open();
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("select top 1  USER_NAME from dbo.User_Login order by DATE_TIME desc");
        //            // Trace.TraceInformation("IN DB Access" + channel);
        //            String sql = sb.ToString();
        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                dtResult = new DataTable();
        //                dtResult.Load(command.ExecuteReader());
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    return dtResult;
        //}




        //public void UpdateData(string result, int points)
        //{
        //    try
        //    {
        //        //context.ConversationData.TryGetValue<string>("channel",out channel);
        //        channel = result;
        //        //string channel = "Milestones";
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cssAgent"].ToString()))
        //        {
        //            connection.Open();
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("update dbo.csspoints set points=" + points + " where person='" + channel + "'");
        //            // Trace.TraceInformation("IN DB Access" + channel);
        //            String sql = sb.ToString();
        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                //dtResult = new DataTable();
        //                //dtResult.Load(command.ExecuteReader());
        //                command.ExecuteNonQuery();
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}
        public void Update_Course_Points(string result, int points)
        {
            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);
                channel = result;
                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update dbo.csspoints set COURSE_POINTS=" + points + " where person='" + channel + "'");
                    // Trace.TraceInformation("IN DB Access" + channel);
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //dtResult = new DataTable();
                        //dtResult.Load(command.ExecuteReader());
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        //public void UpdateData_feedback(string result, int points)
        //{
        //    try
        //    {
        //        //context.ConversationData.TryGetValue<string>("channel",out channel);
        //        channel = result;
        //        //string channel = "Milestones";
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cssAgent"].ToString()))
        //        {
        //            connection.Open();
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("update dbo.feedback set POINST=" + points + " where FEEDBACK='" + result + "'");
        //            // Trace.TraceInformation("IN DB Access" + channel);
        //            String sql = sb.ToString();
        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                //dtResult = new DataTable();
        //                //dtResult.Load(command.ExecuteReader());
        //                command.ExecuteNonQuery();
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}






        public void UpdateData_advise_points(string result, int points)
        {
            try
            {
                //context.ConversationData.TryGetValue<string>("channel",out channel);
                channel = result;
                //string channel = "Milestones";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                var cs = "Server=tcp:idcdbserver.database.windows.net,1433;Initial Catalog=IDCDB;Persist Security Info=False;User ID=idclogin;Password=Idc@login;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update dbo.csspoints set ADVISE_POINTS=" + points + " where PERSON='" + result + "'");
                    //update dbo.csspoints set ADVISE_POINTS = " + points + " where PERSON = 'RAHUL'
                    // Trace.TraceInformation("IN DB Access" + channel);
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //dtResult = new DataTable();
                        //dtResult.Load(command.ExecuteReader());
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


    }

}