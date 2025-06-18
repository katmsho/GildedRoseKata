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

        foreach (IInventoryItem item in this.Items) {
            item.UpdateQuality();
        }
    }

}