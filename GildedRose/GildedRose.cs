using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose {
    public IList<Item> Items;
    public const int QUALITY_MAXIMUM = 50;
    public const int QUALITY_MINIMUM = 0;
    public const int QUALITY_CHANGE_PER_DAY = -1;
    public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN = -2;
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE = 1;
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN = 2;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS = 1;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS = 2;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS = 3;

    public GildedRose(IList<Item> Items) {
        this.Items = Items;
    }

    public void UpdateQuality() {

        foreach (Item item in this.Items) {

            //SPECIAL CASE Sulfuras
            //if Sulfuras, no change 
            if (item.Name.StartsWith("Sulfuras")) {
                continue;
            }

            item.SellIn = item.SellIn - 1;

            //SPECIAL CASE Aged Brie
            if (item.Name.StartsWith("Aged Brie")) {
                HandleAgedBrie(item);
                continue;
            }

            //SPECIAL CASE Backstage pass
            if (item.Name.StartsWith("Backstage passes")) {
                HandleBackstagePass(item);
                continue;
            }

            //must be standard item
            HandleStandardItem(item);
        }
    }

    /// <summary>
    /// increases in quality +1 before and on SellIn, +2 after sellIn
    /// </summary>
    private void HandleAgedBrie(Item item) {
        if (item.SellIn >= 0) {
            item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_AGED_BRIE, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
        else {
            item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
    }

    /// <summary>
    ///Quality increases by 1 as its SellIn value approaches
    ///Quality increases by 2 when there are 10 days or less 
    ///Quality increases by 3 when there are 5 days or less 
    ///Quality drops to 0 after SellIn
    /// </summary>
    private void HandleBackstagePass(Item item) {
        if (item.SellIn >= 0) {
            if (item.SellIn >= 10) {
                item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
            if (item.SellIn < 10 && item.SellIn >= 5) {
                item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
            if (item.SellIn < 5) {
                item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
        }
        else { //is after SellIn
            item.Quality = 0;
        }
    }

    /// <summary>
    ///Quality decreases by 1 as its SellIn value approaches
    ///Quality decreases by 1 after SellIn
    /// </summary>
    private void HandleStandardItem(Item item) {
        if (item.SellIn >= 0) {
            item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
        else {
            item.Quality = Math.Clamp(item.Quality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
    }

}