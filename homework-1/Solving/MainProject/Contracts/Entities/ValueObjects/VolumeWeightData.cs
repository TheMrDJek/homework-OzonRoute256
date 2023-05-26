namespace MainProject.Contracts.Entities.ValueObjects
{
    public struct VolumeWeightData
    {
        public long Weight { get; set; }
        public long Length { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public long PackagedWeight { get; set; }
        public long PackagedLength { get; set; }
        public long PackagedHeight { get; set; }
        public long PackagedWidth { get; set; }
    }
}