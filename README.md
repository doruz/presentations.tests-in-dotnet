# Tests in .NET

## Resources

- Links
  - [Microsoft - Testing in .NET](https://learn.microsoft.com/en-us/dotnet/core/testing/)
  - [Microsoft - Best practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- Books
  - [The Art of Unit Testing: with examples in C# ](https://www.amazon.com/Art-Unit-Testing-examples-ebook/dp/B097826FLN/)
  - [Unit Testing Principles, Practices, and Patterns](https://www.amazon.com/Unit-Testing-Principles-Practices-Patterns/dp/1617296279)

## Unit Tests

A **unit test** is a test that exercises individual software components or methods, also known as a "unit of work". Unit tests should only test code within the developer's control. They don't test infrastructure concerns. Infrastructure concerns include interacting with databases, file systems, and network resources.

### Why should we write unit tests?

- protection against regression.
- having good unit tests, can act as documentation.
- increases code quality.

### Characteristics of a good unit test

- **Fast**: It isn't uncommon for mature projects to have thousands of unit tests. Unit tests should take little time to run. Milliseconds.
- **Isolated**: Unit tests are standalone, can be run in isolation, and have no dependencies on any outside factors such as a file system or database.
- **Repeatable**: Running a unit test should be consistent with its results, that is, it always returns the same result if you don't change anything in between runs.
- **Self-Checking**: The test should be able to automatically detect if it passed or failed without any human interaction.
- **Timely**: A unit test shouldn't take a disproportionately long time to write compared to the code being tested. If you find testing the code taking a large amount of time compared to writing the code, consider a design that is more testable.

## Naming conventions

### Naming projects

- Unit tests projects
  - Pattern: use _.UnitTests_ prefix
  - Example: Shopping.Core > Shopping.Core.**UnitTests**
- Integration tests projects
  - Pattern: use _.IntegrationTests_ prefix
  - Example: Shopping.Api > Shopping.Api.**IntegrationTests**

### Naming test classes

- When class under test is **Cart**, the corresponding test class will be **CartTests**.
- When method under test is **Cart.UpdateExistingLine**, the corresponding test class will be **UpdateExistingLineTests**. If this is the preferred approach, all test classes should grouped inside a folder with the name of the class under test (**Cart**).

### Naming tests

- **When_StateUnderTest_Should_ExpectedBehavior**
  - When_UpdatingExistingLineWithZeroQuantity_Should_RemoveCartLine
  - When_CartLineDoesNotExists_Should_ThrowException
- **Should_ExpectedBehavior_When_StateUnderTest**
  - Should_RemoveCartLine_When_UpdatingExistingLineWithZeroQuantity
  - Should_ThrowException_When_CartLineDoesNotExists
- **Given_When_Then**
  - Given_CartLineDoesNotExists_When_UpdatingIt_Then_ShouldThrowException
  - Given_QuantityIsZero_When_UpdatingExistingLine_Then_ItWillBeRemoved
