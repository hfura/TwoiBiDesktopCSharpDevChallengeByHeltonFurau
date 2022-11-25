using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Models;
using TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Controllers;

namespace TwoiBiDesktopCSharpDevChallengeByHeltonFurau
{
    public partial class FormProduct : Form
    {
        private readonly FormProductsInfo _parent;
        public int id;
        public string identifier, description, descriptionEN, unit;
        public double price, availableStk, vat;

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public FormProduct(FormProductsInfo parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateProduct()
        {
            label3.Text = "Update Product";
            btnSave.Text = "Update";
            txtIdentifier.Text = identifier;
            txtDescription.Text = description;
            txtDescriptionEN.Text = descriptionEN;
            numPrice.Value = (decimal) price;
            txtUnit.Text = unit;
            numAvailableStk.Value = (decimal) availableStk;
            numVAT.Value = (decimal) vat;

        }
        public void Clear()
        {
            txtIdentifier.Text = txtDescription.Text = txtDescriptionEN.Text = txtUnit.Text = "";
            numPrice.Value = numAvailableStk.Value = numVAT.Value = 0;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string identifier = txtIdentifier.Text.Trim();
            string description = txtDescription.Text.Trim();
            string descriptionEN = txtDescriptionEN.Text.Trim();
            double price = ((double)numPrice.Value);
            string unit = txtUnit.Text.Trim();
            double availableStk = ((double)numAvailableStk.Value);
            double vat = ((double)numVAT.Value);

            if (identifier.Length == 0)
            {
                MessageBox.Show("Fill the identifier field");
                return;
            }
            if (description.Length == 0)
            {
                MessageBox.Show("Fill the description field");
                return;
            }
            if (descriptionEN.Length == 0)
            {
                MessageBox.Show("Fill the descriptionEN field");
                return;
            }
            if (price == 0)
            {
                MessageBox.Show("Fill the price field");
                return;
            }
            if (unit.Length == 0)
            {
                MessageBox.Show("Fill the unit field");
                return;
            }


            if(btnSave.Text == "Add")
            {
                btnSave.Text = "Adding...";
                //call create on api
                ProductModel newProduct = await ReqwestController.CreateProduct(identifier, description, descriptionEN, price, unit, availableStk, vat);

                //create product in local db
                ProductController.AddProduct(newProduct, true);

                //clear fields
                Clear();
                btnSave.Text = "Add";
            }

            if (btnSave.Text == "Update")
            {
                btnSave.Text = "Updating...";
                //call update on api
                ProductModel updatedProduct = await ReqwestController.UpdateProduct(id, identifier, description, descriptionEN, price, unit, availableStk, vat);

                //update product in local db
                ProductController.UpdateProduct(updatedProduct);

                //clear fields
                Clear();
                btnSave.Text = "Update";
            }

            _parent.Display();
        }
    }
}
