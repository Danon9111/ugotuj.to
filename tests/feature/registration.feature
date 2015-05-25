@registration
Feature: New User Registration
  As a New User
  I want to register to the service

  Scenario: New User is able to see registration form
    Given I open the registration page
    Then I will see registration form

  Scenario: New User is able to register
    Given I open the registration page
    When I provide following data
      | field | value |
      | e-mail | a@a.a |
      | login | test1 |
      | password | 1234 |
      | password2 | 1234 |
    And I will submit form
    Then I am registered in the service

  Scenario: New User provide e-mail which already exist
    Given I open the registration page
    When I provide following data
      | field | value |
      | e-mail | a@a.a |
      | login | test2 |
      | password | 1234 |
      | password2 | 1234 |
    And I will submit form
    Then Error message is visible

  Scenario: New User provide login which already exist
    Given I open the registration page
    When I provide following data
      | field | value |
      | e-mail | ab@a.a |
      | login | test1 |
      | password | 1234 |
      | password2 | 1234 |
    And I will submit form
    Then Error message is visible
