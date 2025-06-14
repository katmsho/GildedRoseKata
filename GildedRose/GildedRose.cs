using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose {
    public IList<Item> Items;

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
            item.Quality = Math.Clamp(item.Quality + 1, 0, 50);
        }
        else {
            item.Quality = Math.Clamp(item.Quality + 2, 0, 50);
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
                item.Quality = Math.Clamp(item.Quality + 1, 0, 50);
            }
            if (item.SellIn < 10 && item.SellIn >= 5) {
                item.Quality = Math.Clamp(item.Quality + 2, 0, 50);
            }
            if (item.SellIn < 5) {
                item.Quality = Math.Clamp(item.Quality + 3, 0, 50);
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
            item.Quality = Math.Clamp(item.Quality - 1, 0, 50);
        }
        else {
            item.Quality = Math.Clamp(item.Quality - 2, 0, 50);
        }
    }

}