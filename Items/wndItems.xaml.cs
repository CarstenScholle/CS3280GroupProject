using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Group_Project___Main.common;

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Attributes
        /// <summary>
        /// Contains methods and attributes for managing the items of the program
        /// </summary>
        private clsItemsLogic itemLogic;

        /// <summary>
        /// Tells you if the user has modified any of the items (Deleting, Updating or Adding an item).
        /// </summary>
        private bool bWasItemModified;
        #endregion

        #region Properties
        /// <summary>
        /// Gives you a list of all the items that are currently loaded in the program. @Peter: This could be used to display the items in a Datagrid
        /// </summary>
        public List<clsItem> Items { get { return itemLogic.Items; } }

        /// <summary>
        /// Tells you if the user has modified any of the items (Deleting, Updating or Adding an item). @Peter: You can check this bool to know if the main window should update anything.
        /// </summary>
        public bool WasItemModified { get {  return bWasItemModified; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the item window.
        /// </summary>
        public wndItems()
        {
            InitializeComponent();

            // Initialize the item logic and load data from database
            itemLogic = new clsItemsLogic();
            itemLogic.loadItemsFromDatabase();

            // Bind data to the Datagrid
            dgItems.ItemsSource = Items;
            bWasItemModified = false;
        }
        #endregion

        #region Methods

        #endregion
    }
}
