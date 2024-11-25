using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Group_Project___Main.common;

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
        /// Invoice number
        /// </summary>
        public int InvoiceNumber;


        /// <summary>
        /// List of items for the ComboBox
        /// </summary>
        public List<string> ItemList = new List<string>();


        /// <summary>
        /// List of items for the invoice
        /// </summary>
        public List<clsItem> InvoiceItems = new List<clsItem>();


        /// <summary>
        /// Default invoice number
        /// </summary>
        private int defaultInvoice = 5000;

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



            // Fills the Items List
            int iRetVal = 0;
            DataSet ds = da.ExecuteSQLStatement(sql.SelectItemData(), ref iRetVal);
            DataTable table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                ItemList.Add(row["ItemDesc"].ToString());
            }


            // Fills the Invoice Data Grid with default value
            ds = da.ExecuteSQLStatement(sql.SelectInvoiceItems(defaultInvoice.ToString()), ref iRetVal);
            table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                string code = (string)row["ItemCode"];
                string desc = (string)row["ItemDesc"];
                decimal cost = (decimal)row["Cost"];

                InvoiceItems.Add(new clsItem(code, desc, cost));
            }

            InvoiceNumber = defaultInvoice;

        }


        // Should this be done using data binding instead?

        /// <summary>
        /// Method that updates the item info for the selected item
        /// </summary>
        public void UpdateItemInfo()
        {

        }


        /// <summary>
        /// Method that updates the invoice info for the selected item
        /// </summary>
        public void UpdateInvoiceInfo()
        {

        }
        #endregion



    }
}
