namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GirlsHandmadeShop.Services.Data.DTOs;

    public interface IGetCountsService
    {
        CountsDto GetCounts();
    }
}
