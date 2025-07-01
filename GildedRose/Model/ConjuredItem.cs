using System;

public class ConjuredItem : IInventoryItem {

    public const int QUALITY_CHANGE_PER_DAY_CONJURED = QUALITY_CHANGE_PER_DAY * 2;
    public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN_CONJURED = QUALITY_CHANGE_PER_DAY_AFTER_SELLIN * 2;

    public ConjuredItem(string Name, int SellIn, int Quality) {
        this.Name = Name;
        this.SellIn = SellIn;
        this.Quality = Quality;
    }

    /// <summary>
    ///Quality decreases by double standard as its SellIn value approaches
    ///Quality decreases by double standard after SellIn
    /// </summary>
    public override void UpdateQuality() {
        this.SellIn -= 1;

        if (this.SellIn >= 0) {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_CONJURED, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
        else {
            this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_AFTER_SELLIN_CONJURED, QUALITY_MINIMUM, QUALITY_MAXIMUM);
        }
    }
}