using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using Assignment6AirlineReservation;
using Group_Project___Main.common;

namespace Group_Project___Main.Items
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
        private bool bWasItemModified = false;

        /// <summary>
        /// Tells you if the user is currently modifying an item.
        /// </summary>
        private bool bIsEditing = false;

        /// <summary>
        /// Tells you if the user is currently adding an item.
        /// </summary>
        private bool bIsAdding = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gives you a list of all the items that are currently loaded in the program. @Peter: This could be used to display the items in a Datagrid
        /// </summary>
        public List<clsItem> Items { get { return itemLogic.Items; } }

        /// <summary>
        /// Tells you if the user has modified any of the items (Deleting, Updating or Adding an item). @Peter: You can check this bool to know if the main window should update anything.
        /// </summary>
        public bool WasItemModified { get { return bWasItemModified; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the item window.
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();

                // Initialize the item logic and load data from database
                itemLogic = new clsItemsLogic();

                // Bind data to the Datagrid
                dgItems.ItemsSource = Items;

                resetGui();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method should be called when the main window wants to open the item window. Will reset the
        /// GUI elements and internal variables, before calling the ShowDialog Method.
        /// </summary>
        public void openWindow()
        {
            try
            {
                bWasItemModified = false;
                resetGui();
                ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resets the gui of the item window to its initial state.
        /// </summary>
        private void resetGui()
        {
            try
            {
                itemLogic.loadItemsFromDatabase();

                bWasItemModified = false;
                bIsEditing = false;
                bIsAdding = false;
                lblErrorMessage.Visibility = Visibility.Collapsed;
                setItemInputEnabledDisabled(false);
                dgItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user selects an item from the List View. Updates the displayed data in the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Reset the error message
                lblErrorMessage.Visibility = Visibility.Collapsed;
                // If no item is selected, clean the text-input
                if (dgItems.SelectedItem == null)
                {
                    clearItemInput();
                    return;
                }
                clsItem selectedItem = (clsItem)dgItems.SelectedItem;
                txtItemCode.Text = selectedItem.ItemCode;
                txtItemCost.Text = selectedItem.ItemCost.ToString();
                txtItemDescription.Text = selectedItem.ItemDesc;
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Removes all input from the textinput elements.
        /// </summary>
        private void clearItemInput()
        {
            try
            {
                txtItemCode.Text = string.Empty;
                txtItemCost.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user clickes on the "add item" button. Enables the textinputs for the user, so he
        /// can input a new item.
        /// </summary>
        /// <param name="sender"> The button that was clicked. </param>
        /// <param name="e"> The event arguments. </param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblErrorMessage.Visibility = Visibility.Collapsed;
                bIsAdding = true;
                dgItems.SelectedItem = null;
                setItemInputEnabledDisabled(true);
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Activates or deactivates the gui elements that are used for modifying the items.
        /// </summary>
        /// <param name="isEnabled"> If true, the elements for modifying items are enabled, otherwise they are disabled. </param>
        private void setItemInputEnabledDisabled(bool isEnabled)
        {
            try
            {
                if (isEnabled)
                    spSaveCancelChanges.Visibility = Visibility.Visible;
                else
                    spSaveCancelChanges.Visibility = Visibility.Collapsed;
                if (bIsAdding)
                    txtItemCode.IsEnabled = isEnabled;
                else
                    txtItemCode.IsEnabled = false;
                txtItemCost.IsEnabled = isEnabled;
                txtItemDescription.IsEnabled = isEnabled;
                dgItems.IsEnabled = !isEnabled;
                btnDeleteItem.IsEnabled = !isEnabled;
                btnAddItem.IsEnabled = !isEnabled;
                btnEditItem.IsEnabled = !isEnabled;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user clickes the "cancel" button. Cancels the user from modifying the items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblErrorMessage.Visibility = Visibility.Collapsed;
                if (bIsAdding)
                {
                    clearItemInput();
                    bIsAdding = false;
                }
                else
                {
                    bIsEditing = false;
                }

                setItemInputEnabledDisabled(false);
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user clickes the "edit item" button. Enables the user input for editing an item.
        /// </summary>
        /// <param name="sender"> The button that was clicked. </param>
        /// <param name="e"> The event arguments. </param>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblErrorMessage.Visibility = Visibility.Collapsed;
                if (dgItems.SelectedItem == null)
                {
                    lblErrorMessage.Content = "Select an item from the listed item first, before editing it!";
                    lblErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                setItemInputEnabledDisabled(true);
                bIsEditing = true;
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user clicks on the "delete item" button. Deletes an item if it is not part of an invoice.
        /// </summary>
        /// <param name="sender"> The clicked button. </param>
        /// <param name="e"> The event arguments. </param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblErrorMessage.Visibility = Visibility.Collapsed;
                // Check if an item was selected
                if (dgItems.SelectedItem == null)
                {
                    lblErrorMessage.Content = "Select an item from the listed item first, before deletíng it!";
                    lblErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                // Check if the item code is valid:
                if (!itemLogic.doesItemCodeExist(txtItemCode.Text))
                {
                    lblErrorMessage.Visibility = Visibility.Visible;
                    lblErrorMessage.Content = "The provided item code does not exist!";
                    return;
                }
                // Check if the item is part of an invoice before deleting it
                DataSet dsInvoices = itemLogic.getItemInvoices(txtItemCode.Text);
                if (dsInvoices.Tables[0].Rows.Count != 0)
                {
                    string errorMsg = "Cannot delete item, since it's part of the following invoices: ";
                    foreach (DataRow dr in dsInvoices.Tables[0].Rows)
                    {
                        // Read row
                        int iInvoiceNumber = (int)dr["InvoiceNum"];
                        errorMsg += iInvoiceNumber.ToString() + " ";
                    }
                    lblErrorMessage.Visibility = Visibility.Visible;
                    lblErrorMessage.Content = errorMsg;
                    return;
                }

                itemLogic.deleteItemFromDatabase(txtItemCode.Text);
                resetGui();
                bWasItemModified = true;
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Is called when the user clickes the "save changes" button. Validates the user input first and then modifies the database based on the changes.
        /// </summary>
        /// <param name="sender"> The clicked button. </param>
        /// <param name="e"> The event arguments. </param>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Validate the input
                if (txtItemCode.Text == string.Empty)
                {
                    lblErrorMessage.Visibility = Visibility.Visible;
                    lblErrorMessage.Content = "Please provide an item code!";
                    return;
                }

                bool doesItemExist = itemLogic.doesItemCodeExist(txtItemCode.Text);

                // Check if the item code already exists
                if (bIsAdding)
                {
                    if (doesItemExist)
                    {
                        lblErrorMessage.Visibility = Visibility.Visible;
                        lblErrorMessage.Content = "The provided item code already exist!";
                        return;
                    }
                }
                else if (bIsEditing)
                {
                    if (!doesItemExist)
                    {
                        lblErrorMessage.Visibility = Visibility.Visible;
                        lblErrorMessage.Content = "The provided item code does not exist!";
                        return;
                    }
                }

                // Check if the costs are a decimal
                decimal costs;
                if (!decimal.TryParse(txtItemCost.Text, out costs))
                {
                    lblErrorMessage.Visibility = Visibility.Visible;
                    lblErrorMessage.Content = "The provided costs of the item are not a valid number!";
                    return;
                }

                // 2. Store changes in the database
                if (bIsAdding)
                    itemLogic.addItemToDatabase(txtItemCode.Text, txtItemDescription.Text, costs);
                else if (bIsEditing)
                    itemLogic.editItemInDatabase(txtItemCode.Text, txtItemDescription.Text, costs);

                // 3. Reset the gui
                resetGui();
                dgItems.SelectedItem = itemLogic.getItemByCode(txtItemCode.Text);

                // 4. Tell the main window that an item was modified
                bWasItemModified = true;
            }
            catch (Exception ex)
            {
                clsExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion
    }
}
