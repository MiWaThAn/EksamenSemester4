using Google.Api;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data;
using ProductCatalogService.Entities;
namespace ProductCatalogService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogContext _context;

        public ProductRepository(ProductCatalogContext context)
        {
            _context = context;
        }

        //Create
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        //Read
        public async Task<Product?> GetByIdAsync(Guid id)
        {

            return await _context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var distinctIds = ids
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (distinctIds.Count == 0)
            {
                return [];
            }

            var products = await _context.Products
                .Where(p => distinctIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            return products.AsReadOnly();
        }

        public async Task<Product?> GetByBarcodeAsync(string barcode)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
        }





        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Products.CountAsync();

            var items = await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }

        public Task UpdateAsync(Product product)
        {
            
            _context.Products.Update(product);
            return Task.CompletedTask;
        }


        //Delete
        public Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask; 
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            
        }
    }
}
