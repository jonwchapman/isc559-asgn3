using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class daSQLapp
    {
        //Define default constructor method to initialize properties
        public daSQLapp()
        {
            pTransactionSuccessful = true;
            pJobCnt = 0;
        }

        #region "Properties"

        private bool pTransactionSuccessful;
        public bool TransactionSuccessful
        {
            get { return pTransactionSuccessful; }
        }
        private int pJobCnt;
        public int JobCnt
        {
            get { return pJobCnt; }
        }


        #endregion

        #region "methods"
        public DataTable GetJobList(string ConnectionString)
        {
            DataTable dtJobList = new DataTable();                                          // create datatable object, it is possible to overload and pass it a name as well, ie  Datatable("somename");

            using (SqlConnection JobConnection = new SqlConnection(ConnectionString))       // the "using" statement will dispose of the connection object when done, helps prevent DOS attacks.
            {
                JobConnection.Open();                                                        // open connection! JobConnection.Close(); is not necessary.
                SqlCommand JobCommand = new SqlCommand();                                   // create instance of command object, possible to overload and pass sproc name directly, ie  new SqlCommand("get_JobList");

                JobCommand.Connection = JobConnection;                                      // named connection
                JobCommand.CommandType = CommandType.StoredProcedure;                       // command type
                JobCommand.CommandText = "get_JobList";                                


                using(SqlDataAdapter JobAdapter = new SqlDataAdapter(JobCommand))            // new instance of DataAdapter, passing in JobCommand object. "using" to mitigate DOS attacks
                {

                    try
                    {
                        DataSet JobDS = new DataSet();                                       // create new Dataset.
                        JobAdapter.Fill(JobDS);                                              // fill it with

                        dtJobList = JobDS.Tables[0];                                         // reference first table in JobsDS dataset object, remember DataSet Object Model
                    }

                    catch (SqlException ReadError)
                    {
                        pTransactionSuccessful = false;

                        DataRow ErrorRow = dtJobList.NewRow();
                        dtJobList.Columns.Add("ErrorMessage");
                        ErrorRow["ErrorMessage"] = ReadError.Message.ToString();
                        dtJobList.Rows.Add(ErrorRow);
                    }
                }
            }

            return dtJobList;
        }
        
        
        
        
        
        
        public DataTable GetJob(int JobID, string ConnectionString)
        {
            DataTable dtJob = new DataTable("dtJob");

            using (SqlConnection JobConnection = new SqlConnection(ConnectionString))
            {
                JobConnection.Open();
                /*
                 * Now that we have a connection object open
                 * create a command object
                 * associated it with the connection object (you can have more than one connection)
                 * specifcy the command type
                 * specifcy the command text
                 * Build a DataAdapter: using(){}
                 * Fill the DataSet
                 * Extract the DataTable
                */
                SqlCommand JobCommand = new SqlCommand();

                JobCommand.Connection = JobConnection;
                JobCommand.CommandType = CommandType.StoredProcedure;
                JobCommand.CommandText = "GetJob";
                JobCommand.Parameters.Add("@JobID", SqlDbType.Int).Value = JobID;
                JobCommand.Parameters["@JobID"].Direction = ParameterDirection.Input;


                using (SqlDataAdapter JobAdapter = new SqlDataAdapter(JobCommand))
                {

                    try
                    {
                        DataSet JobDS = new DataSet();
                        JobAdapter.Fill(JobDS);

                        dtJob = JobDS.Tables[0];

                    }
                    //The data.SqlClient class is ening on port 1433
                    //We want to create an excception object that returns
                    //information about an execption
                    catch (SqlException ReadError)
                    {
                        pTransactionSuccessful = false;

                        DataRow ErrorRow = dtJob.NewRow();
                        dtJob.Columns.Add("ErrorMessage");
                        ErrorRow["ErrorMessage"] = ReadError.Message.ToString();
                        dtJob.Rows.Add(ErrorRow);
                    }
                }
            }

            return dtJob;
        }
        public DataTable GetJobCnt(int JobID, string ConnectionString)
        {
            DataTable dtJob = new DataTable("dtJob");
            /*
             * SQL Server  has a connection pool. If a connection object is not
             * closed, it can create a DOS attack because the connection pool
             * will be exhausted. The using(){} statement will dispose of the 
             * connection object when done.
             */


            using (SqlConnection JobConnection = new SqlConnection(ConnectionString))
            {
                JobConnection.Open();
                /*
                 * Now that we have a connection object open
                 * create a command object
                 * associated it with the connection object (you can have more than one connection)
                 * specifcy the command type
                 * specifcy the command text
                 * Build a DataAdapter: using(){}
                 * Fill the DataSet
                 * Extract the DataTable
                */
                SqlCommand JobCommand = new SqlCommand();

                JobCommand.Connection = JobConnection;
                JobCommand.CommandType = CommandType.StoredProcedure;
                JobCommand.CommandText = "GetJobCnt";
                JobCommand.Parameters.Add("@JobID", SqlDbType.Int).Value = JobID;
                JobCommand.Parameters["@JobID"].Direction = ParameterDirection.Input;
                JobCommand.Parameters.Add("@JobCnt", SqlDbType.Int).Value = JobID;
                JobCommand.Parameters["@JobCnt"].Direction = ParameterDirection.Output;


                using (SqlDataAdapter JobAdapter = new SqlDataAdapter(JobCommand))
                {

                    try {
                        DataSet JobDS = new DataSet();
                        JobAdapter.Fill(JobDS);

                        dtJob = JobDS.Tables[0];

                        pJobCnt = Convert.ToInt16(JobCommand.Parameters["@JobCnt"].Value);
                    }

                    catch (SqlException ReadError)
                    {
                        pTransactionSuccessful = false;

                        DataRow ErrorRow = dtJob.NewRow();
                        dtJob.Columns.Add("ErrorMessage");
                        ErrorRow["ErrorMessage"] = ReadError.Message.ToString();
                        dtJob.Rows.Add(ErrorRow);
                    }
                }
            }

            return dtJob;
        }

        #endregion
    }
}
