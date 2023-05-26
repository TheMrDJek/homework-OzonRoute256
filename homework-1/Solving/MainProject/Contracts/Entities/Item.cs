using System.Collections.Generic;

using MainProject.Contracts.Entities.ValueObjects;

namespace MainProject.Contracts.Entities
{
    public sealed class Item
    {
    public long Id { get; set; }
    public VolumeWeightData VolumeWeight { get; set; }
    public Price Price { get; set; }
    public List<Seller> Sellers { get; set; } = new List<Seller>();
    public SaleInfo SaleInfo { get; set; }
    }
}