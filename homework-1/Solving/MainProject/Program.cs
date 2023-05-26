using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

using MainProject.Contracts.Entities;
using MainProject.Contracts.Entities.ValueObjects;
using MainProject.Contracts.Externals;
using MainProject.Enums;
using MainProject.Services;

namespace MainProject
{
    public class Program
    {
        [MemoryDiagnoser]
        public class IntroSetupCleanupGlobal
        {
            private IEnumerable<ItemDto> _dataFromExternalService;

            private ItemDto GetByIndex(int value)
            {
                int index = value % 5;

                ItemDto itemDto = index switch
                {
                    0 => JsonSerializer.Deserialize<ItemDto>(ItemDtoGenerator.RawDataItem0),
                    1 => JsonSerializer.Deserialize<ItemDto>(ItemDtoGenerator.RawDataItem1),
                    2 => JsonSerializer.Deserialize<ItemDto>(ItemDtoGenerator.RawDataItem2),
                    3 => JsonSerializer.Deserialize<ItemDto>(ItemDtoGenerator.RawDataItem3),
                    4 => JsonSerializer.Deserialize<ItemDto>(ItemDtoGenerator.RawDataItem4),
                    _=> throw new Exception("Unknown index")
                };

                itemDto!.Id = value;

                return itemDto;
            }

            [GlobalSetup]
            public void GlobalSetup()
            {
                var list = new List<ItemDto>();
                foreach (int i in Enumerable.Range(1, 5000))
                {
                    list.Add(GetByIndex(i));
                }

                _dataFromExternalService = list;
            }
            
            [Benchmark]
            public void Process()
            {
                var converter = new ItemConverter();
                Item[] items = converter.ConvertItems(_dataFromExternalService);
                var itemsBySize = new Dictionary<SizeType, List<Item>>();

                for (int i = 0; i < items.Length; i++)
                {
                    SizeType sizeType = ItemSizeManager.GetSizeType(items[i].VolumeWeight);
                    
                    if (!itemsBySize.ContainsKey(sizeType))
                    {
                        itemsBySize.Add(sizeType, new List<Item>(){items[i]});
                    }
                    else
                    {
                        itemsBySize[sizeType].Add(items[i]);
                    }
                }
                
                ItemSaver.SaveBySize(itemsBySize);
                
                var itemsForMainPage = new List<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    if (MainPageManager.IsForMainPage(items[i]))
                    {
                        itemsForMainPage.Add(items[i]);
                    }
                }

                ItemSaver.SendItemsForMainPage(itemsForMainPage);
            }
        }

        private static void Main(string[] args)
        {
            Summary[] summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}