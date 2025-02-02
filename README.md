# Playwright Test Suite - LOOPQA Technical Evaluation

## Overview

This repository contains a Playwright-driven test suite created for the LOOPQA technical evaluation. The objective was to automate the testing of a demo application, ensuring that the test cases cover login functionality, navigation, task validation, and the proper display of tags across different application columns.

The tests are driven by data provided in a JSON-like format, allowing for scalability and reusability across different test scenarios.

The following test cases are included in the evaluation:

- **Test Case 1**: Validate the "Implement user authentication" task in the "To Do" column in the "Web Application" section.
- **Test Case 2**: Validate the "Fix navigation bug" task in the "To Do" column in the "Web Application" section.
- **Test Case 3**: Validate the "Design system updates" task in the "In Progress" column in the "Web Application" section.
- **Test Case 4**: Validate the "Push notification system" task in the "To Do" column in the "Mobile Application" section.
- **Test Case 5**: Validate the "Offline mode" task in the "In Progress" column in the "Mobile Application" section.
- **Test Case 6**: Validate the "App icon design" task in the "Done" column in the "Mobile Application" section.

## File Structure

- **TestBase.cs**: Contains helper methods for logging in, navigating to applications, and verifying task columns.
- **TestSuite.cs**: Contains the actual test cases and test logic.
- **appsettings.json**: Stores configuration settings such as URL, username, and password for login.

## How It Works

- The tests are driven by parameterized inputs, allowing for easy scalability as more tasks or columns are added to the system.
- Playwright is used to simulate user interactions with the demo application in Chromium, ensuring that each scenario runs as expected.
- Each test case includes login automation, navigation, and task verification along with tag checks.
