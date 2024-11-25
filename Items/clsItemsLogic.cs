using Group_Project___Main.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project___Main.Items
{
    /// <summary>
    /// Class contains methods and attibutes for managing all items from the application.
    /// </summary>
    public class clsItemsLogic
    {
        #region Attributes
        private List<clsItem> lItems;
        private clsDataAccess dataAccess;
        private clsItemsSQL itemsSQL;
        #endregion

        #region Properties
        /// <summary>
        /// Gives you a list of all the items that are currently loaded in the program
        /// </summary>
        public List<clsItem> Items { get { return lItems; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new ItemsLogic instance. 
        /// </summary>
        public clsItemsLogic()
        {
            lItems = new List<clsItem>();
            dataAccess = new clsDataAccess();
            itemsSQL = new clsItemsSQL();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads all items from the database and fills the list "Items" with those items. All items that were already inside the list will be
        /// overwritten by the newly loaded items.
        /// </summary>
        public void loadItemsFromDatabase()
        {
            lItems.Clear();
            int iRetVal = 0;
            DataSet dsItems = dataAccess.ExecuteSQLStatement(itemsSQL.SelectAllItems(), ref iRetVal);
            foreach (DataRow dr in dsItems.Tables[0].Rows)
            {
                // Read row
                object test = dr["ItemCode"];
                string sItemCode = (string)dr["ItemCode"];
                string sItemDesc = (string)dr["ItemDesc"];
                decimal dCost = (decimal)dr["Cost"];

                // Create a new item and add it to the list
                lItems.Add(new clsItem(sItemCode, sItemDesc, dCost));
            }
        }

        /// <summary>
        /// Adds a new item to the database.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The item description. </param>
        /// <param name="dCost"> The item costs. </param>
        public void addItemToDatabase(string sItemCode, string sItemDesc, decimal dCost)
        {
            dataAccess.ExecuteNonQuery(itemsSQL.AddItem(sItemCode, sItemDesc, dCost));
        }

        /// <summary>
        /// Tells you if an item code is already used for an existing item.
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns> True if an item with the given code already exists, false otherwise.</returns>
        public bool doesItemCodeExist(string sItemCode)
        {
            foreach (clsItem item in lItems)
            {
                if (item.ItemCode == sItemCode)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Modifies an existing item and saves changes into the database.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The item description. </param>
        /// <param name="dCost"> The item costs. </param>
        public void editItemInDatabase(string sItemCode, string sItemDesc, decimal dCost)
        {
            dataAccess.ExecuteNonQuery(itemsSQL.UpdateItem(sItemCode, sItemDesc, dCost));
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="sItemCode"> The item code of the item. </param>
        public void deleteItemFromDatabase(string sItemCode)
        {
            dataAccess.ExecuteNonQuery(itemsSQL.DeleteItem(sItemCode));
        }

        public DataSet getItemInvoices(string sItemCode)
        {
            int iRetVal = 0;
            return dataAccess.ExecuteSQLStatement(itemsSQL.SelectInvoiceNumForItem(sItemCode), ref iRetVal);
        }

        /// <summary>
        /// Tries to find the requested item based by its code. If no item with the code was found, null is returned.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <returns> The item that contains the code or null. </returns>
        public clsItem getItemByCode(string sItemCode)
        {
            foreach (clsItem item in lItems)
            {
                if (item.ItemCode.Equals(sItemCode))
                    return item;
            }
            return null;
        }
        #endregion
    }
}
