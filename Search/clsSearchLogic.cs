using Group_Project___Main.common;
using Group_Project___Main.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project___Main.Search
{
    internal class clsSearchLogic
    {
        //Creates new instance of the data access class
        clsDataAccess accessData = new clsDataAccess();
        //Stores the data returned from the data access class
        DataSet storeData = new DataSet();

        //Limit on returned rows
        private int iRet = 0;

        /// <summary>
        /// Returns the invoiceNum column as a list of strings for combo box display
        /// </summary>
        public List<string> GetDistinctInvoiceNumbers()
        {
            try
            {
                //Instantiates the list of strings to be returned by this class' methods
                List<string> lstDataReturn = new List<string>();

                // Collect table data
                storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceNumbers(), ref iRet);

                // Fill the list with invoice numbers from the dataset
                foreach (DataRow row in storeData.Tables[0].Rows)
                {
                    lstDataReturn.Add($"#{row[0].ToString()}");
                    //lstDataReturn.Add(row[0].ToString());
                }

                return lstDataReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the InvoiceDate column as a list of strings for combo box display
        /// </summary>
        public List<string> GetDistinctInvoiceDates()
        {
            try
            {
                //Instantiates the list of strings to be returned by this class' methods
                List<string> lstDataReturn = new List<string>();

                // Clear the list to ensure no leftover data
                lstDataReturn.Clear();

                // Collect table data
                storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceDates(), ref iRet);

                // Fill the list with InvoiceDate values from the dataset
                foreach (DataRow row in storeData.Tables[0].Rows)
                {
                    lstDataReturn.Add(DateTime.Parse(row[0].ToString()).ToShortDateString());
                    //lstDataReturn.Add(row[0].ToString());
                }

                return lstDataReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the totalCost collumn as a list of strings for combo box display
        /// </summary>
        public List<string> GetDistinctInvoiceCosts()
        {
            try
            {
                //Instantiates the list of strings to be returned by this class' methods
                List<string> lstDataReturn = new List<string>();

                // Collect table data
                storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetDistinctTotalCosts(), ref iRet);

                // Fill the list with invoice numbers from the dataset
                foreach (DataRow row in storeData.Tables[0].Rows)
                {
                    lstDataReturn.Add($"${row[0].ToString()}.00");
                    //lstDataReturn.Add(row[0].ToString());
                }

                return lstDataReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the invoiceNum collumn as a list of strings for combo box display
        /// </summary>
        public List<clsItem> GetInvoices(string InvoiceNumber, string InvoiceDate, string TotalCost)
        {
            try
            {
                // Instantiates the list of invoices to be displayed in the data grid
                List<clsItem> invoiceTable = new List<clsItem>();

                // Determine the appropriate SQL query based on the provided filters
                //None are null
                if (!string.IsNullOrEmpty(InvoiceNumber) && !string.IsNullOrEmpty(InvoiceDate) && !string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumberDateAndCost(InvoiceNumber, InvoiceDate, TotalCost), ref iRet);
                }
                //Total cost is null
                else if (!string.IsNullOrEmpty(InvoiceNumber) && !string.IsNullOrEmpty(InvoiceDate) && string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumberAndDate(InvoiceNumber, InvoiceDate), ref iRet);
                }
                //Invoice date is null
                else if (!string.IsNullOrEmpty(InvoiceNumber) && string.IsNullOrEmpty(InvoiceDate) && !string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumberAndCost(InvoiceNumber, TotalCost), ref iRet);
                }
                //Invoice number is null
                else if (string.IsNullOrEmpty(InvoiceNumber) && !string.IsNullOrEmpty(InvoiceDate) && !string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByCostAndDate(TotalCost, InvoiceDate), ref iRet);
                }
                //Only Invoice number is not null
                else if (!string.IsNullOrEmpty(InvoiceNumber) && string.IsNullOrEmpty(InvoiceDate) && string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumber(InvoiceNumber), ref iRet);
                }
                //Only Invoice date is not null
                else if (string.IsNullOrEmpty(InvoiceNumber) && !string.IsNullOrEmpty(InvoiceDate) && string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByDate(InvoiceDate), ref iRet);
                }
                //Only Total cost is not null
                else if (string.IsNullOrEmpty(InvoiceNumber) && string.IsNullOrEmpty(InvoiceDate) && !string.IsNullOrEmpty(TotalCost))
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByCost(TotalCost), ref iRet);
                }
                // All are null
                else
                {
                    storeData = accessData.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices(), ref iRet);
                }

                // Check if storeData contains rows
                if (storeData.Tables.Count == 0 || storeData.Tables[0].Rows.Count == 0)
                {
                    // Return an empty table if nothing is found
                    return invoiceTable;
                }

                // Fill the invoiceTable list with data from storeData
                foreach (DataRow row in storeData.Tables[0].Rows)
                {
                    decimal totalCost = (decimal)row["TotalCost"];
                    clsItem item = new clsItem(
                        row["InvoiceNum"].ToString(),
                        DateTime.Parse(row["InvoiceDate"].ToString()).ToShortDateString(),
                        totalCost);
                    invoiceTable.Add(item);
                }

                // Return the filled invoice table
                return invoiceTable;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}