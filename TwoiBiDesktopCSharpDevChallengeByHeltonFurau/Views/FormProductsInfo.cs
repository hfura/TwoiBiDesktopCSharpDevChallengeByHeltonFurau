using TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Controllers;

namespace TwoiBiDesktopCSharpDevChallengeByHeltonFurau
{
    public partial class FormProductsInfo : Form
    {
        FormProduct formProduct;
        public FormProductsInfo()
        {
            InitializeComponent();
            formProduct = new FormProduct(this);
        }

        public void Display()
        {
            string query = "SELECT * from products WHERE inactive != 1";
            ProductController.DisplayAndSearchProducts(query, dataGridView);
        }
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            formProduct.Clear();
            formProduct.ShowDialog();
        }

        private void FormProductsInfo_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * from products WHERE (identifier LIKE '%"+ txtSearchProduct.Text +"%' OR description LIKE '%"+ txtSearchProduct.Text + "%' OR descriptionEN LIKE '%"+ txtSearchProduct.Text +"%') AND inactive != 1 ";
            ProductController.DisplayAndSearchProducts(query, dataGridView);
        }

        private async void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //update
            if(e.ColumnIndex == 0 )
            {
                formProduct.Clear();
                formProduct.id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());

                formProduct.identifier = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                formProduct.description = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                formProduct.descriptionEN = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                formProduct.price = (double)dataGridView.Rows[e.RowIndex].Cells[6].Value;
                formProduct.unit = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                formProduct.availableStk = (double)dataGridView.Rows[e.RowIndex].Cells[8].Value;
                formProduct.vat = (double)dataGridView.Rows[e.RowIndex].Cells[9].Value;




                formProduct.UpdateProduct();
                formProduct.ShowDialog();
                return;
            }
            //delete
            if(e.ColumnIndex == 1 )
            {
                if( (MessageBox.Show("Delete this product?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes))
                {
                    int id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    int response = await ReqwestController.DeleteProduct(id);
                    if (response == 1)
                    {
                        ProductController.DeleteProduct(id);
                        Display();
                    }
                    else
                    {
                        MessageBox.Show("Error! \n Some error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }
        }

        private async void btnSyncData_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are sure you want to sync data?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes))
            {
                btnSyncData.Text = "Processing...";
                var products = await ReqwestController.GetAllProducts();
                int success = 0;
                int fail = 0;
                foreach (var product in products)
                    if (ProductController.AddProduct(product, false) == 1)
                    {
                        success++;
                    }
                    else
                    {
                        fail++;
                    }

                MessageBox.Show("Sync complete!\n" + "Success: " + success + "\nFailed: " + fail, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSyncData.Text = "Sync Data With Reqwest";
                Display();
            }
        }
    }
}