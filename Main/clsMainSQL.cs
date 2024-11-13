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





    }
}
