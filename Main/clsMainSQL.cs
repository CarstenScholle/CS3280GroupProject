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
        /// Constructor method
        /// </summary>
        public clsMainSQL()
        {

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


        public string SelectInvoiceItems(string sInvoiceID)
        {
            try
            {
                string sSql = "SELECT ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems INNER JOIN ItemDesc ON ItemDesc.ItemCode = LineItems.ItemCode WHERE InvoiceNum = " + sInvoiceID;
                return sSql;
            } catch
            {
                throw;
            }
        }


        public string SelectItemData()
        {
            try
            {
                string sSQL = "SELECT ItemDesc FROM ItemDesc;";
                return sSQL;
            } catch
            {
                throw;
            }
        }







    }
}
