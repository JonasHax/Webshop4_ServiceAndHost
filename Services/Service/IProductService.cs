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
        int GetANumber(int number);

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

        //[OperationContract]
        //List<ProductVersion> GetProductVersionsByProductID(int id);
    }
}