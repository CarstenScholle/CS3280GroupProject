using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project___Main.Items
{
    /// <summary>
    /// Class provides Methods for mapping a database operation to the corresponding SQL statement.
    /// </summary>
    public class clsItemsSQL
    {
        #region Methods
        /// <summary>
        /// Select all Items that are currently in the ItemDesc table.
        /// </summary>
        /// <returns> SQL statement for selecting all items from the ItemDesc Table. </returns>
        public string SelectAllItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        /// <summary>
        /// Select the Invoice number for which the provided item is assigned to.
        /// </summary>
        /// <param name="sItemCode"> The Item code. </param>
        /// <returns> SQL statement for selecting the Invoice number from the LineItems Table. </returns>
        public string SelectInvoiceNumForItem(string sItemCode)
        {
            return $"SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = '{sItemCode}'";
        }

        /// <summary>
        /// Updates the attributes of an existing item.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The new item description. </param>
        /// <param name="dItemCost"> The new item cost. </param>
        /// <returns> SQL statement for updating an item from the ItemDesc Table. </returns>
        public string UpdateItem(string sItemCode, string sItemDesc, decimal dItemCost)
        {
            return $"UPDATE ItemDesc SET ItemDesc = '{sItemDesc}', Cost = {dItemCost} WHERE ItemCode = '{sItemCode}'";
        }

        /// <summary>
        /// Add a new item to the table.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The item description. </param>
        /// <param name="dItemCost"> The cost of the item. </param>
        /// <returns> SQL statement for adding an item to the ItemDesc Table. </returns>
        public string AddItem(string sItemCode, string sItemDesc, decimal dItemCost)
        {
            return $"Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('{sItemCode}', '{sItemDesc}', {dItemCost})";
        }

        /// <summary>
        /// Remove an existing item from the table.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <returns> SQL statement for removing an item from the ItemDesc Table. </returns>
        public string DeleteItem(string sItemCode)
        {
            return $"Delete from ItemDesc Where ItemCode = '{sItemCode}'";
        }
        #endregion
    }
}
