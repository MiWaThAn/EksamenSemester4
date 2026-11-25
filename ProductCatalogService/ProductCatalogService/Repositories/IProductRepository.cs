using ProductCatalogService.Entities;

namespace ProductCatalogService.Repositories
{
    public interface IProductRepository
    {
        //Create
        Task AddAsync(Product product);
        //Read       
        
        Task<(IEnumerable<Product> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task<Product?> GetByBarcodeAsync(string barcode);

        //Update
        Task UpdateAsync(Product product);
        //Delete
        Task DeleteAsync(Product product);
        
        
        Task SaveChangesAsync();

    }
}
