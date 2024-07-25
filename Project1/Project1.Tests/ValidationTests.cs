using Xunit;
using System.Collections.Generic;

namespace Project1.Tests
{
    public class ValidationTests
    {
            public static IEnumerable<object[]> InvalidNames =>
            new List<object[]>
            {
                new object[] { null, false },
                new object[] { "", false },
                new object[] { " ", false },
                new object[] { "A", false },
                new object[] { new string('A', 21), false },  // 21 characters long
                new object[] { "John123", false },
                new object[] { "J@hn", false },  // Special character
                new object[] { new string('A', 2), true },  // Exactly 2 characters long
                new object[] { new string('A', 20), true }  // Exactly 20 characters long
            };

            public static IEnumerable<object[]> ValidNames =>
            new List<object[]>
            {
                new object[] { "John", true },
                new object[] { "O'Connor", true },
                new object[] { "Mary Jane", true },
                new object[] { "Jane-Doe", true }
            };

            public static IEnumerable<object[]> InvalidUsernames =>
            new List<object[]>
            {
                new object[] { null, false },
                new object[] { "", false },
                new object[] { "AB", false },
                new object[] { new string('U', 21), false },  
                new object[] { new string('U', 3), true }  
            };

            public static IEnumerable<object[]> ValidUsernames =>
            new List<object[]>
            {
                new object[] { "ValidUser", true },
                new object[] { "user123", true }  // Case with numbers
            };

            public static IEnumerable<object[]> InvalidPasswords =>
            new List<object[]>
            {
                new object[] { null, false },
                new object[] { "", false },
                new object[] { "short", false },
                new object[] { new string('P', 21), false },  
                new object[] { new string('P', 6), true }  
            };

            public static IEnumerable<object[]> ValidPasswords =>
            new List<object[]>
            {
                new object[] { "ValidPass123", true },
                new object[] { "P@ssw0rd!", true }  // Edge case with special characters
            };

             public static IEnumerable<object[]> InvalidGenres =>
        new List<object[]>
        {
            new object[] { null, false },
            new object[] { "", false },
            new object[] { " ", false },
            new object[] { "A", false },
            new object[] { new string('A', 31), false },  // 31 characters long
            new object[] { "12345", false },
            new object[] { "Genre$", false },  // Special character
            new object[] { "Sci-fi", true }  // Valid genre
        };

        public static IEnumerable<object[]> ValidGenres =>
        new List<object[]>
        {
            new object[] { "Fantasy", true },
            new object[] { "Historical Fiction", true },
            new object[] { "Science Fiction", true }
        };

        public static IEnumerable<object[]> InvalidBookTitles =>
        new List<object[]>
        {
            new object[] { null, false },
            new object[] { "", false },
            new object[] { " ", false },
            new object[] { "A", false },
            new object[] { new string('A', 31), false },  // 31 characters long
            new object[] { "Book123", true },
            new object[] { "Book@", false },  // Special character
            new object[] { "Book Title", true }  // Valid title
        };

        public static IEnumerable<object[]> ValidBookTitles =>
        new List<object[]>
        {
            new object[] { "The Great Gatsby", true },
            new object[] { "To Kill a Mockingbird", true },
            new object[] { "1984", true }
        };

        public static IEnumerable<object[]> InvalidReviewTexts =>
        new List<object[]>
        {
            new object[] { null, false },
            new object[] { "", false },
            new object[] { "Too short", false },  // Less than 10 characters
            new object[] { new string('A', 1001), false },  // 1001 characters long
            new object[] { "This is a valid review text.", true }  // Valid review text
        };

        public static IEnumerable<object[]> ValidReviewTexts =>
        new List<object[]>
        {
            new object[] { "This book was absolutely amazing!", true },
            new object[] { "I thoroughly enjoyed the plot and characters.", true }
        };

        public static IEnumerable<object[]> InvalidIntegers =>
        new List<object[]>
        {
            new object[] { null, false },
            new object[] { "", false },
            new object[] { " ", false },
            new object[] { "123.45", false },
            new object[] { "abc", false },
            new object[] { "1e4", false }
        };

        public static IEnumerable<object[]> ValidIntegers =>
        new List<object[]>
        {
            new object[] { "123", true },
            new object[] { "-456", true },
            new object[] { "0", true },
            new object[] { "789", true }
        };

        [Theory]
        [MemberData(nameof(InvalidNames))]
        [MemberData(nameof(ValidNames))]
        public void ValidateName_ShouldReturnExpectedResult(string name, bool expectedIsValid)
        {

            bool isValid = Validation.ValidateName(name);

            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidUsernames))]
        [MemberData(nameof(ValidUsernames))]
        public void ValidateUsername_ShouldReturnExpectedResult(string username, bool expectedIsValid)
        {
   
            bool isValid = Validation.ValidateUsername(username);

            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidPasswords))]
        [MemberData(nameof(ValidPasswords))]
        public void ValidatePassword_ShouldReturnExpectedResult(string password, bool expectedIsValid)
        {

            bool isValid = Validation.ValidatePassword(password);

   
            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidGenres))]
        [MemberData(nameof(ValidGenres))]
        public void ValidateGenre_ShouldReturnExpectedResult(string genre, bool expectedIsValid)
        {

            bool isValid = Validation.ValidateGenre(genre);

            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidBookTitles))]
        [MemberData(nameof(ValidBookTitles))]
        public void ValidateBookTitle_ShouldReturnExpectedResult(string bookTitle, bool expectedIsValid)
        {

            bool isValid = Validation.ValidateBookTitle(bookTitle);

            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidReviewTexts))]
        [MemberData(nameof(ValidReviewTexts))]
        public void ValidateReviewText_ShouldReturnExpectedResult(string reviewText, bool expectedIsValid)
        {

            bool isValid = Validation.ValidateReviewText(reviewText);


            Assert.Equal(expectedIsValid, isValid);
        }

        [Theory]
        [MemberData(nameof(InvalidIntegers))]
        [MemberData(nameof(ValidIntegers))]
        public void ValidateInteger_ShouldReturnExpectedResult(string input, bool expectedIsValid)
        {
            bool isValid = Validation.ValidateInteger(input);
            Assert.Equal(expectedIsValid, isValid);
        }
    }
}
