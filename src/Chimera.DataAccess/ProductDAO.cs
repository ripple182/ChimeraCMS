using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using Chimera.Entities.Product;
using Chimera.Entities.Property;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class ProductDAO
    {
        private const string COLLECTION_NAME = "Products";

        /// <summary>
        /// Save or update a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool Save(Product product)
        {
            if (product.Id.Equals(string.Empty))
            {
                product.CreatedDateUtc = DateTime.UtcNow;
            }

            return Execute.Save<Product>(COLLECTION_NAME, product);
        }

        /// <summary>
        /// Load a list of all active products, just their name and id.
        /// </summary>
        /// <returns></returns>
        public static List<PreviewProduct> LoadPreviewProducts()
        {
            MongoCollection<Product> Collection = Execute.GetCollection<Product>(COLLECTION_NAME);

            List<PreviewProduct> PreviewProductList = new List<PreviewProduct>();

            //grab all active products where the name/description contains the searchText
            List<Product> ProductList = (from e in Collection.AsQueryable<Product>() where e.Active orderby e.Name select e).ToList();

            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (var MyProduct in ProductList)
                {
                    PreviewProductList.Add(new PreviewProduct { Id = MyProduct.Id, Name = MyProduct.Name });
                }
            }

            return PreviewProductList;
        }

        /// <summary>
        /// Generate a unique list of search properties from the active products in the database.
        /// </summary>
        /// <returns>list of search properties for all active products.</returns>
        public static List<Property> LoadProductSearchProperties()
        {
            MongoCollection<Product> Collection = Execute.GetCollection<Product>(COLLECTION_NAME);

            List<Product> ProductList = (from e in Collection.AsQueryable<Product>() where e.Active select e).ToList();

            Dictionary<string, Property> ReturnPropertyList = new Dictionary<string, Property>();

            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (var MyProduct in ProductList)
                {
                    if (MyProduct.SearchPropertyList != null && MyProduct.SearchPropertyList.Count > 0)
                    {
                        foreach (var ProductSearchProp in MyProduct.SearchPropertyList)
                        {
                            if (!ReturnPropertyList.ContainsKey(ProductSearchProp.Name))
                            {
                                ReturnPropertyList.Add(ProductSearchProp.Name, new Property(ProductSearchProp.Name));
                            }

                            if (ProductSearchProp.Values != null && ProductSearchProp.Values.Count > 0)
                            {
                                foreach (var SearchPropValue in ProductSearchProp.Values)
                                {
                                    if (!ReturnPropertyList[ProductSearchProp.Name].Values.Contains(SearchPropValue))
                                    {
                                        ReturnPropertyList[ProductSearchProp.Name].Values.Add(SearchPropValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ReturnPropertyList.Values.ToList();
        }

        /// <summary>
        /// Load a product by its unique ID
        /// </summary>
        /// <param name="bsonId"></param>
        /// <returns></returns>
        public static Product LoadByBsonId(string bsonId)
        {
            MongoCollection<Product> Collection = Execute.GetCollection<Product>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Product>() where e.Id == bsonId select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a list of products by their unique ids
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public static List<Product> LoadSpecificProducts(List<string> idList)
        {
            MongoCollection<Product> Collection = Execute.GetCollection<Product>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Product>() where e.Active == true orderby e.Name select e).Where(e => idList.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Search active products by selected search filters and the searchText vs name/description.
        /// </summary>
        /// <param name="selectedSearchFilters">Key/value of the selected search filters, i.e.  there is a "Color" dropdown menu, and the user selects "Red".</param>
        /// <param name="searchText">generic search text from a search text box to be checked vs name / description.</param>
        /// <returns>list of products that we searched for.</returns>
        public static List<Product> SearchProducts(Dictionary<string, List<string>> selectedSearchFilters = null, string searchText = "", bool? active = null)
        {
            MongoCollection<Product> Collection = Execute.GetCollection<Product>(COLLECTION_NAME);

            Dictionary<string, Product> ReturnProductDictionary = new Dictionary<string, Product>();

            //grab all active products where the name/description contains the searchText
            List<Product> ProductList = new List<Product>();

            if (active != null)
            {
                ProductList = (from e in Collection.AsQueryable<Product>() where e.Active == Boolean.Parse(active.ToString()) && (e.Name.ToUpper().Contains(searchText.ToUpper()) || e.Description.ToUpper().Contains(searchText.ToUpper())) orderby e.Name select e).ToList();
            }
            else
            {
                ProductList = (from e in Collection.AsQueryable<Product>() where e.Name.ToUpper().Contains(searchText.ToUpper()) || e.Description.ToUpper().Contains(searchText.ToUpper()) orderby e.Name select e).ToList();
            }

            foreach (var MyProduct in ProductList)
            {
                if (!ReturnProductDictionary.ContainsKey(MyProduct.Id))
                {
                    if (selectedSearchFilters != null && selectedSearchFilters.Count > 0)
                    {
                        if (MyProduct.SearchPropertyList != null && MyProduct.SearchPropertyList.Count > 0)
                        {
                            foreach (var ProductSearchProp in MyProduct.SearchPropertyList)
                            {
                                if (selectedSearchFilters.Keys.Contains(ProductSearchProp.Name))
                                {
                                    foreach (string SearchFilterValue in selectedSearchFilters[ProductSearchProp.Name])
                                    {
                                        //will be null if no values found
                                        var ProductSearchPropValue = ProductSearchProp.Values.Where(e => e.Value.Equals(SearchFilterValue));

                                        if (ProductSearchPropValue != null && !ReturnProductDictionary.ContainsKey(MyProduct.Id))
                                        {
                                            ReturnProductDictionary.Add(MyProduct.Id, MyProduct);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ReturnProductDictionary.Add(MyProduct.Id, MyProduct);
                    }
                }
            }

            return ReturnProductDictionary.Values.ToList();
        }
    }
}
