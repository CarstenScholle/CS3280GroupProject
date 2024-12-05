using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project___Main.Search
{
    internal class clsSearchSQL
    {
        /// <summary>
        /// Returns the SQL string for selecting all invoices.
        /// </summary>
        public static string GetAllInvoices()
        {
            try
            {
                return "SELECT * FROM Invoices";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by InvoiceNum.
        /// </summary>
        public static string GetInvoicesByNumber(string invoiceNum)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by InvoiceDate.
        /// </summary>
        public static string GetInvoicesByDate(string invoiceDate)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE InvoiceDate = #{invoiceDate}#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by TotalCost.
        /// </summary>
        public static string GetInvoicesByCost(string totalCost)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns the SQL string for selecting invoices by InvoiceNum and InvoiceDate.
        /// </summary>
        public static string GetInvoicesByNumberAndDate(string invoiceNum, string invoiceDate)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{invoiceDate}#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by InvoiceNum and TotalCost.
        /// </summary>
        public static string GetInvoicesByNumberAndCost(string totalCost, string invoiceNum)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost} AND InvoiceNum = {invoiceNum}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by TotalCost and InvoiceDate.
        /// </summary>
        public static string GetInvoicesByCostAndDate(string totalCost, string invoiceDate)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost} AND InvoiceDate = #{invoiceDate}#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting invoices by InvoiceNum, InvoiceDate, and TotalCost.
        /// </summary>
        public static string GetInvoicesByNumberDateAndCost(string invoiceNum, string invoiceDate, string totalCost)
        {
            try
            {
                return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{invoiceDate}# AND TotalCost = {totalCost}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting distinct InvoiceNum values.
        /// </summary>
        public static string GetDistinctInvoiceNumbers()
        {
            try
            {
                return "SELECT DISTINCT(InvoiceNum) FROM Invoices ORDER BY InvoiceNum";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting distinct InvoiceDate values.
        /// </summary>
        public static string GetDistinctInvoiceDates()
        {
            try
            {
                return "SELECT DISTINCT(InvoiceDate) FROM Invoices ORDER BY InvoiceDate";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the SQL string for selecting distinct TotalCost values.
        /// </summary>
        public static string GetDistinctTotalCosts()
        {
            try
            {
                return "SELECT DISTINCT(TotalCost) FROM Invoices ORDER BY TotalCost";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}