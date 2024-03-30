using Common.Application;
using Common.Domain.ValueObjects;
using FluentValidation;

namespace Shop.Application.Categories.Create;

public record CreateCategoryCommand(string Title, SeoData SeoData, string Slug) : IBaseCommand;