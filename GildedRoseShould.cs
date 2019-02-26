using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    public class GildedRoseShould
    {
        [Fact]
        public void DecrementQuality_WhenQualityGreaterThanZero()
        {
            var item = SetupItemHelper("+5 Dexterity Vest", 10, 20);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(19);
        }

        [Fact]
        public void NotDecrementQuality_WhenItemNameIs_AgedBrie()
        {
            var item = SetupItemHelper("Aged Brie", 2, 1);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().NotBe(0);
        }

        [Fact]
        public void NotDecrementQuality_WhenItemNameIs_Concert()
        {
            var item = SetupItemHelper("Backstage passes to a TAFKAL80ETC concert", 10, 20);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().NotBe(0);
        }

        [Fact]
        public void NotDecrementQuality_WhenItemNameIs_Sulfuras()
        {
            var item = SetupItemHelper("Sulfuras, Hand of Ragnaros", 0, 80);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().NotBe(19);
        }

        [Fact]
        public void DecrementQuality_TwiceAsFastWhenSellByDateHasPassed()
        {
            var item = SetupItemHelper("Elixir of the Mongoose", 0, 7);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(5);
        }

        [Fact]
        public void NotDecrementQuality_BeyondZero()
        {
            var item = SetupItemHelper("Elixir of the Mongoose", 5, 0);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(0);
        }

        [Fact]
        public void IncrementQuality_OfAgedBrie_AsItAges()
        {
            var item = SetupItemHelper("Aged Brie", 5, 10);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(11);
        }

        [Fact]
        public void Decrement_SellIn_Daily()
        {
            var item = SetupItemHelper("+5 Dexterity Vest", 10, 20);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().SellIn.Should().Be(9);
        }

        [Fact]
        public void NotDecrementSellIn_ForSulfuras()
        {
            var item = SetupItemHelper("Sulfuras, Hand of Ragnaros", 10, 80);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().SellIn.Should().Be(10);
        }

        [Fact]
        public void EnsureConcertTicketsQuality_IsZero_AfterConcert()
        {
            var item = SetupItemHelper("Backstage passes to a TAFKAL80ETC concert", 0, 10);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(0);
        }

        [Fact]
        public void DecrementQualityTwiceAsFast_ForConjuredItems()
        {
            var item = SetupItemHelper("Conjured Mana Cake", 3, 6);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(4);
        }

        [Fact]
        public void IncrementConcertByTwo_IfLessThan10Days()
        {
            var item = SetupItemHelper("Backstage passes to a TAFKAL80ETC concert", 10, 26);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(28);
        }
        private static IList<Item> SetupItemHelper(string name, int days, int quality)
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = name, SellIn = days, Quality = quality},
            };

            return items;
        }

        [Fact]
        public void ResetConcertTicketsQualityIfAboveMax()
        {
            var item = SetupItemHelper("Backstage passes to a TAFKAL80ETC concert", 3, 100);
            var app = new GildedRose(item);

            app.UpdateQuality();

            item.First().Quality.Should().Be(50);

        }
    }
}