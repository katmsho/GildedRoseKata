using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata;
using NUnit.Framework;
using VerifyTests;

namespace GildedRoseTests;

public class ItemWithIndex
{
    public Item item { get; set; }
    public int index { get; set; }

}

public class GildedRoseNamingTest
{
    private GildedRose gildedRoseApp { get; set; }
    private int currentDay { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.currentDay = 0;
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            new Item {Name = "Very Aged Brie", SellIn = 5, Quality = 7},
            new Item {Name = "aged brie", SellIn = 5, Quality = 7},
            new Item {Name = "AGED BRIE", SellIn = 5, Quality = 7},
            new Item {Name = "Hand of Ragnaros, Sulfuras", SellIn = 10, Quality = 40},
            new Item {Name = "sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 40},
            new Item {Name = "SULFURAS, Hand of Ragnaros", SellIn = 10, Quality = 40},
            new Item {Name = "New Backstage passes", SellIn = 15, Quality = 20},
            new Item {Name = "Backstage Passes", SellIn = 15, Quality = 20},
            new Item {Name = "backstage passes", SellIn = 15, Quality = 20},
            new Item {Name = "BACKSTAGE PASSES", SellIn = 15, Quality = 20},
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases()
    {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 10; i++)
        {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality -1);
        }
    }
}

