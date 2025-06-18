using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class ItemWithIndex {
    public Item Item { get; set; }
    public int Index { get; set; }
}

public class GildedRoseTest {
    public const int QUALITY_MAXIMUM = 50;
    public const int QUALITY_MINIMUM = 0;
    public const int QUALITY_SULFURAS = 80;
    public const int QUALITY_CHANGE_PER_DAY = -1;
    public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN = -2;
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE = 1;
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN = 2;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS = 1;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS = 2;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS = 3;
    public const int QUALITY_CHANGE_PER_DAY_CONJURED = QUALITY_CHANGE_PER_DAY * 2;
    public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN_CONJURED = QUALITY_CHANGE_PER_DAY_AFTER_SELLIN * 2;
    public InventoryItemFactory itemFactory = new();

}

[TestFixture]
public class GildedRoseAgedBrieNamingTest : GildedRoseTest {
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp() {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            itemFactory.CreateItem("Very Aged Brie", 5, 7),
            itemFactory.CreateItem("aged brie", 5, 7),
            itemFactory.CreateItem("AGED BRIE", 5, 7),
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases() {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 3; i++) {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}

public class GildedRoseBackstagePassesNamingTest : GildedRoseTest {
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp() {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            itemFactory.CreateItem("Backstage Passes", 15, 20),
            itemFactory.CreateItem("backstage passes", 15, 20),
            itemFactory.CreateItem("BACKSTAGE PASSES", 15, 20),
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases() {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 3; i++) {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}


public class GildedRoseConjuredNamingTest : GildedRoseTest {
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp() {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            itemFactory.CreateItem("conjured", 15, 20),
            itemFactory.CreateItem("CONJURED", 15, 20),
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases() {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 2; i++) {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}


public class GildedRoseSulfurasNamingTest : GildedRoseTest {
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp() {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            itemFactory.CreateItem("Hand of Ragnaros, Sulfuras", 10, 40),
            itemFactory.CreateItem("sulfuras, Hand of Ragnaros", 10, 40),
            itemFactory.CreateItem("SULFURAS, Hand of Ragnaros", 10, 40),
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases() {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 3; i++) {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}

public class GildedRoseQualityTest : GildedRoseTest {
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp() {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            itemFactory.CreateItem("+5 Dexterity Vest", 10, 20),
            itemFactory.CreateItem("Too Old to Sell", 0, 20),
            itemFactory.CreateItem("Don't go below 0 quality", 0, 1),
            itemFactory.CreateItem("Don't go below 0 quality", 0, 0),
            itemFactory.CreateItem("Aged Brie", 5, 5),
            itemFactory.CreateItem("Aged Brie", 1, QUALITY_MAXIMUM-1),
            itemFactory.CreateItem("Aged Brie", 1,QUALITY_MAXIMUM),
            itemFactory.CreateItem("Aged Brie", 0, 2),
            itemFactory.CreateItem("Aged Brie", 0, 1),
            itemFactory.CreateItem("Sulfuras, Hand of Ragnaros", 0, QUALITY_SULFURAS),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, 40),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, QUALITY_MAXIMUM-1),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, QUALITY_MAXIMUM),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, 40),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, 40),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, QUALITY_MAXIMUM-1),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 5, QUALITY_MAXIMUM),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 1, QUALITY_MAXIMUM),
            itemFactory.CreateItem("Backstage passes to a TAFKAL80ETC concert", 0, 0),
            itemFactory.CreateItem("Conjured Mana Cake", 3, 6)

        };

