using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAgg;

public class ProductImage:BaseEntity
{
    public long ProductId { get; internal set; }

    public string ImageName { get; private set; }

    public int SortOrder { get; private set; }

    private ProductImage()
    {
        
    }
    public ProductImage(string imageName, int sortOrder)
    {
        NullOrEmptyDomainDataException.CheckString(imageName,nameof(imageName));
        ImageName = imageName;
        SortOrder = sortOrder;
    }

   
}