public class GildedRoseQualityTest
{
    private GildedRose gildedRoseApp { get; set; }
    private int currentDay { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.currentDay = 0;
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Too Old to Sell", SellIn = 0, Quality = 20},
            new Item {Name = "Don't go below 0 quality", SellIn = 0, Quality = 1},
            new Item {Name = "Don't go below 0 quality", SellIn = 0, Quality = 0},
            new Item {Name = "Aged Brie", SellIn = 1, Quality = 49},
            new Item {Name = "Aged Brie", SellIn = 1, Quality = 50},
            new Item {Name = "Aged Brie", SellIn = 0, Quality = 2},
            new Item {Name = "Aged Brie", SellIn = 0, Quality = 1},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 40},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 50},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 40},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 50},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 50},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 0}
        };

        gildedRoseApp = new GildedRose(currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList();  //need deep copy
        gildedRoseApp.UpdateQuality();
        this.currentItems = gildedRoseApp.Items;
    }
 

    [Test]
    public void AfterUpdateQuality()
    {
        // Basic tests on data: no quality value should be less than zero; no quality value should be greater than 50 (exception Sulfuras); Sulfurus quality = 80
        int numExpectedQualityLessThanZero = 0;
        int numActualQualityLessThanZero = currentItems.Where(item => item.Quality < 0).Count();
        Assert.That(numExpectedQualityLessThanZero == numActualQualityLessThanZero);

        int numExpectedQualityGreaterThanZeroAndNotSulfuras = 0;
        int numActualQualityGreaterThanZeroAndNotSulfuras = currentItems.Where(item => item.Quality > 50 && (!item.Name.Contains("Sulfuras", System.StringComparison.CurrentCulture))).Count();
        Assert.That(numExpectedQualityGreaterThanZeroAndNotSulfuras == numActualQualityGreaterThanZeroAndNotSulfuras);

        //no current tests on min/max sellIn value. It may be useful to know how far an item is past it's sellin date so will not restrict it to 0 or positive. Also have no input of what would be a valid max value. Hmm, what happens if very long sellIn value, but quality degrades to zero - add this to assumptions/to ask product
       
       //verify that every sellin has been redcued by 1
        var listLength = this.currentItems.Count();
        for (int i = 1; i < listLength; i++) {
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
         }

        //SPECIAL CASE: Sulfuras
        int numExpectedSulfuraAndQualityNot80AndSellInNot0 = 0;
        int numActualSulfuraAndQualityNot80AndSellInNot0 = this.currentItems.Where(item => item.Quality != 80 && item.SellIn != 0 && item.Name.Contains("Sulphuras", System.StringComparison.CurrentCulture)).Count(); //sulfurus spelling/location in string tested elsewehere
        Assert.That(numExpectedSulfuraAndQualityNot80AndSellInNot0 == numActualSulfuraAndQualityNot80AndSellInNot0);

        //SPECIAL CASE: Aged Brie
        List<ItemWithIndex> agedBrieItems = this.currentItems.Select((item, index) => new ItemWithIndex { item = item, index = index }).Where(ItemWithIndex => ItemWithIndex.item.Name.Contains("Aged Brie", System.StringComparison.CurrentCulture)).ToList();

        foreach (ItemWithIndex agedBrie in agedBrieItems)
        {
            var currentAgedBrieQuality = agedBrie.item.Quality;
            var previousAgedBrieQuality = previousItems[agedBrie.index].Quality;

            if (agedBrie.item.SellIn > 0) // aged brie past sellin is covered on standard past-sellin test
            {
                if (previousAgedBrieQuality == 50)
                {
                    Assert.That(currentAgedBrieQuality == previousAgedBrieQuality);
                }
                else
                {
                    Assert.That(currentAgedBrieQuality == previousAgedBrieQuality + 1);
                }
            }
        }

        //SPECIAL CASE: Backstage Passes
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { item = item, index = index }).Where(ItemWithIndex => ItemWithIndex.item.Name.Contains("Backstage passes", System.StringComparison.CurrentCulture)).ToList();

        foreach (ItemWithIndex backstagePass in backstagePassItems)
        {
            var currentBackstagePassQuality = backstagePass.item.Quality;
            var currentBackstagePassSellIn = backstagePass.item.SellIn;
            var previousBackstagePassQuality = previousItems[backstagePass.index].Quality;

            // backstage passess immediately have zero quality after sellin reached
            if (currentBackstagePassSellIn < 0)
            {
                 Assert.That(currentBackstagePassQuality == 0);  //TODO - this should be correct, but test fails
            }
            else
            {
                // maximum quality is 50 
                if (previousBackstagePassQuality == 50)
                {
                    Assert.That(currentBackstagePassQuality == previousBackstagePassQuality);
                }
                else
                {
                    // within 5 days of sellIn backstage pass increase by 3
                    if (currentBackstagePassSellIn < 5)
                    {
                        Assert.That(currentBackstagePassQuality == Math.Clamp(previousBackstagePassQuality + 3, 0, 50));
                    }
                    // between 5 and 10 days of sellIn backstage pass increase by 2
                    if (currentBackstagePassSellIn >= 5 && currentBackstagePassSellIn < 10)
                    {
                        Assert.That(currentBackstagePassQuality == Math.Clamp(previousBackstagePassQuality + 2, 0, 50));
                    }
                    // more than 10 days from sellIn backstage pass increase by 1
                    if (currentBackstagePassSellIn >= 10)
                    {
                        Assert.That(currentBackstagePassQuality == Math.Clamp(previousBackstagePassQuality + 1, 0, 50));
                    }
                }
            }

        }

        //STANDARD CASE
        List<ItemWithIndex> standardItems = this.currentItems.Select((item, index) => new ItemWithIndex { item = item, index = index })
            .Where(ItemWithIndex => !ItemWithIndex.item.Name.Contains("Sulfuras", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.item.Name.Contains("Aged Brie", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.item.Name.Contains("Backstage passes", System.StringComparison.CurrentCulture)).ToList();

        foreach (ItemWithIndex item in standardItems)
        {
            Console.WriteLine(item.item.Name + ", " + item.item.SellIn + ", " + item.item.Quality);

            var currentQuality = item.item.Quality;
            var currentSellIn = item.item.SellIn;
            var previousQuality = previousItems[item.index].Quality;

            //Should not go lower than 0 for quality
            if (previousQuality == 0)
            {
                Assert.That(currentQuality == previousQuality);
            }

            //current quality should be 1 less than previous if not reached sell in, miniumm 0
            if (currentSellIn >= 0)
            {
                Assert.That(currentQuality == Math.Clamp(previousQuality - 1, 0, 50));
            }
            //current quality should be 2 less than previous if not reached sell in, miniumm 0
            else
            {
                Assert.That(currentQuality == Math.Clamp(previousQuality - 2, 0, 50));
            }
        }

    }
}