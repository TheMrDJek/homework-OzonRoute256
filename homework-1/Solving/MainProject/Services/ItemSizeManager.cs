using System.Collections.Generic;
using System.Linq;

using MainProject.Contracts.Entities.ValueObjects;
using MainProject.Enums;

namespace MainProject.Services
{
    internal static class ItemSizeManager
    {
        public static SizeType GetSizeType(VolumeWeightData data)
        {
            
            var allSize = new long[]
                { data.Height, data.Length, data.Width, data.PackagedHeight, data.PackagedLength, data.PackagedWidth };

            // var maxSuze = allSize.Max()
            long maxSize = 0;

            for (int i = 0; i < allSize.Length; i++)
            {
                if (allSize[i] > maxSize)
                {
                    maxSize = allSize[i];
                }
            }

            if (maxSize > 100)
            {
                return SizeType.Max;
            }

            //if (new [] { data.PackagedWeight, data.Weight }.Max() > 10000)
            if ((data.PackagedHeight > data.Weight ? data.PackagedHeight : data.Weight) > 10000)
            {
                return SizeType.Max;
            }

            long packagedVolume = data.PackagedHeight * data.PackagedLength * data.PackagedWidth;
            long volume = data.Height * data.Length * data.Width;

            //long minVolume = new[] { packagedVolume, volume }.Min();
            long minVolume = packagedVolume > volume ? volume : packagedVolume;

            if (minVolume < 10000)
            {
                return SizeType.Small;
            }

            if (minVolume < 30000)
            {
                return SizeType.Medium;
            }

            return SizeType.Max;
        }
    }
}