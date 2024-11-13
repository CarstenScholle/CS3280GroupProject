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

namespace Group10ProjectSearchBar.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        //sSelectedInvoiceID - Holds invoice ID if the user selected one, and 0 if not
        //SelectedInvoiceID - Property the main window can use to get the selected invoice ID [Call immediately after returning from search screen]
        public wndSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Return to main window without change when cancel button is clicked
        /// </summary>
        private void bSearchCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Return to main window with selected invoice [Main Window Interaction]
        /// </summary>
        private void bSearchSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            //Set local list varable that holds the Invoice ID for Selected Invoice
            //Pass variable to main window
            //Return to main window
        }

        /// <summary>
        /// Clear the filters set in the combo boxes and reset the table to a default state
        /// </summary>
        private void bSearchClearFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Have search logic return an approprate data list for selected object and apply it
        /// </summary>
        private void cbSearchInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Have search logic return an approprate data list for selected object and apply it
        /// </summary>
        private void cbSearchInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Have search logic return an approprate data list for selected object and apply it
        /// </summary>
        private void cbSearchTotalCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
