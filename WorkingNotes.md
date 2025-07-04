# Changes

- 12/06/25 Initial notes, assumptions and todo list after reading requirements: https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/main/GildedRoseRequirements.md
- 13/06/25 Draft of tests, updated assumptions and todo list. Decided to have one set of items covering all edge cases so only need to run one update cycle instead of running mutliple days and looking at output
- 13/06/25 Split tests into separate classes / tests. Created constants for numerical values.
- 14/06/25 Refactored program to deal with special cases first and exit that iteration of loop as early as possible. The makes the code more readable and easier to understand rather than having multiple repeated nested if statements checking the names. Also moved to have specific statements to add/minus 1/2/3 to quality as required, rather than repeatedly adding +1/-1, again for readability/maintainability. It is easier to see what it happening when each situation is handled by a specific piece of code, rather than spread out across multiple if blocks.
- 18/06/25 Refactored to use factory, interface with concrete classes defining the appropriate update method. Would have ben nice to use the Items class and update the property, but not allowed in terms of this Kata.
- 18/06/35 Added Conjured Item type


# Notes
- Could implement logging, validation etc but not the point of this kata
- Now got basic code refactored & understandable consider patterns / factory. With current number of 'special cases' not really necessary, but already have one change request for another (conjured items) and will inevitably be more.  In real world would use change request to lobby for getting some extra time for program refactor so would stop having 'special cases' (the number of which will only increase) and implement a consistent factory / strategy pattern.  I'd also try to get some work onto The Goblins backlog for improvements to the Items class to make the classes / interface a bit more streamlined.


# Assumptions 
Would normally verify these with product/client.

- Quality reduces by 1 per day on standard items before the sellin date
- Quality reduces by 2 per day on standard items after the sellin date
- Once quality reaches zero it cannot be sold
- ~~Aged Brie reduces in quality after the sellin is reached, as per standard items (even aged cheese has a limit!)~~ Incorrect - Aged Brie increases by 2 after SellIn reaches zero
- Aged Brie will be defined as having the exact string 'Aged Brie' as the name of the item. Any other text or change of spelling/capitalisation will not trigger the special handling.
- Sulfuras will be defined as having the exact string 'Sulfuras' at the beginning of the item name. Any change of spelling/capitalisation will not trigger the special handling. Any additional text after 'Sulfuras' will be ignored. 
- Sulfuras quality is always 80 and does not change
- Sulfuras sellIn is always 0 as it is legendary and is not in stock
- Backstage passes will be defined as having the exact string 'Backstage passes' at the beginning of the item name. Any change of spelling/capitalisation will not trigger the special handling. Any additional text after 'Backstage passes' will be ignored. 
- Tests on the Item class - reasonable inital values, name length etc need to be written by the goblin as the owner of that code
- Should SellIn date roughly align to when the Quality approaches zero? No information given to know this, so will not test for it.


# TODO

- ~~Conjured items. Will do as change request after initial requirements are tested and delivered~~
- ~~Split out asserts into separate tests~~
- ~~Name tests expressively~~
- ~~Code should look for 'Backstage passes' not full Name~~
- ~~Verify whether the days/quality change for the backstage passes change on the SellIn date or the day after. Will assume what code does is correct.~~  On  SellIn=0 is lthe last day for +3, SellIn=5 for +2, SellIn=10 for +1

## Tests

- Special Case Sulfuras
  - quality always =80
  - sellin always =0

- Special Case Aged Brie
  - sellin >0  quality +1
  - sellin <=0 quality +2

- Special Case Backstage Pass
  - sellin >10  quality +1
  - <= 10  sellin >5  quality +2
  - <= 5 sellin >0 quality +3
  - sellin =0 quality = 0

- Quality Maximun
  - Excluding Sulfuras, quality maximum = 50

- Quality Minimum
  - Quality minimum = 0
  - no exceptions


- ~~Sellin Minimum~~
  - ~~Sellin minimum = 0~~
  - ~~no exceptions~~ 
  - Incorrect: SellIn keeps reducing past 0. Presumably these would get removed.







