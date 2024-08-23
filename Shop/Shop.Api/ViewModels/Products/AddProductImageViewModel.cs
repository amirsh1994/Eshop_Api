namespace Shop.Api.ViewModels.Products;

public class AddProductImageViewModel
{
    public IFormFile ImageFile { get;  set; }

    public long ProductId { get;  set; }

    public int Sequence { get;  set; }
}