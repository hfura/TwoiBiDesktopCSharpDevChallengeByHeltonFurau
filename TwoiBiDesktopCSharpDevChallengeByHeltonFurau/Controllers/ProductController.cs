using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Models;

namespace TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Controllers
{
    internal class ProductController
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=csharpdevchallenge";
            MySqlConnection con = new MySqlConnection(sql);

            try
            {
                con.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("MySQL Connection Error! \n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return con;
        }

        public static int AddProduct(ProductModel pdt, bool showAlert)
        {

            string sql = "INSERT INTO products VALUES (@id, @identifier, @description, @descriptionEN, @price, @unit, @availableSTK, @vat, @inactive, @componentType, NULL)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = pdt.Id;
            cmd.Parameters.Add("@identifier", MySqlDbType.VarChar).Value = pdt.Identifier;
            cmd.Parameters.Add("@description", MySqlDbType.Text).Value = pdt.Description;
            cmd.Parameters.Add("@descriptionEN", MySqlDbType.Text).Value = pdt.DescriptionEN;
            cmd.Parameters.Add("@price", MySqlDbType.Double).Value = pdt.Price;
            cmd.Parameters.Add("@unit", MySqlDbType.VarChar).Value = pdt.Unit;
            cmd.Parameters.Add("@availableSTK", MySqlDbType.Double).Value = pdt.AvailableSTK;
            cmd.Parameters.Add("@vat", MySqlDbType.Double).Value = pdt.VAT;
            cmd.Parameters.Add("@inactive", MySqlDbType.Binary).Value = pdt.Inactive;
            cmd.Parameters.Add("@componentType", MySqlDbType.Int64).Value = pdt.ComponentType;
            cmd.Parameters.Add("@stkByWarehouses", MySqlDbType.JSON).Value = pdt.StkByWarehouses;

            try
            {
                cmd.ExecuteNonQuery();
                if(showAlert)MessageBox.Show("Product Added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                return 1;
            }
            catch (MySqlException e)
            {
                if(showAlert) MessageBox.Show("Error! \n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
                return 0;
            }

        }

        public static void UpdateProduct(ProductModel pdt)
        {
            string sql = "UPDATE products SET identifier = @identifier, description = @description, descriptionEN = @descriptionEN, price = @price, unit = @unit," +
                "availableSTK = @availableSTK, vat = @vat, inactive = @inactive, componentType = @componentType, stkByWarehouses = @stkByWarehouses WHERE id = @id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = pdt.Id;
            cmd.Parameters.Add("@identifier", MySqlDbType.VarChar).Value = pdt.Identifier;
            cmd.Parameters.Add("@description", MySqlDbType.Text).Value = pdt.Description;
            cmd.Parameters.Add("@descriptionEN", MySqlDbType.Text).Value = pdt.DescriptionEN;
            cmd.Parameters.Add("@price", MySqlDbType.Double).Value = pdt.Price;
            cmd.Parameters.Add("@unit", MySqlDbType.VarChar).Value = pdt.Unit;
            cmd.Parameters.Add("@availableSTK", MySqlDbType.Double).Value = pdt.AvailableSTK;
            cmd.Parameters.Add("@vat", MySqlDbType.Double).Value = pdt.VAT;
            cmd.Parameters.Add("@inactive", MySqlDbType.Binary).Value = pdt.Inactive;
            cmd.Parameters.Add("@componentType", MySqlDbType.Int64).Value = pdt.ComponentType;
            cmd.Parameters.Add("@stkByWarehouses", MySqlDbType.JSON).Value = pdt.StkByWarehouses;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error! \n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        public static void DeleteProduct(int id)
        {
            string sql = "UPDATE products SET inactive = @inactive WHERE id = @id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = id;
            cmd.Parameters.Add("@inactive", MySqlDbType.Binary).Value = 1;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error! \n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        public static void DisplayAndSearchProducts(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dgv.DataSource = dt;
            con.Close();
        }
    }
}
