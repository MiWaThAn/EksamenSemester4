using ProductCatalogService.Entities;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Data
{
       public class ProductDbSeeding
       {
           
            private readonly IProductRepository _productRepository;
            public ProductDbSeeding(IProductRepository productRepository)
            {
                
                _productRepository = productRepository;
            }

            public async Task SeedAsync(CancellationToken cancellationToken = default)
            {



                string barcode1 = "123456789";
                var existProd1 = await _productRepository.GetByBarcodeAsync(barcode1);
                if (existProd1 == null)
                {
                    var product = Product.Create(
                        name: "ProductTest1",
                        price: 200,
                        description: "Greatest product ever!",
                        imageUrl: null,
                        size: "M",
                        color: "Blue",
                        category: "General",
                        barcode: barcode1 );
                    await _productRepository.AddAsync(product);
                }

                string barcode2 = "987654321";
                var existProd2 = await _productRepository.GetByBarcodeAsync(barcode2);
                if (existProd2 == null)
                {
                    var product = Product.Create(
                        name: "ProductTest2",
                        price: 100,
                        description: "Second Greatest product ever!",
                        imageUrl: null,
                        size: "L",
                        color: "Red",
                        category: "General",
                        barcode: barcode2);
                    await _productRepository.AddAsync(product);
                }



            await _productRepository.SaveChangesAsync();
            }


       }
    
}