        gildedRoseApp = new GildedRose(currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality })
                            .ToList();  //need deep copy
        gildedRoseApp.UpdateQuality();
        this.currentItems = gildedRoseApp.Items;
    }

    // Basic tests on data: no quality value should be less than zero; no quality value should be greater than 50 (exception Sulfuras)
    [Test]
    public void QualityMaximumExceptSulfuras() {
        int numExpectedQualityGreaterThanMaximumAndNotSulfuras = 0;
        int numActualQualityGreaterThanMaximumAndNotSulfuras = currentItems.Where(item => item.Quality > QUALITY_MAXIMUM
                                                            && (!item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)))
                                                            .Count();
        Assert.That(numExpectedQualityGreaterThanMaximumAndNotSulfuras == numActualQualityGreaterThanMaximumAndNotSulfuras);
    }

    [Test]
    public void QualityMinimumIsZero() {
        int numExpectedQualityLessThanZero = 0;
        int numActualQualityLessThanZero = currentItems.Where(item => item.Quality < 0).Count();
        Assert.That(numExpectedQualityLessThanZero == numActualQualityLessThanZero);
    }

    [Test]
    public void SellInReducedBy1AfterUpdateIfNotSulfuras() {
        //verify that every sellIn has been reduced by 1 if not Sulfuras
        var nonSulfurasItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                                .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                .ToList();

        foreach (ItemWithIndex nonSulfurasItem in nonSulfurasItems) {
            Assert.That(nonSulfurasItem.Item.SellIn == this.previousItems[nonSulfurasItem.Index].SellIn - 1);
        }
    }
    //SPECIAL CASE: Sulfuras
    [Test]
    public void IfQualitySulfurasValueThenNameMustBeSulfuras() {
        int numExpectedSulfurasQualityAndNameNotSulfura = 0;
        int numActualSulfurasQualityAndNameNotSulfura = this.currentItems.Where(item => item.Quality == QUALITY_SULFURAS
                                                            && !item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                            .Count();
        Assert.That(numExpectedSulfurasQualityAndNameNotSulfura == numActualSulfurasQualityAndNameNotSulfura);
    }

    [Test]
    public void SulfurasMustHaveQualitySulfurasAndSellIn0() {
        int numExpectedSulfuraAndQualityNotSulfurasQualityAndSellInNot0 = 0;
        int numActualSulfuraAndQualityNotSulfurasQualityAndSellInNot0 = this.currentItems.Where(item => item.Quality != QUALITY_SULFURAS
                                                            && item.SellIn != 0
                                                            && item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                            .Count();
        Assert.That(numExpectedSulfuraAndQualityNotSulfurasQualityAndSellInNot0 == numActualSulfuraAndQualityNotSulfurasQualityAndSellInNot0);
    }

    //SPECIAL CASE: Aged Brie
    [Test]
    public void AgedBrieIncreasesQualityBy1PerDayBeforeSellIn() {
        List<ItemWithIndex> agedBrieItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                            .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
                                            && ItemWithIndex.Item.SellIn >= 0)
                                            .ToList();

        foreach (ItemWithIndex agedBrie in agedBrieItems) {
            var currentAgedBrieQuality = agedBrie.Item.Quality;
            var previousAgedBrieQuality = previousItems[agedBrie.Index].Quality;

            if (previousAgedBrieQuality == QUALITY_MAXIMUM) {
                Assert.That(currentAgedBrieQuality == previousAgedBrieQuality);
            }
            else {
                Assert.That(currentAgedBrieQuality == Math.Clamp(previousAgedBrieQuality + QUALITY_CHANGE_PER_DAY_AGED_BRIE, QUALITY_MINIMUM, QUALITY_MAXIMUM));

            }
        }
    }

    [Test]
    public void AgedBrieIncreasesQualityBy2PerDayAfterSellIn() {
        List<ItemWithIndex> agedBrieItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                            .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
                                            && ItemWithIndex.Item.SellIn < 0)
                                            .ToList();

        foreach (ItemWithIndex agedBrie in agedBrieItems) {
            var currentAgedBrieQuality = agedBrie.Item.Quality;
            var previousAgedBrieQuality = previousItems[agedBrie.Index].Quality;

            if (previousAgedBrieQuality == QUALITY_MAXIMUM) {
                Assert.That(currentAgedBrieQuality == previousAgedBrieQuality);
            }
            else {
                Assert.That(currentAgedBrieQuality == previousAgedBrieQuality + QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN);
            }
        }
    }

    //SPECIAL CASE: Backstage Passes    
    [Test]
    public void BackstagePassesQualityZeroAfterSellIn() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                        && ItemWithIndex.Item.SellIn < 0)
                                        .ToList();

        foreach (ItemWithIndex backstagePass in backstagePassItems) {
            Assert.That(backstagePass.Item.Quality == 0);
        }

    }

    [Test]
    public void BackstagePassesSellInLessThan5InclusiveQualityIncreaseShouldBe3() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                        && ItemWithIndex.Item.SellIn < 5 //less than 5 as previous day would have been = 5 
                                        && ItemWithIndex.Item.SellIn >= 0)
                                        .ToList();

        foreach (ItemWithIndex backstagePass in backstagePassItems) {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM));
        }

    }

    [Test]
    public void BackstagePassesSellInGreaterThan5LessThan10InclusiveQualityIncreaseShouldBe2() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                        && ItemWithIndex.Item.SellIn >= 5
                                         && ItemWithIndex.Item.SellIn < 10)
                                        .ToList();

        foreach (ItemWithIndex backstagePass in backstagePassItems) {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM));
        }

    }

    [Test]
    public void BackstagePassesSellInGreaterThan10QualityIncreaseShouldBe1() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                         && ItemWithIndex.Item.SellIn >= 10)
                                        .ToList();

        foreach (ItemWithIndex backstagePass in backstagePassItems) {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM));
        }

    }


    //SPECIAL CASE: Conjured Items
    public void ConjuredDecreasesQualityAtDoubleRate() {
        List<ItemWithIndex> conjuredItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                            .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Conjured", System.StringComparison.CurrentCulture))
                                            .ToList();

        foreach (ItemWithIndex conjured in conjuredItems) {
            var currentConjuredQuality = conjured.Item.Quality;
            var previousConjuredQuality = previousItems[conjured.Index].Quality;

            if (previousConjuredQuality == QUALITY_MAXIMUM) {
                Assert.That(currentConjuredQuality == previousConjuredQuality);
            }

            if (conjured.Item.SellIn >= 0) {
                Assert.That(currentConjuredQuality == Math.Clamp(previousConjuredQuality + QUALITY_CHANGE_PER_DAY_CONJURED, QUALITY_MINIMUM, QUALITY_MAXIMUM));
            }
            else {
                Assert.That(currentConjuredQuality == Math.Clamp(previousConjuredQuality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN_CONJURED, QUALITY_MINIMUM, QUALITY_MAXIMUM));
            }
        }
    }


    //STANDARD CASE
    [Test]
    public void QualityDecreasedBy1BeforeSellIn() {
        List<ItemWithIndex> standardItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
            .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Conjured", System.StringComparison.CurrentCulture)
            && ItemWithIndex.Item.SellIn >= 0)
            .ToList();

        foreach (ItemWithIndex standardItem in standardItems) {
            Assert.That(standardItem.Item.Quality == Math.Clamp(previousItems[standardItem.Index].Quality + QUALITY_CHANGE_PER_DAY, QUALITY_MINIMUM, QUALITY_MAXIMUM));
        }

    }

    public void QualityDecreasedBy2AfterSellIn() {
        List<ItemWithIndex> standardItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
            .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
            && ItemWithIndex.Item.SellIn < 0)
            .ToList();

        foreach (ItemWithIndex standardItem in standardItems) {
            Assert.That(standardItem.Item.Quality == Math.Clamp(previousItems[standardItem.Index].Quality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN, QUALITY_MINIMUM, QUALITY_MAXIMUM));
        }

    }




}
