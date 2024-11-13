using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Group_Project___Main
{
    // Maybe try inheritance? clsMainLogic : clsMainSQL?
    // TODO: Figure out how to extract all the items from the database Invoice and add them to the items list
    // Do the same for the invoice data itself

    // - Main window needs comments to talk about how it will interact with the other windows to get the selected invoice or know when to refresh the items.

    // - Main window, when the Item's Window is closed needs to have comments talking about refreshing the combo boxes with the items in it, in case they got updated.


    internal class clsMainLogic
    {
        /// <summary>
        /// Data base
        /// </summary>
        public clsMainSQL db = new clsMainSQL();

        /// <summary>
        /// List of items
        /// </summary>
        public List<string> itemList = new List<string>();


        /// <summary>
        /// clsMainLogic Constructor
        /// </summary>
        public clsMainLogic()
        {

        }


        /// <summary>
        /// Method that fills the data for both item list and invoice datagrid
        /// </summary>
        public void FillData()
        {
            int iRetVal = 0;
            DataSet ds = db.GetDataSet("SELECT ItemDesc FROM ItemDesc;", ref iRetVal);

            DataTable table = ds.Tables[0];

            // Fills the Items List
            foreach (DataRow row in table.Rows)
            {
                itemList.Add(row["ItemDesc"].ToString());
            }

            // Fills the Invoice Data Grid

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




    }
}
