using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service {

    [ServiceContract]
    public interface IProductService {

        [OperationContract]
        Product GetProduct(int id);

        [OperationContract]
        List<Product> GetAllProducts();

        [OperationContract]
        bool InsertProduct(Product productToInsert);

        [OperationContract]
        List<string> GetAllSizes();

        [OperationContract]
        List<string> GetAllColors();

        [OperationContract]
        List<string> GetAllCategories();

        [OperationContract]
        bool InsertProductVersion(ProductVersion prodVerToInsert, int styleNumber);

        [OperationContract]
        bool InsertProductCategoryRelation(int styleNumber, string category);

        [OperationContract]
        bool DeleteProduct(int styleNumber);

        [OperationContract]
        bool DeleteProductVersion(int styleNumber, string sizeCode, string colorCode);

        [OperationContract]
        bool UpdateProductVersion(int styleNumber, string sizeCode, string colorCode, int newStock);

        [OperationContract]
        bool UpdateProduct(Product productToUpdate);

        [OperationContract]
        int GetStock(int styleNumber, string sizeCode, string colorCode);
    }
}