﻿using Group_Project___Main.Items;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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

        /// <summary>
        /// Main Logic Class
        /// </summary>
        clsMainLogic mainLogic = new clsMainLogic();

        /// <summary>
        /// Search Window
        /// </summary>
        wndSearch searchWindow;

        /// <summary>
        /// Items Window
        /// </summary>
        wndItems itemsWindow;
        

        /// <summary>
        /// Main Window constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                

                


                dgInvoice.ItemsSource = mainLogic.InvoiceItems;
                cbItemList.ItemsSource = mainLogic.ItemList;


                MainRefresh();

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        
        /// <summary>
        /// method that refreshes the invoice data grid
        /// </summary>
        private void MainRefresh()
        {
            try {
                mainLogic.FillData();

                lblInvoiceNumber.Content = "Invoice Number: " + mainLogic.InvoiceNumber;
                lblInvoiceDate.Content = "Invoice Date: " + mainLogic.InvoiceDate;
            
                dgInvoice.Items.Refresh();

            

                lblInvoiceTotal.Content = "Invoice Total: " + mainLogic.InvoiceTotal;
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
            try
            {
                mainLogic.SelectedItem = cbItemList.SelectedItem.ToString();
                mainLogic.UpdateItemInfo();
                lblItemName.Content = "Name: " + mainLogic.SelectedItem;
                lblItemPrice.Content = "Cost: " + mainLogic.SelectedItemCost;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// This method is called when the create invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try {
                mainLogic.CreateInvoice();
                MainRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// This method is called when the Add Item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem_Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                mainLogic.AddItem();
                dgInvoice.Items.Refresh();
                lblInvoiceTotal.Content = "Invoice Total: " + mainLogic.InvoiceTotal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method is called when the Delete Item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                // Check if an item was selected
                if (dgInvoice.SelectedItem != null)
                {
                    mainLogic.DeleteItem(dgInvoice.SelectedItem);
                    //dgInvoice.ItemsSource = mainLogic.InvoiceItems;
                    // I thought when you remove an item from the list ItemsSource is set to
                    // the item will be removed on the GUI as well because of DataBinding
                    dgInvoice.Items.Refresh();
                    lblInvoiceTotal.Content = "Invoice Total: " + mainLogic.InvoiceTotal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Method for opening the Edit Invoice window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Invoices_Click(object sender, RoutedEventArgs e)
        {
            try {
                this.Hide();
                itemsWindow = new wndItems();
                itemsWindow.openWindow();
                this.Show();


                // refresh item list combobox
                mainLogic.FillData();
                cbItemList.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Method for opening the Search Invoice window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Invoices_Click(object sender, RoutedEventArgs e)
        {
            try {
                this.Hide();
                searchWindow = new wndSearch();
                searchWindow.ShowDialog();
                this.Show();
                // Passing data around
                string sSearchedItem = searchWindow.SelectedInvoiceID();
                if (sSearchedItem != null && sSearchedItem != "0")
                {
                    mainLogic.InvoiceNumber = int.Parse(sSearchedItem);
                    mainLogic.UpdateInvoiceInfo();
                    MainRefresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// Makes sure application actually closes because for some reason it doesn't when you close it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            try {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method for enabling editing of the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbItemList.IsEnabled = true;
                btnAdd.IsEnabled = true;
                btnDelete.IsEnabled = true;
                dgInvoice.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method for closing editing mode. Everything saves automatically from other methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbItemList.IsEnabled = false;
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                dgInvoice.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }


}