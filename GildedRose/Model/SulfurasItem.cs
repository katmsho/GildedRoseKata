public class SulfurasItem : IInventoryItem {

    public const int QUALITY_SULFURAS = 80;

    public SulfurasItem(string Name, int SellIn) {
        this.Name = Name;
        this.SellIn = SellIn;
        this.Quality = QUALITY_SULFURAS;
    }

    /// <summary>
    /// Does not change Quality or SellIn values
    /// </summary>
    public override void UpdateQuality() {
        return;
    }
}