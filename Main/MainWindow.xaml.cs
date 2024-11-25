using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group_Project___Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Main Window – Must have comments about how after the search window is opened,
        // how the main window will extract the Invoice ID from the search window.
        // Also needs another comment that describes how the items combo box will be refreshed
        // if any items are modified/added on the items window.

        // Everything needs try-catch blocks



        public MainWindow()
        {
            try
            {
                InitializeComponent();

                clsMainLogic mainLogic = new clsMainLogic();
                //DataContext = mainLogic;

                dgInvoice.ItemsSource = mainLogic.InvoiceItems;
                cbItemList.ItemsSource = mainLogic.ItemList;


                //InvoiceDate.Content = mainLogic.InvoiceDate;
                //InvoiceTotal.Content = mainLogic.InvoiceTotal;



                mainLogic.FillData();

                InvoiceNumber.Content = "Invoice Number: " + mainLogic.InvoiceNumber;
                // Figure out how to databind the label to InvoiceNumber, then do the other labels for the main page.

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// This method is called when the selected item is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        /// This method is called when the save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// This method is called when the Add Item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This method is called when the Delete Item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This method is called when a menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Show respective window depending on which one is clicked

        }








    }


}