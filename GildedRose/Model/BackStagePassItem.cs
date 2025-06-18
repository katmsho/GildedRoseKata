using System;

public class BackstagePassItem : IInventoryItem {

    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS = 1;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS = 2;
    public const int QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS = 3;

    public BackstagePassItem(string Name, int SellIn, int Quality) {
        this.Name = Name;
        this.SellIn = SellIn;
        this.Quality = Quality;
    }

    /// <summary>
    ///Quality increases by 1 as its SellIn value approaches
    ///Quality increases by 2 when there are 10 days or less 
    ///Quality increases by 3 when there are 5 days or less 
    ///Quality drops to 0 after SellIn
    /// </summary>
    public override void UpdateQuality() {
        this.SellIn -= 1;

        if (this.SellIn >= 0) {
            if (this.SellIn >= 10) {
                this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
            if (this.SellIn < 10 && this.SellIn >= 5) {
                this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_5_TO_10_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
            if (this.SellIn < 5) {
                this.Quality = Math.Clamp(this.Quality + QUALITY_CHANGE_PER_DAY_BACKSTAGE_PASS_0_TO_5_DAYS, QUALITY_MINIMUM, QUALITY_MAXIMUM);
            }
        }
        else { //is after SellIn
            this.Quality = 0;
        }


    }

}