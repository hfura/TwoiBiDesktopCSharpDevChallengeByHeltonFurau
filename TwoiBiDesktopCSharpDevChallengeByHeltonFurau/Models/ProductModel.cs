using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwoiBiDesktopCSharpDevChallengeByHeltonFurau.Models
{
    internal class ProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("descriptionEN")]
        public string DescriptionEN { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
        [JsonPropertyName("availableSTK")]
        public double AvailableSTK { get; set; }
        [JsonPropertyName("vat")]
        public double VAT { get; set; }
        [JsonPropertyName("inactive")]
        public bool Inactive { get; set; }
        [JsonPropertyName("componentType")]
        public int ComponentType { get; set; }
        [JsonPropertyName("stkByWarehouses")]
        public string[] StkByWarehouses { get; set; }
    }
}
