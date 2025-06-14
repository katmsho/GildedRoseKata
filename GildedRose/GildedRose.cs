using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    public IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            //SPECIAL CASE Sulfuras
            //if Sulfuras, no change 
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                continue;
            }

            item.SellIn = item.SellIn - 1;

            //SPECIAL CASE Aged Brie
            //increases in quality +1 before and on SellIn, +2 after sellIn
            if (item.Name == "Aged Brie")
            {
                HandleAgedBrie(item);
                continue;
            }

            //SPECIAL CASE Backstage pass
            //Quality increases by as its SellIn value approaches
            //Quality increases by 2 when there are 10 days or less 
            //Quality increases by 3 when there are 5 days or less 
            //Quality drops to 0 after the concert
            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                HandleBackstagePass(item);
                continue;
            }

            //standard item
            if (item.SellIn >= 0)
            {
                item.Quality = Math.Clamp(item.Quality - 1, 0, 50);
            }
            else
            {
                item.Quality = Math.Clamp(item.Quality - 2, 0, 50);
            }

        }
    }

    private void HandleAgedBrie(Item item)
    {
        if (item.SellIn >= 0)
        {
            item.Quality = Math.Clamp(item.Quality + 1, 0, 50);
        }
        else
        {
            item.Quality = Math.Clamp(item.Quality + 2, 0, 50);
        }
    }

    private void HandleBackstagePass(Item item)
    {
        if (item.SellIn >= 0)
        {
            if (item.SellIn >= 10)
            {
                item.Quality = Math.Clamp(item.Quality + 1, 0, 50);
            }
            if (item.SellIn < 10 && item.SellIn >= 5)
            {
                item.Quality = Math.Clamp(item.Quality + 2, 0, 50);
            }
            if (item.SellIn < 5)
            {
                item.Quality = Math.Clamp(item.Quality + 3, 0, 50);
            }
        }
        else
        { //is after SellIn
            item.Quality = 0;
        }
    }
}