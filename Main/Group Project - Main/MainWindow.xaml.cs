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
            InitializeComponent();

            clsMainLogic mainLogic = new clsMainLogic();
            this.DataContext = mainLogic;

            mainLogic.FillData();


            // ItemsData needs to bind to ItemsList
            // InvoiceData needs to bind to InvoiceList



            // Show the Data
            // I think this only works for Windows Forms, not WPF
            try
            {
                //Create a DataSet to hold the data
                // DataSet ds;

                //Number of return values
                // int iRet = 0;

                //Execute the statement and get the data
                // ds = mainLogic.ds.ExecuteSQLStatement("SELECT * FROM Invoices", ref iRet);

                //Show the data
                //dataGridView1.DataSource = ds;
                //dataGridView1.DataMember = ds.Tables[0].TableName;
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

        }
    }


}