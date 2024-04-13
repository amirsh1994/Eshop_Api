using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.Create;

public record CreateCategoryCommand(string Title, SeoData SeoData, string Slug) :IBaseCommand<long>;