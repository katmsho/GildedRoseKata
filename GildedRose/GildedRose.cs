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
            //SPECIAL CASE
            //if Sulfuras, no change so skip this iteration of loop
            if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
            {
                continue;
            }
            
             Items[i].SellIn = Items[i].SellIn - 1;  

            //if standard item (i.e. not aged brie or backstage pass) reduce quality by 1
            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                Items[i].Quality = Math.Clamp(Items[i].Quality - 1, 0, 50);
            }
            else // must be aged brie or backstage pass
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1; // add 1 to quality always to aged brie and backstage pass

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 10)
                        {
                            Items[i].Quality = Math.Clamp(Items[i].Quality + 1, 0, 50); // if backstage pass and 10 days or less add extra 1 to quality                            
                        }

                        if (Items[i].SellIn < 5)
                        {
                            Items[i].Quality = Math.Clamp(Items[i].Quality + 1, 0, 50); // if backstage pass and 5 days or less add extra 1 to quality                            
                        }
                    }
                }
            }      
 
            if (Items[i].SellIn < 0) //Past SellIn date so reduce quality by 1 again
            {
                if (Items[i].Name != "Aged Brie")
                {
                    if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                         Items[i].Quality = Math.Clamp(Items[i].Quality - 1, 0, 50); //reduce by 1 if not agedBrie or backstage pass 
                    }
                    else
                    {
                        Items[i].Quality = 0; // set to zero if is backstage pass
                    }
                }
                else
                {
                    Items[i].Quality = Math.Clamp(Items[i].Quality + 1, 0, 50); //if Aged Brie add 1 to quality  
                }
            }
        }
    }
}