using DbModel.Context;
using DbModel.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly BasketContext _basketContext;
        public ProductRepo(BasketContext basketContext)
        {
            _basketContext = basketContext;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _basketContext.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _basketContext.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _basketContext.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _basketContext.ProductTypes.ToListAsync();
              
        }
    }
}
