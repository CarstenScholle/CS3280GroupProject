using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data; // For DataSet
using System.Data.OleDb; // For Database Connection
using System.Reflection; // For MethodInfo Class. Discovers the attributes of a method and provides access to method metadata.

namespace Group_Project___Main
{
    internal class clsMainSQL
    {

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Constructor that sets up the connection to the database.
        /// </summary>
        public clsMainSQL()
        {
            sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\Invoice.accdb";
        }

        /// <summary>
        /// Returns the Data Set
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="iRetVal"></param> // ref keyword passes a variable by reference meaning the original value will be changed if it is changed
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataSet GetDataSet(string sSQL, ref int iRetVal)
        {
            try
            {
                DataSet ds = new DataSet();

                // "using" will automatically dispose if there's an error
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        // Connect to the database
                        conn.Open();

                        // Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0; // value of 0 means it takes as long as it needs

                        // Fill up the Data Set with data
                        adapter.Fill(ds);

                    }
                }

                // Set up the number of values to be returned
                iRetVal = ds.Tables[0].Rows.Count;

                return ds;

            } catch (Exception e) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }


        /// <summary>
        /// Takes in a SQL query and returns the first row
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        obj = adapter.SelectCommand.ExecuteScalar();

                    }
                }

                //See if obj is null
                if (obj == null)
                {
                    return "";
                } else
                {
                    return obj.ToString();
                }

            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }


        /// <summary>
        /// Takes an SQL statement that is a non query and executes it
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    iNumRows = cmd.ExecuteNonQuery();

                }

                // returns the number of rows affected
                return iNumRows;

            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }




        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
                return sSQL;
            } catch {
                throw;
            }
            
        }





    }
}
