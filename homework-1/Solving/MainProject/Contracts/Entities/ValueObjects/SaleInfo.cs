using System;

namespace MainProject.Contracts.Entities.ValueObjects
{
    public struct SaleInfo : IEquatable<SaleInfo>
    {
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsBestSeller { get; set; }

        public bool Equals(SaleInfo other)
        {
            return Rating == other.Rating && IsActive == other.IsActive && IsBestSeller == other.IsBestSeller;
        }

        public override bool Equals(object obj)
        {
            return obj is SaleInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rating, IsActive, IsBestSeller);
        }
    }
}