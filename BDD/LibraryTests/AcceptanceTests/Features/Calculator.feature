Feature: CalculatorOperations
        As a calculator user
        To obtain the result of math oparations
        I want to make sure I can add and multiply two integer numbers

@Calculator @Add @WI115
Scenario: Add two numbers
        Given a first number 1
        And a second number 2
        When two numbers are added
        Then the result must be 3

@Calculator @Multiply @WI115
Scenario: Multiply two numbers
        Given a first number 1
        And a second number 1
        When two numbers are mutiplied
        Then the result must be 1