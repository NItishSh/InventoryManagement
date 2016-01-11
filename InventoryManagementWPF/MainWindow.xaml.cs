using System;
using System.Collections.Generic;
using System.Linq;
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
using InventoryManagement;

namespace InventoryManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtName.TextChanged += TxtName_TextChanged;
            txtManufacturer.TextChanged += TxtManufacturer_TextChanged;
            txtExpiryDate.TextChanged += TxtExpiryDate_TextChanged;
            txtMRP.TextChanged += TxtMRP_TextChanged;
            txtCostPrice.TextChanged += TxtCostPrice_TextChanged;
            txtQuantity.TextChanged += TxtQuantity_TextChanged;
            
            LoadlvFullinventory();
        }

        #region validations
        private void TxtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValidateNumeric(txtQuantity.Text))
            {
                lblQuantityError.Visibility = Visibility.Visible;
            }
            else
            {
                lblQuantityError.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtCostPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValidateMoney(txtCostPrice.Text))
            {
                lblCostPriceError.Visibility = Visibility.Visible;
            }
            else
            {
                lblCostPriceError.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtMRP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValidateMoney(txtMRP.Text))
            {
                lblMRPError.Visibility = Visibility.Visible;
            }
            else
            {
                lblMRPError.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtExpiryDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValidateExpiryDate(txtExpiryDate.Text))
            {                
                lblExpiryDateError.Visibility = Visibility.Visible;
            }
            else
            {
                lblExpiryDateError.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtManufacturer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValicateAlphaNumeric(txtManufacturer.Text))
            {                
                lblManufacturerError.Visibility = Visibility.Visible;
            }
            else
            {
                lblManufacturerError.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validate.ValicateAlphaNumeric(txtName.Text))
            {                
                lblNameError.Visibility = Visibility.Visible;
            }
            else
            {
                lblNameError.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        private void LoadlvFullinventory()
        {
            InventoryManager manager = new InventoryManager();
            lvFullinventory.ItemsSource = manager.GetAllItems();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validate input before using validator class
            //Add new inventory by invoking a method in inventory manager
            if(lblNameError.IsVisible || lblManufacturerError.IsVisible||lblExpiryDateError.IsVisible || lblMRPError.IsVisible||lblCostPriceError.IsVisible||lblQuantityError.IsVisible)
            {
                //Show a message to correct the imput informationn before saving a inventory item
            }
            else
            {
                Inventory newItem = new Inventory
                {
                    Name = txtName.Text,
                    Manufacturer = txtManufacturer.Text,
                    ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text),
                    MRP = Convert.ToDecimal(txtMRP.Text),
                    CostPrice = Convert.ToDecimal(txtCostPrice.Text),
                    PurchaseDate = DateTime.Now,
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Description = txtDescription.Text
                };
                InventoryManager manager = new InventoryManager();
                manager.AddItem(newItem);
                ClearinventoryTextBoxes();
                LoadlvFullinventory();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //clear all  text boxex and text blocks
            ClearinventoryTextBoxes();
        }

        private void ClearinventoryTextBoxes()
        {
            txtName.Clear();
            txtManufacturer.Clear();
            txtExpiryDate.Clear();
            txtMRP.Clear();
            txtCostPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
        }
        private void txtMedicineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //use new thread to load the data and try to implement loading progress.
            //call method in inventory manager to get Items by name
            if(!string.IsNullOrEmpty(txtMedicineName.Text))
            {
                InventoryManager manager = new InventoryManager();
                lvMedicines.ItemsSource = manager.GetAllItemsByName(txtMedicineName.Text);
                lbmedicineName.ItemsSource = manager.GetAllItemsByName(txtMedicineName.Text);                
            }               
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var itemId = ((Button)sender).Tag;
            //get item by above Id then add it to bill

        }
    }
}
