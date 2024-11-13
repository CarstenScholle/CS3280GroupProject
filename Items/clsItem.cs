using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Dataclass that stores data for an item.
    /// </summary>
    public class clsItem
    {
        #region Properties
        /// <summary>
        /// Unique item code for identifying an item.
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// The cost of the item.
        /// </summary>
        public decimal ItemCost { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for an item. Creates a new item with the provided features.
        /// </summary>
        /// <param name="sItemCode"> The item code. </param>
        /// <param name="sItemDesc"> The item description. </param>
        /// <param name="dItemCost"> The cost of the item. </param>
        public clsItem(string sItemCode, string sItemDesc, decimal dItemCost) 
        {
            ItemCode = sItemCode;
            ItemDesc = sItemDesc;
            ItemCost = dItemCost;
        }
        #endregion
    }
}
