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
        public clsItemsLogic ()
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
        #endregion
    }
}
