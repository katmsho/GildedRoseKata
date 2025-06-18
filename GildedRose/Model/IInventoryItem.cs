using GildedRoseKata;

public class IInventoryItem : Item {
    public const int QUALITY_MAXIMUM = 50;
    public const int QUALITY_MINIMUM = 0;
    public const int QUALITY_CHANGE_PER_DAY = -1;
    public const int QUALITY_CHANGE_PER_DAY_AFTER_SELLIN = -2;
    public virtual void UpdateQuality() { }

}
