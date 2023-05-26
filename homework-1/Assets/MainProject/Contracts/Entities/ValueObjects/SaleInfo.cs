namespace MainProject.Contracts.Entities.ValueObjects
{
    public struct SaleInfo
    {
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsBestSeller { get; set; }
    }
}