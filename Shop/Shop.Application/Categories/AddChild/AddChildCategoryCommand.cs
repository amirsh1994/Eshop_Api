﻿using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.AddChild;

public record AddChildCategoryCommand(long ParentId,string Title, SeoData SeoData, string Slug) :IBaseCommand<long>;

