using System;
using System.Collections.Generic;

namespace SisoDb.Specifications.Model
{
    public abstract class QueryXItem<T>
    {
        public const string JsonFormat = "{{\"StructureId\":{0},\"SortOrder\":{1},\"IntegerValue\":{2},\"StringValue\":\"{3}\"}}";

        public static IList<TItem> CreateItems<TItem>(int numOfItems, Action<int, TItem> initializer) where TItem : new()
        {
            var items = new List<TItem>();

            for (var c = 0; c < numOfItems; c++)
            {
                var item = new TItem();

                initializer.Invoke(c, item);

                items.Add(item);
            }

            return items;
        }

        public static IList<TItem> CreateFourItems<TItem>() where TItem : QueryXItem<T>, new()
        {
            return IfStringIdApplyStringIdValue(new[]
            {
                new TItem{SortOrder = 1, IntegerValue = 100, NullableIntegerValue = 100, StringValue = "A"},
                new TItem{SortOrder = 2, IntegerValue = 200, NullableIntegerValue = 200, StringValue = "B"},
                new TItem{SortOrder = 3, IntegerValue = 300, NullableIntegerValue = 300, StringValue = "C"},
                new TItem{SortOrder = 4, IntegerValue = 400, NullableIntegerValue = 400, StringValue = "D"}
            });
        }

        public static IList<TItem> CreateTenItems<TItem>() where TItem : QueryXItem<T>, new()
        {
            return IfStringIdApplyStringIdValue(new[]
            {
                new TItem{SortOrder = 1, IntegerValue = 100, NullableIntegerValue = 100, StringValue = "A"},
                new TItem{SortOrder = 2, IntegerValue = 200, NullableIntegerValue = 200, StringValue = "B"},
                new TItem{SortOrder = 3, IntegerValue = 300, NullableIntegerValue = 300, StringValue = "C"},
                new TItem{SortOrder = 4, IntegerValue = 400, NullableIntegerValue = 400, StringValue = "D"},
                new TItem{SortOrder = 5, IntegerValue = 500, NullableIntegerValue = 500, StringValue = "E"},
                new TItem{SortOrder = 6, IntegerValue = 600, NullableIntegerValue = 600, StringValue = "F"},
                new TItem{SortOrder = 7, IntegerValue = 700, NullableIntegerValue = 700, StringValue = "G"},
                new TItem{SortOrder = 8, IntegerValue = 800, NullableIntegerValue = 800, StringValue = "H"},
                new TItem{SortOrder = 9, IntegerValue = 900, NullableIntegerValue = 900, StringValue = "I"},
                new TItem{SortOrder = 10, IntegerValue = 1000, NullableIntegerValue = 1000, StringValue = "J"}
            });
        }

        public static IList<TItem> CreateFourUnorderedItems<TItem>() where TItem : QueryXItem<T>, new()
        {
            return IfStringIdApplyStringIdValue(new[]
            {
                new TItem{SortOrder = 2, IntegerValue = 400, NullableIntegerValue = 400, StringValue = "D"},
                new TItem{SortOrder = 2, IntegerValue = 300, NullableIntegerValue = 300, StringValue = "C"},
                new TItem{SortOrder = 1, IntegerValue = 200, NullableIntegerValue = 200, StringValue = "B"},
                new TItem{SortOrder = 1, IntegerValue = 100, NullableIntegerValue = 100, StringValue = "A"}
            });
        }

        private static IList<TItem> IfStringIdApplyStringIdValue<TItem>(TItem[] items) where TItem : QueryXItem<T>
        {
            if (!(items[0] is QueryStringItem))
                return items;

            for (var c = 0; c < items.Length; c++)
                (items[c] as QueryStringItem).StructureId = (c + 1).ToString();

            return items;
        }

        public T StructureId { get; set; }

        public int SortOrder { get; set; }

        public int IntegerValue { get; set; }

        public int? NullableIntegerValue { get; set; }

        public string StringValue { get; set; }

        public abstract string AsJson();
    }
}