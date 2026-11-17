namespace ProductCatalogService.Entities
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public string Size { get; private set; }
        public string Color { get; private set; }
        public string Category { get; private set; }
        public string Barcode { get; private set; }
        public bool? InStock { get; private set; }

        protected Product() { } //For EF Core only
        private Product(string name, double price, string description, string? imageUrl, string size, string color, string category, string barcode)
        {
            UpdateName(name);
            UpdatePrice(price);
            UpdateDescription(description);
            UpdateImageUrl(imageUrl);
            UpdateSize(size);
            UpdateColor(color);
            UpdateCategory(category);
            UpdateBarcode(barcode);
            
        }



        public static Product Create(string name, double price, string description, string? imageUrl, string size, string color, string category, string barcode)
        {
            return new Product(name, price, description, imageUrl, size, color, category, barcode);
            //I should make results for my factory methods, but it'll for now be exceptions for better time management.
        }

        public void Update(string name, double price, string description, string? imageUrl, string size, string color, string category)
        {
            UpdateName(name);
            UpdatePrice(price);
            UpdateDescription(description);
            UpdateImageUrl(imageUrl);
            UpdateSize(size);
            UpdateColor(color);
            UpdateCategory(category);
            //I should make results for my factory methods, but it'll for now be exceptions for better time management.
        }

        private void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            if (name.Length < 3 || name.Length > 100)
            {
                throw new ArgumentException("Product name must be between 3 and 100 characters long.");
            }
            Name = name.Trim();
        }
        private void UpdatePrice(double price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Product price cannot be negative.");
            }
            Price = price;
        }
        private void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Product description cannot be null or empty.");
            }
            Description = description.Trim();
        }
        private void UpdateImageUrl(string? imageUrl)
        {
           
            ImageUrl = imageUrl?.Trim();
        }
        private void UpdateSize(string size)
        {
            if (string.IsNullOrWhiteSpace(size))
            {
                throw new ArgumentException("Product size cannot be null or empty.");
            }
            Size = size.Trim();
        }
        private void UpdateColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Product color cannot be null or empty.");
            }
            Color = color.Trim();
        }
        private void UpdateCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Product category cannot be null or empty.");
            }
            Category = category.Trim();
        }
        private void UpdateBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new ArgumentException("Product barcode cannot be null or empty.");
            }
            if (barcode.Length < 8 || barcode.Length > 14)
            {
                throw new ArgumentException("Product barcode must be between 8 and 14 characters long.");
            }
            Barcode = barcode.Trim();
        }
        public void UpdateInStock(bool? inStock)
        {
            InStock = inStock;
        }


    }
}
