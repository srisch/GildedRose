using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    public class GildedRose
    {
        private const int minSellIn = 0;
        private const int minItemQuality = 0;
        private const int maxItemQuality = 50;

        private const string agedBrie = "Aged Brie";
        private const string concertTickets = "Backstage passes to a TAFKAL80ETC concert";
        private const string conjuredItem = "Conjured";
        private const string sulfuras = "Sulfuras, Hand of Ragnaros";

        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items.Where(constantItem => !constantItem.Name.Contains(sulfuras)))
            {
                if (item.Name is agedBrie)
                {
                    IncrementItemQuality(item);
                }
                else if (item.Name is concertTickets)
                {
                    HandleBackstagePasses(item);
                }
                else DecrementItemQuality(item);

                DecrementSellinDays(item);
                HandleItemPastSellin(item);

                Guard.ThatItemQualityDoesNotExceedMax(item);
                Guard.ThatItemQualityDoesNotExceedMin(item);
            }
        }

        private static void HandleItemPastSellin(Item item)
        {
            if (item.SellIn < minSellIn)
                if (item.Name != agedBrie)
                    DecrementItemQuality(item);
                else
                    IncrementItemQuality(item);
        }

        private static void DecrementSellinDays(Item item)
        {
            item.SellIn -= 1;
        }

        private static void HandleBackstagePasses(Item item)
        {
            const int eleven_days = 11;
            const int six_days = 6;

            if (item.SellIn <= minSellIn)
            {
                item.Quality = 0;
            }
            else if (item.SellIn < six_days)
            {
                item.Quality += 3;
            }
            else if (item.SellIn < eleven_days)
            {
                item.Quality += 2;
            }
            else IncrementItemQuality(item);
        }

        private static void DecrementItemQuality(Item item)
        {
            if (item.Name.Contains(conjuredItem))
                item.Quality -= 2;
            else
                item.Quality -= 1;
        }

        private static void IncrementItemQuality(Item item)
        {
            item.Quality += 1;
        }
    }
}