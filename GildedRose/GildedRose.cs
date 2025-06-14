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
        for (var i = 0; i < Items.Count; i++)
        {
            //SPECIAL CASE Sulfuras
            //if Sulfuras, no change 
            if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
            {
                continue;
            }

            Items[i].SellIn = Items[i].SellIn - 1;

            //SPECIAL CASE Aged Brie
            //increases in quality +1 before and on SellIn, +2 after sellIn
            if (Items[i].Name == "Aged Brie")
            {
                HandleAgedBrie(Items[i]);

                continue;
            }

            //SPECIAL CASE Backstage pass
            //Quality increases by as its SellIn value approaches
            //Quality increases by 2 when there are 10 days or less 
            //Quality increases by 3 when there are 5 days or less 
            //Quality drops to 0 after the concert
            if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                HandleBackstagePass(Items[i]);
                continue;
            }

            //standard item
            if (Items[i].SellIn >= 0)
            {
                Items[i].Quality = Math.Clamp(Items[i].Quality - 1, 0, 50);
            }
            else
            {
                Items[i].Quality = Math.Clamp(Items[i].Quality - 2, 0, 50);
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
        else //is after SellIn
        {
            item.Quality = 0;
        }
    }
}