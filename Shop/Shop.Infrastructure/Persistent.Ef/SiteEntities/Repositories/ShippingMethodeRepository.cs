﻿using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;

internal class ShippingMethodeRepository:BaseRepository<ShippingMethod>,IShippingMethodRepository
{
    public ShippingMethodeRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(ShippingMethod method)
    {
        Context.ShippingMethods.Remove(method);
    }
}