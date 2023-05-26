using System.Collections.Generic;
using System.Linq;

using MainProject.Contracts.Entities;
using MainProject.Contracts.Entities.ValueObjects;
using MainProject.Contracts.Externals;

namespace MainProject.Services
{
    internal sealed class ItemConverter
    {
        public Item[] ConvertItems(IEnumerable<ItemDto> dtoItems)
        {
            var dtoItemsArr = dtoItems.ToArray();
            var items = new Item[dtoItemsArr.Length];

            for (int i = 0; i < dtoItemsArr.Length; i++)
            {
                var item = new Item
                {
                    Id = dtoItemsArr[i].Id,
                    Price = new Price
                    {
                        Currency = dtoItemsArr[i].PriceCurrency,
                        Value = dtoItemsArr[i].PriceValue
                    },
                    VolumeWeight = new VolumeWeightData
                    {
                        Height = dtoItemsArr[i].Height,
                        Length = dtoItemsArr[i].Length,
                        Weight = dtoItemsArr[i].Weight,
                        Width = dtoItemsArr[i].Width,
                        PackagedHeight = dtoItemsArr[i].PackagedHeight,
                        PackagedLength = dtoItemsArr[i].PackagedLength,
                        PackagedWeight = dtoItemsArr[i].PackagedWeight,
                        PackagedWidth = dtoItemsArr[i].PackagedWidth
                    },
                    SaleInfo = new SaleInfo
                    {
                        Rating = dtoItemsArr[i].Rating,
                        IsActive = dtoItemsArr[i].IsActive,
                        IsBestSeller = dtoItemsArr[i].IsBestSeller
                    }
                };

                for (int j = 0; j < dtoItemsArr[i].SellerIds.Length; j++)
                {
                    item.Sellers.Add(new Seller(){Id = dtoItemsArr[i].SellerIds[j]});
                }
                
                items[i] = item;
            }

            return items;
        }
    }
}