using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel.Interface
{
    public interface IProductRepo
    {
            Task<Product> GetProductByIdAsync(int id);
            Task<IReadOnlyList<Product>> GetProductsAsync();
            Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
            Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

    }
}
