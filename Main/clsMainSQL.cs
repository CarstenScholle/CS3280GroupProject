﻿using System;
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

        public string SelectInvoiceItemData()
        {
            try
            {
                string sSQL = "SELECT ItemDesc FROM ItemDesc;";
                return sSQL;
            }
            catch
            {
                throw;
            }
        }


        public string SelectItemData(string sItemName)
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc WHERE ItemDesc = '" + sItemName + "';";
                return sSQL;
            } catch
            {
                throw;
            }
        }

        public string DeleteSelectedItem(string sInvoiceID, string sItemCode)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + sInvoiceID + " AND ItemCode = '" + sItemCode + "';";
                return sSQL;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateInvoiceCost(string sInvoiceID, decimal totalCost)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + sInvoiceID;
                return sSQL;
            }
            catch
            {
                throw;
            }
        }





        public string InsertItem()
        {
            try
            {
                //string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" + ;
                return sSQL;
            }
            catch
            {
                throw;
            }
        }

        public string InsertInvoice(string sInvoiceID, decimal totalCost)
        {
            try
            {
                //string sSQL = "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + sInvoiceID;
                return sSQL;
            }
            catch
            {
                throw;
            }
        }








    }
}
