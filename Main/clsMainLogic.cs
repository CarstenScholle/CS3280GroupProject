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
        public string InvoiceDate;
        public decimal InvoiceTotal;

        /// <summary>
        /// Selected Item for the combobox
        /// </summary>
        public string SelectedItem;
        public decimal SelectedItemCost;
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


        

        /// <summary>
        /// Method that updates the item info for the selected item
        /// </summary>
        public void UpdateItemInfo()
        {
            // Update Invoice Label information
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


        /// <summary>
        /// Method that updates the invoice info for the selected item
        /// </summary>
        public void UpdateInvoiceInfo()
        {

            // Update Invoice Label information
            int iRetVal = 0;
            DataSet ds = da.ExecuteSQLStatement(sql.SelectInvoiceData(InvoiceNumber.ToString()), ref iRetVal);
            DataTable table = ds.Tables[0];

            foreach (DataRow row in table.Rows) // Will only go through 1 row, but this is the only way I know how to access it
            {
                InvoiceNumber = (int)row["InvoiceNum"];
                DateTime d = (DateTime)row["InvoiceDate"];
                InvoiceDate = d.ToString();
                InvoiceTotal = (decimal)row["TotalCost"];
            }

        }

        /// <summary>
        /// Method for adding selected item to the invoice
        /// </summary>
        public void AddItem()
        {
            InvoiceItems.Add(new clsItem(SelectedItemCode, SelectedItem, SelectedItemCost));
        }
        // For some reason, this isn't updating the InvoiceItems


        /// <summary>
        /// Method for deleting selected item in invoice list and DB
        /// </summary>
        /// <param name="name"></param>
        public void DeleteItem(object SI)
        {
            clsItem SelectedItem = (clsItem)SI;
            InvoiceItems.Remove(SelectedItem);

            //Delete Item from Database
            string sSQL = sql.DeleteSelectedItem(InvoiceNumber.ToString(), SelectedItem.ItemCode);
            da.ExecuteNonQuery(sSQL);
            //Getting an error here for some reason.
            //Error says no value is given for required parameters.
            
            
        }
        #endregion



    }
}
