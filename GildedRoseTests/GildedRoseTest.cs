using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;
 
public class ItemWithIndex
{
    public Item Item { get; set; }
    public int Index { get; set; }

}

public class GildedRoseTest
{
   public const int QUALITY_MAXIMUM = 50;
   public const int QUALITY_MINIMUM = 0;
   public const int QUALITY_SULFURAS = 80;
   public const int QUALITY_CHANGE_PER_DAY = -1;
   public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN = -2;
  public  const int QUALITY_CHANGE_PER_DAY_AGED_BRIE = 1;
  public  const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS = 1;
   public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS = 2;
  public  const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS = 3;


}

[TestFixture]
public class  GildedRoseAgedBrieNamingTest : GildedRoseTest
{
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            new Item {Name = "Very Aged Brie", SellIn = 5, Quality = 7},
            new Item {Name = "aged brie", SellIn = 5, Quality = 7},
            new Item {Name = "AGED BRIE", SellIn = 5, Quality = 7},
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases()
    {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 3; i++)
        {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}

public class GildedRoseSulfurasNamingTest : GildedRoseTest
{
    private GildedRose gildedRoseApp { get; set; }    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            new Item {Name = "Hand of Ragnaros, Sulfuras", SellIn = 10, Quality = 40},
            new Item {Name = "sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 40},
            new Item {Name = "SULFURAS, Hand of Ragnaros", SellIn = 10, Quality = 40},
        };

        gildedRoseApp = new GildedRose(this.currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality }).ToList(); //need deep copy
        gildedRoseApp.UpdateQuality();
    }

    [Test]
    public void NamingOfSpecialCases()
    {
        this.currentItems = gildedRoseApp.Items;

        for (int i = 0; i < 3; i++)
        {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}

public class GildedRoseBackstagePassesNamingTest  : GildedRoseTest 
{
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
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

        for (int i = 0; i < 3; i++)
        {
            Assert.That(this.currentItems[i].Name == this.previousItems[i].Name);
            Assert.That(this.currentItems[i].SellIn == this.previousItems[i].SellIn - 1);
            Assert.That(this.currentItems[i].Quality == this.previousItems[i].Quality + QUALITY_CHANGE_PER_DAY);
        }
    }
}



public class GildedRoseQualityTest  : GildedRoseTest
{
    private GildedRose gildedRoseApp { get; set; }
    private IList<Item> currentItems { get; set; }
    private IList<Item> previousItems { get; set; }

    [SetUp]
    public void SetUp()
    {
        this.previousItems = [];
        this.currentItems = new List<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Too Old to Sell", SellIn = 0, Quality = 20},
            new Item {Name = "Don't go below 0 quality", SellIn = 0, Quality = 1},
            new Item {Name = "Don't go below 0 quality", SellIn = 0, Quality = 0},
            new Item {Name = "Aged Brie", SellIn = 1, Quality = QUALITY_MAXIMUM-1},
            new Item {Name = "Aged Brie", SellIn = 1, Quality = QUALITY_MAXIMUM},
            new Item {Name = "Aged Brie", SellIn = 0, Quality = 2},
            new Item {Name = "Aged Brie", SellIn = 0, Quality = 1},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = QUALITY_SULFURAS},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 40},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = QUALITY_MAXIMUM-1},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = QUALITY_MAXIMUM},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 40},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = QUALITY_MAXIMUM-1},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = QUALITY_MAXIMUM},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = QUALITY_MAXIMUM},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 0}
        };

        gildedRoseApp = new GildedRose(currentItems);
        this.previousItems = this.currentItems.Select(item => new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality })
                            .ToList();  //need deep copy
        gildedRoseApp.UpdateQuality();
        this.currentItems = gildedRoseApp.Items;
    }

