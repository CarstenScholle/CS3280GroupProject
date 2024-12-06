using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Group_Project___Main.common;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Reflection;

namespace Group_Project___Main
{
    // Maybe try inheritance? clsMainLogic : clsMainSQL?
    // TODO: Figure out how to extract all the items from the database Invoice and add them to the items list
    // Do the same for the invoice data itself

    // - Main window needs comments to talk about how it will interact with the other windows to get the selected invoice or know when to refresh the items.

    // - Main window, when the Item's Window is closed needs to have comments talking about refreshing the combo boxes with the items in it, in case they got updated.


    internal class clsMainLogic
    {


        #region Attributes

        /// <summary>
        /// Data base
        /// </summary>
        public clsMainSQL sql = new clsMainSQL();

        
        /// <summary>
        /// Data Access Class
        /// </summary>
        public clsDataAccess da = new clsDataAccess();


        /// <summary>
        /// Invoice number, Invoice date, Invoice total
        /// </summary>
        public int InvoiceNumber = 5000;

        /// <summary>
        /// Invoice date
        /// </summary>
        public string InvoiceDate;

        /// <summary>
        /// Invoice total
        /// </summary>
        public decimal InvoiceTotal;

        /// <summary>
        /// Selected Item for the combobox
        /// </summary>
        public string SelectedItem;

        /// <summary>
        /// Selected Item cost
        /// </summary>
        public decimal SelectedItemCost;

        /// <summary>
        /// Selecte Item code
        /// </summary>
        public string SelectedItemCode;


        /// <summary>
        /// List of items for the ComboBox
        /// </summary>
        public List<string> ItemList = new List<string>();


        /// <summary>
        /// List of items for the invoice
        /// </summary>
        public List<clsItem> InvoiceItems = new List<clsItem>();


        #endregion



        #region Properties
        #endregion


        #region Constructor
        /// <summary>
        /// clsMainLogic Constructor
        /// </summary>
        public clsMainLogic()
        {

        }
        #endregion


        #region Methods

        /// <summary>
        /// Method that fills the data for both item list and invoice datagrid
        /// </summary>
        public void FillData()
        {
            try {
                if (ItemList.Count > 0)
                {
                    ItemList.Clear();
                }

                // Fills the Items List
                int iRetVal = 0;
                DataSet ds = da.ExecuteSQLStatement(sql.SelectInvoiceItemData(), ref iRetVal);
                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    ItemList.Add(row["ItemDesc"].ToString());
                }



                if (InvoiceItems.Count > 0)
                {
                    InvoiceItems.Clear();
                }

                // Fills the Invoice Data Grid with default value
                ds = da.ExecuteSQLStatement(sql.SelectInvoiceItems(InvoiceNumber.ToString()), ref iRetVal);
                table = ds.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    string code = (string)row["ItemCode"];
                    string desc = (string)row["ItemDesc"];
                    decimal cost = (decimal)row["Cost"];

                    InvoiceItems.Add(new clsItem(code, desc, cost));
                }


                UpdateInvoiceInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        

        /// <summary>
        /// Method that updates the item info for the selected item
        /// </summary>
        public void UpdateItemInfo()
        {
            try {
                // Updates Invoice Label information
                int iRetVal = 0;
                DataSet ds = da.ExecuteSQLStatement(sql.SelectItemData(SelectedItem), ref iRetVal);
                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    SelectedItem = (string)row["ItemDesc"];
                    SelectedItemCost = (decimal)row["Cost"];
                    SelectedItemCode = (string)row["ItemCode"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Method that updates the invoice info for the selected item
        /// </summary>
        public void UpdateInvoiceInfo()
        {
            try {
                // Update Invoice Label information
                int iRetVal = 0;
                DataSet ds = da.ExecuteSQLStatement(sql.SelectInvoiceData(InvoiceNumber.ToString()), ref iRetVal);
                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows) // Will only go through 1 row, but this is the only way I know how to access it rn
                {
                    InvoiceNumber = (int)row["InvoiceNum"];
                    DateTime d = (DateTime)row["InvoiceDate"];
                    InvoiceDate = d.ToString();
                    InvoiceTotal = (decimal)row["TotalCost"];
                }

                // Double check that invoice total is correct
                decimal total = 0;
                foreach (var item in InvoiceItems)
                {
                    total += item.ItemCost;
                }
                if(InvoiceTotal != total)
                {
                    InvoiceTotal = total;
                    da.ExecuteNonQuery(sql.UpdateInvoiceCost(InvoiceNumber.ToString(), InvoiceTotal));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Method for adding selected item to the invoice
        /// </summary>
        public void AddItem()
        {
            try {
                InvoiceItems.Add(new clsItem(SelectedItemCode, SelectedItem, SelectedItemCost));

                InvoiceTotal += SelectedItemCost;


                da.ExecuteNonQuery(sql.UpdateInvoiceCost(InvoiceNumber.ToString(), InvoiceTotal));


                // An inelegant way of adding 1 to the the line number
                int iRetVal = 0;
                string lineNum = sql.GetLineItemNum(InvoiceNumber.ToString());
                DataSet ds = da.ExecuteSQLStatement(lineNum, ref iRetVal);
                DataTable dt = ds.Tables[0];
                int lineNumInt;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lineNumInt = (int)dr["LineItemNum"] + 1;
                } else
                {
                    lineNumInt = 1;
                }

                /*
                int lineNumInt = int.Parse(lineNum);
                lineNumInt++;
                lineNum = lineNumInt.ToString();
                */

                // Insert new item into the database
                string sSQL = sql.InsertItem(InvoiceNumber.ToString(), lineNumInt.ToString(), SelectedItemCode);
                da.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        


        /// <summary>
        /// Method for deleting selected item in invoice list and DB
        /// </summary>
        /// <param name="name"></param>
        public void DeleteItem(object SI)
        {
            try {

                clsItem SelectedItem = (clsItem)SI;
                decimal itemCost = SelectedItem.ItemCost;
                InvoiceTotal -= itemCost;

                InvoiceItems.Remove(SelectedItem);

                da.ExecuteNonQuery(sql.UpdateInvoiceCost(InvoiceNumber.ToString(), InvoiceTotal));

                //Delete Item from Database
                string sSQL = sql.DeleteSelectedItem(InvoiceNumber.ToString(), SelectedItem.ItemCode);
                da.ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method for creating a new invoice
        /// </summary>
        public void CreateInvoice()
        {
            try {
                // An inelegant way of adding 1 to the the highest invoice number
                int iRetVal = 0;
                string lineNum = sql.GetInvoiceNum();
                DataSet ds = da.ExecuteSQLStatement(lineNum, ref iRetVal);
                DataTable dt = ds.Tables[0];
            

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    InvoiceNumber = (int)dr["InvoiceNum"] + 1;
                }
                else
                {
                    InvoiceNumber = 5000; // default lowest invoice number
                }

                InvoiceDate = DateTime.Today.ToString();
                InvoiceTotal = 0;

                da.ExecuteNonQuery(sql.InsertInvoice(InvoiceNumber.ToString(), DateTime.Today.ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        #endregion



    }
}
