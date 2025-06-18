using System;
public class StandardItem : IInventoryItem {

    public StandardItem(string Name, int SellIn, int Quality) {
        this.Name = Name;
        this.SellIn = SellIn;
        this.Quality = Quality;
    }

    /// <summary>
    ///Quality decreases by 1 as its SellIn value approaches
    ///Quality decreases by 1 after SellIn
    /// </summary>
    public override void UpdateQuality() {
        this.SellIn -= 1;

        if (this.SellIn >= 0) {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
        else {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }

    }
}