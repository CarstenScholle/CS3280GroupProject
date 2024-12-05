using Group_Project___Main.common;
using Group_Project___Main.Search;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace Group_Project___Main
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        // Holds invoice ID if the user selected an invoice, and holds 0 if not
        string sSelectedInvoiceID = "0";
        // Creates instance of the search logic class
        clsSearchLogic searchLogic = new clsSearchLogic();

        public wndSearch()
        {
            try
            {
                InitializeComponent();
                //sets options for invoice number combo box
                cbSearchInvoiceNumber.ItemsSource = searchLogic.GetDistinctInvoiceNumbers();
                //sets options for invoice date combo box
                cbSearchInvoiceDate.ItemsSource = searchLogic.GetDistinctInvoiceDates();
                //sets options for total cost combo box
                cbSearchTotalCost.ItemsSource = searchLogic.GetDistinctInvoiceCosts();

                //sets default datagrid display
                dgSearchData.ItemsSource = searchLogic.GetInvoices(null, null, null);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Return to main window without change when cancel button is clicked
        /// </summary>
        private void bSearchCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //indicates no selection chosen
                sSelectedInvoiceID = "0";
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Return to main window with selected invoice [Main Window Interaction]
        /// </summary>
        private void bSearchSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Clear the filters set in the combo boxes and reset the table to a default state
        /// </summary>
        private void bSearchClearFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbSearchInvoiceNumber.SelectedItem = null;
                cbSearchInvoiceDate.SelectedItem = null;
                cbSearchTotalCost.SelectedItem = null;
                dgSearchData.ItemsSource = searchLogic.GetInvoices(null, null, null);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Have search logic return an approprate data list for selected object and apply it
        /// </summary>
        private void gbFilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //collect selected items from comboboxes
                string invoiceNumber = cbSearchInvoiceNumber.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    invoiceNumber = invoiceNumber.Substring(1); //removes formatting
                }
                string invoiceDate = cbSearchInvoiceDate.SelectedItem?.ToString();
                string totalCost = cbSearchTotalCost.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(totalCost))
                {
                    totalCost = totalCost.Substring(1, totalCost.Length - 4); //removes formatting
                }

                dgSearchData.ItemsSource = searchLogic.GetInvoices(invoiceNumber, invoiceDate, totalCost);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When a row is selected on the datagrid store that invoices ID
        /// </summary>
        private void dgSearchData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Ensure a row is selected
                var selectedRow = dgSearchData.SelectedItem;

                if (selectedRow != null)
                {
                    // Cast the selected row to the type of your data (e.g., clsItem)
                    var selectedData = (clsItem)selectedRow;

                    sSelectedInvoiceID = selectedData.ItemCode;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// Property the main window can use to get the selected invoice ID [Call immediately after returning from search screen]
        /// </summary>
        public string SelectedInvoiceID()
        {
            return sSelectedInvoiceID;
        }



        /// <summary>
        /// Displays an error window when error is thrown
        /// </summary>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception! " + ex.Message);
            }
        }


    }
}