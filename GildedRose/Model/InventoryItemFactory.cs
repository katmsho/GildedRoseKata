
using GildedRoseKata;

public class InventoryItemFactory {

    public Item CreateItem(string Name, int SellIn, int Quality) {

        if (Name.StartsWith("Sulfuras")) {
            return new SulfurasItem(Name, SellIn);
        }
        if (Name.StartsWith("Aged Brie")) {
            return new AgedBrieItem(Name, SellIn, Quality);
        }
        if (Name.StartsWith("Backstage passes")) {
            return new BackstagePassItem(Name, SellIn, Quality);
        }

        return new StandardItem(Name, SellIn, Quality);

    }
}