// Basic tests on data: no quality value should be less than zero; no quality value should be greater than 50 (exception Sulfuras); Sulfurus quality = 80
    [Test]
    public void QualityMaximumExceptSulfuras()
    { 
        int numExpectedQualityGreaterThanMaximumAndNotSulfuras = 0;
        int numActualQualityGreaterThanMaximumAndNotSulfuras = currentItems.Where(item => item.Quality > QUALITY_MAXIMUM
                                                            && (!item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)))
                                                            .Count();
        Assert.That(numExpectedQualityGreaterThanMaximumAndNotSulfuras == numActualQualityGreaterThanMaximumAndNotSulfuras);
    }

    [Test]
    public void QualityMinimumIsZero()
    { 
        int numExpectedQualityLessThanZero = 0;
        int numActualQualityLessThanZero = currentItems.Where(item => item.Quality < 0).Count();
        Assert.That(numExpectedQualityLessThanZero == numActualQualityLessThanZero);
    }

    [Test]
    public void SellInReducedBy1AfterUpdateIfNotSulfuras()
    {
        //verify that every sellIn has been reduced by 1 if not Sulfuras
        var nonSulfurasItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                                .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                .ToList();

        foreach (ItemWithIndex nonSulfurasItem in nonSulfurasItems)
        { 
            Assert.That(nonSulfurasItem.Item.SellIn == this.previousItems[nonSulfurasItem.Index].SellIn - 1);
        }
    }
    //SPECIAL CASE: Sulfuras
    [Test]
    public void IfQualitySulfurasValueThenNameMustBeSulfuras()
    {
        int numExpectedSulfurasQualityAndNameNotSulfura = 0;
        int numActualSulfurasQualityAndNameNotSulfura = this.currentItems.Where(item => item.Quality == QUALITY_SULFURAS 
                                                            && !item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                            .Count(); 
        Assert.That(numExpectedSulfurasQualityAndNameNotSulfura == numActualSulfurasQualityAndNameNotSulfura);   
    }

    [Test]
    public void SulfurasMustHaveQualitySulfurasAndSellIn0()
    { 
        int numExpectedSulfuraAndQualityNotSulfurasQualityAndSellInNot0 = 0;
        int numActualSulfuraAndQualityNotSulfurasQualityAndSellInNot0 = this.currentItems.Where(item => item.Quality != QUALITY_SULFURAS
                                                            && item.SellIn != 0
                                                            && item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture))
                                                            .Count(); 
        Assert.That(numExpectedSulfuraAndQualityNotSulfurasQualityAndSellInNot0 == numActualSulfuraAndQualityNotSulfurasQualityAndSellInNot0);   
    }

    //SPECIAL CASE: Aged Brie
    [Test]
    public void AgedBrieIncreasesQualityBy1PerDay()
    { 
        List<ItemWithIndex> agedBrieItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                            .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture))
                                            .ToList();

        foreach (ItemWithIndex agedBrie in agedBrieItems)
        {
            var currentAgedBrieQuality = agedBrie.Item.Quality;
            var previousAgedBrieQuality = previousItems[agedBrie.Index].Quality;

            if (agedBrie.Item.SellIn > 0) // aged brie past sellIn is covered on standard past-sellIn test
            {
                if (previousAgedBrieQuality == QUALITY_MAXIMUM)
                {
                    Assert.That(currentAgedBrieQuality == previousAgedBrieQuality);
                }
                else
                {
                    Assert.That(currentAgedBrieQuality == previousAgedBrieQuality + QUALITY_CHANGE_PER_DAY_AGED_BRIE);
                }
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

        foreach (ItemWithIndex backstagePass in backstagePassItems)
        {
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
                                                
        foreach (ItemWithIndex backstagePass in backstagePassItems)
        {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS, 0, QUALITY_MAXIMUM));
        }

    }

    [Test]
    public void BackstagePassesSellInGreaterThan5LessThan10InclusiveQualityIncreaseShouldBe2() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                        && ItemWithIndex.Item.SellIn >= 5
                                         && ItemWithIndex.Item.SellIn < 10)
                                        .ToList();
                                                
        foreach (ItemWithIndex backstagePass in backstagePassItems)
        {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS, 0, QUALITY_MAXIMUM));
        }

    }

    [Test]
    public void BackstagePassesSellInGreaterThan10QualityIncreaseShouldBe1() {
        List<ItemWithIndex> backstagePassItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
                                        .Where(ItemWithIndex => ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
                                         && ItemWithIndex.Item.SellIn >= 10)
                                        .ToList();
                                                
        foreach (ItemWithIndex backstagePass in backstagePassItems)
        {
            Assert.That(backstagePass.Item.Quality == Math.Clamp(previousItems[backstagePass.Index].Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS, 0, QUALITY_MAXIMUM));
        }

    }

    //STANDARD CASE
    [Test]
    public void QualityDecreasedBy1BeforeSellIn() {
        List<ItemWithIndex> standardItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
            .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
            && ItemWithIndex.Item.SellIn >= 0 )
            .ToList();

        foreach (ItemWithIndex standardItem in standardItems)
        {
             Assert.That(standardItem.Item.Quality == Math.Clamp(previousItems[standardItem.Index].Quality + QUALITY_CHANGE_PER_DAY, 0, QUALITY_MAXIMUM));
        }

    }

    public void QualityDecreasedBy2AfterSellIn() {
        List<ItemWithIndex> standardItems = this.currentItems.Select((item, index) => new ItemWithIndex { Item = item, Index = index })
            .Where(ItemWithIndex => !ItemWithIndex.Item.Name.StartsWith("Sulfuras", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Aged Brie", System.StringComparison.CurrentCulture)
            && !ItemWithIndex.Item.Name.StartsWith("Backstage passes", System.StringComparison.CurrentCulture)
            && ItemWithIndex.Item.SellIn < 0 )
            .ToList();

        foreach (ItemWithIndex standardItem in standardItems)
        {
             Assert.That(standardItem.Item.Quality == Math.Clamp(previousItems[standardItem.Index].Quality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN, 0, QUALITY_MAXIMUM));
        }

    }




}
