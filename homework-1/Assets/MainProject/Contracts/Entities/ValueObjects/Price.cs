namespace MainProject.Contracts.Entities.ValueObjects
{
    public sealed class Price
    {
        public long Value { get; set; }
        public string Currency { get; set; }
    }
}