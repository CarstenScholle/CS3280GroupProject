using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Class provides Methods for mapping a database operation to the corresponding SQL statement.
    /// </summary>
    class clsItemsSQL
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
        /// <param name="cItemCode"> The Item code. </param>
        /// <returns> SQL statement for selecting the Invoice number from the LineItems Table. </returns>
        public string SelectInvoiceNumForItem(string cItemCode)
        {
            return $"SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = '{cItemCode}'";
        }

        /// <summary>
        /// Updates the attributes of an existing item.
        /// </summary>
        /// <param name="cItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The new item description. </param>
        /// <param name="dItemCost"> The new item cost. </param>
        /// <returns> SQL statement for updating an item from the ItemDesc Table. </returns>
        public string UpdateItem(string cItemCode, string sItemDesc, decimal dItemCost)
        {
            return $"UPDATE ItemDesc SET ItemDesc = '{sItemDesc}', Cost = {dItemCost} WHERE ItemCode = '{cItemCode}'";
        }

        /// <summary>
        /// Add a new item to the table.
        /// </summary>
        /// <param name="cItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The item description. </param>
        /// <param name="dItemCost"> The cost of the item. </param>
        /// <returns> SQL statement for adding an item to the ItemDesc Table. </returns>
        public string AddItem(string cItemCode, string sItemDesc, decimal dItemCost)
        {
            return $"Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('{cItemCode}', '{sItemDesc}', {dItemCost})";
        }

        /// <summary>
        /// Remove an existing item from the table.
        /// </summary>
        /// <param name="cItemCode"> The item code. </param>
        /// <returns> SQL statement for removing an item from the ItemDesc Table. </returns>
        public string DeleteItem(string cItemCode)
        {
            return $"Delete from ItemDesc Where ItemCode = '{cItemCode}'";
        }
        #endregion
    }
}
