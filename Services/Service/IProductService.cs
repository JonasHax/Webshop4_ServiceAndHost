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

        //[OperationContract]
        //List<ProductVersion> GetProductVersionsByProductID(int id);
    }
}