using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var cat = await Context.Categories
                .Include(x => x.Children)
                .ThenInclude(x=>x.Children)
                .FirstOrDefaultAsync(x => x.Id == categoryId);
            if (cat==null)
            {
                return false;
            }

            var isExistsProduct=await Context.Products.AnyAsync(x=>x.CategoryId==cat.Id||  x.SubCategoryId==cat.Id||  x.SecondarySubCategoryId==cat.Id);

            if (isExistsProduct)
            {
                return false;
            }

            if (cat.Children.Any(x=>x.Children.Any()))
            {
                Context.RemoveRange(cat.Children.SelectMany(x=>x.Children));
            }
            Context.RemoveRange(cat.Children);
            Context.Remove(cat);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
