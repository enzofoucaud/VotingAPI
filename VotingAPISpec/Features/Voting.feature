Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers
Link to a feature: [Calculator](VotingAPISpec/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

Scenario: Count candidates
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 18   |
	Then candidates should be 2

Scenario: Verify ages with correct ages
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 23   |
	Then ages should be Tout le monde peut participer

Scenario: Verify ages with incorrect ages
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 17   |
	Then ages should be Error: Yanis ne peut pas participer car il a 17

Scenario: Voting closed and get winner on first round
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 18   |
	And new round
	And give "8" vote to "Enzo"
	And give "2" vote to "Yanis"
	When close voting
	And the winner is "Enzo"

Scenario: Voting not closed and get winner on first round
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 18   |
	And new round
	And give "8" vote to "Enzo"
	And give "2" vote to "Yanis"
	When the winner is "Error: Le vote n'est pas clos"

Scenario: Voting closed and get winner on second round
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 18   |
		| John  | 18   |
	And new round
	And give "4" vote to "Enzo"
	And give "4" vote to "Yanis"
	And give "2" vote to "John"
	When close voting
	And the winner is "Aucuns vainqueurs sur le premier tour, tour suivant"
	Given new round
	And give "8" vote to "Enzo"
	And give "2" vote to "Yanis"
	When close voting
	And the winner is "Enzo"

Scenario: Voting closed and no winner on second round (vote equality)
	Given new candidates
		| Names | Ages |
		| Enzo  | 18   |
		| Yanis | 18   |
		| John  | 18   |
	And new round
	And give "4" vote to "Enzo"
	And give "4" vote to "Yanis"
	And give "2" vote to "John"
	When close voting
	And the winner is "Aucuns vainqueurs sur le premier tour, tour suivant"
	Given new round
	And give "8" vote to "Enzo"
	And give "8" vote to "Yanis"
	When close voting
	And the winner is "Les candidats sont à égalités. Stop!"

Scenario: Voting closed and no winner on second round (percent equality)
	Given new candidates
		| Names | Ages |
		| Enzo  | 21   |
		| Yanis | 22   |
		| John  | 20   |
	And new round
	And give "3" vote to "Enzo"
	And give "2" vote to "Yanis"
	And give "2" vote to "John"
	When close voting
	And the winner is "Aucuns vainqueurs sur le premier tour, tour suivant"
	Given new round
	And give "2" vote to "Enzo"
	And give "8" vote to "Yanis"
	When close voting
	And the winner is "Yanis"