Feature: BookingFilters
	In order test the new results filters
	I want to search and refine using these filters

Scenario: Search Result Filter Test
	Given User is on the booking.com homepage
	And User perform a Search
	| Location | NumberOfPeople | NumberOfRooms |
	| Limerick | 2              | 1             |
	When User filters by <Filter> and selects the <Option> filter
	Then <Hotel Name> listed in the results = <Is Listed>
	Examples:
	| Filter           | Option | Hotel Name            | Is Listed |
	| Fun things To Do | Sauna  | Limerick Strand Hotel | True      |
	| Fun things To Do | Sauna  | George Hotel Limerick | False     |
	| Star Rating      | 5 Star | The Savoy Hotel       | True      |
	| Star Rating      | 5 Star | George Hotel Limerick | False     |

#Scenario: Sauna Filter - negative
#	Given User is on the booking.com homepage
#	And searches for 'Limerick'
#	When User filters by 'Fun Things To Do' and selects the 'Sauna' filter
#	Then 'George Limerick Hotel' is not in the results
#
#Scenario: 5 star Filter - positive
#	Given User is on the booking.com homepage
#	And searches for 'Limerick'
#	When User filters by 'Star Rating' and selects the '5 stars' filter
#	Then 'The Savoy Hotel' is in the results
#
#Scenario: 5 star Filter - negative
#	Given User is on the booking.com homepage
#	And searches for 'Limerick'
#	When User filters by 'Star Rating' and selects the '5 stars' filter
#	Then 'George Limerick Hotel' is not in the results