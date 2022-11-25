
using TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Models;

using Flurl;
using Flurl.Http;

namespace TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Controllers
{
    internal class ReqwestController
    {
        private static readonly string API = "https://test-reqwest-application.herokuapp.com";
        private static readonly string AppID = "453f0e4bd91d417b8c68b6c468bf5560";
        private static readonly string TenID = "testchallenge";

        //create
        public static async Task<ProductModel> CreateProduct(string identifier, string description, string descriptionEN, double price, string unit, double availableSTK, double vat)
        {
            ProductModel neWProduct = await API.WithHeaders(new { APPLICATION_ID = AppID, TENANT_ID = TenID }).AppendPathSegment("products").PostJsonAsync(new {
                identifier,
                description,
                descriptionEN,
                price,
                unit,
                availableSTK,
                vat
            } ).ReceiveJson<ProductModel>();

            return neWProduct;

        }
        //update
        public static async Task<ProductModel> UpdateProduct(int id, string identifier, string description, string descriptionEN, double price, string unit, double availableSTK, double vat)
        {
            ProductModel updatedProduct = await API.WithHeaders(new { APPLICATION_ID = AppID, TENANT_ID = TenID }).AppendPathSegment("products/"+id+"").PutJsonAsync(new
            {
                identifier,
                description,
                descriptionEN,
                price,
                unit,
                availableSTK,
                vat,
                inactive = 0
            }).ReceiveJson<ProductModel>();

            return updatedProduct;
        }
        //delete
        public static async Task<int> DeleteProduct(int id)
        {
            var response = await API.WithHeaders(new { APPLICATION_ID = AppID, TENANT_ID = TenID }).AppendPathSegment("products/"+id+"").DeleteAsync();
            if(response.StatusCode == 200)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //get all
        public static async Task<List<ProductModel>> GetAllProducts()
        {
            List<ProductModel> products = await API.WithHeaders(new { APPLICATION_ID = AppID, TENANT_ID = TenID }).AppendPathSegment("products/all").GetJsonAsync<List<ProductModel>>();

            return products;
        }

    }
}
