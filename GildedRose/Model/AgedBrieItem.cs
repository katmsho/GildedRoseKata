using System;
public class AgedBrieItem : IInventoryItem {
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE = 1;
    public const int QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN = 2;

    public AgedBrieItem(string Name, int SellIn, int Quality) {
        this.Name = Name;
        this.SellIn = SellIn;
        this.Quality = Quality;
    }

    /// <summary>
    /// increases in quality +1 before and on SellIn, +2 after sellIn
    /// </summary>
    public override void UpdateQuality() {
        this.SellIn -= 1;

        if (this.SellIn >= 0) {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_AGED_BRIE, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
        else {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_AGED_BRIE_AFTER_SELLIN, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }

    }
}
