﻿using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;

public class SliderRepository:BaseRepository<Slider>,ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(Slider slider)
    {
        Context.Remove(slider);
    }
}