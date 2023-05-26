namespace MainProject.Contracts.Externals
{
    public sealed class ItemDto
    {
        public long Id { get; set; }
        public long Weight { get; set; }
        public long Length { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public long PackagedWeight { get; set; }
        public long PackagedLength { get; set; }
        public long PackagedHeight { get; set; }
        public long PackagedWidth { get; set; }
        public long PriceValue { get; set; }
        public string PriceCurrency { get; set; }
        public long[] SellerIds { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsBestSeller { get; set; }
    }
}