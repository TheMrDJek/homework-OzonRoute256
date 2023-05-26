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
            var items = new List<Item>();

            foreach (ItemDto itemDto in dtoItems)
            {
                var item = new Item
                {
                    Id = itemDto.Id,
                    Price = new Price
                    {
                        Currency = itemDto.PriceCurrency,
                        Value = itemDto.PriceValue
                    },
                    Sellers = itemDto.SellerIds.Select(s => new Seller { Id = s }).ToList(),
                    VolumeWeight = new VolumeWeightData
                    {
                        Height = itemDto.Height,
                        Length = itemDto.Length,
                        Weight = itemDto.Weight,
                        Width = itemDto.Width,
                        PackagedHeight = itemDto.PackagedHeight,
                        PackagedLength = itemDto.PackagedLength,
                        PackagedWeight = itemDto.PackagedWeight,
                        PackagedWidth = itemDto.PackagedWidth
                    },
                    SaleInfo = new SaleInfo
                    {
                        Rating = itemDto.Rating,
                        IsActive = itemDto.IsActive,
                        IsBestSeller = itemDto.IsBestSeller
                    }
                };

                items.Add(item);
            }

            return items.ToArray();
        }
    }
}