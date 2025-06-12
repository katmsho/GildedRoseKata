# Notes

12/06/25 Initial notes, assumptions and todo list after reading requirements: https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/main/GildedRoseRequirements.md

# Assumptions 
Would normally verify these with product/client.

- Quality reduces by 1 per day on standard items before the sellin date
- Quality reduces by 2 per day on standard items after the sellin date
- Once quality reaches zero it cannot be sold
- Aged Brie reduces in quality after the sellin is reached, as per standard items (even aged cheese has a limit!)
- Aged Brie will be defined as having the exact string 'Aged Brie' as the name of the item. Any other text or change of spelling/capitalisation will not trigger the special handling.
- Sulfurus will be defined as having the exact string 'Sulfurus' at the beginning of the item name. Any change of spelling/capitalisation will not trigger the special handling. Any additional text after 'Sulfurus' will be ignored. 
- Sulfurus quality is always 80 and does not change
- Sulfurus sellin is always 0 as it is legendary and is not in stock
- Backstage passes will be defined as having the exact string 'Backstage passes'  at the beginning of the item name. Any change of spelling/capitalisation will not trigger the special handling. Any additional text after 'Backstage passes' will be ignored. 
- Tests on the Item class - reasonable inital values, name length etc need to be written by the goblin as the owner of that code


# TODO

- Conjured items. Will do as change request after initial requirements are tested and delivered

## Tests

- Special Case Sulfurus
-- quality always =80
-- sellin always =0

- Special Case Aged Brie
-- sellin >0  quality +1
-- sellin <=0 quality -2

- Special Case Backstage Pass
-- sellin >10  quality +1
-- <= 10  sellin >5  quality +2
-- <= 5 sellin >0 quality +3
-- sellin =0 quality = 0

- Quality Maximun
-- Excluding Sulfurus, quality maximum = 50

- Quality Minimum
-- Quality minimum = 0
-- no exceptions

- Sellin Minimum
-- Sellin minimum = 0
-- no exceptions







