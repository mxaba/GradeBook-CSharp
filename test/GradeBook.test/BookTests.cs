using System;
using Xunit;

namespace GradeBook.test {
    public class BookTests {
        [Fact]
        public void BookCalculatesAnAverageGrdaes() {
            // arrange
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            // act
            var result = book.GetStatistics();

            // ssert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
            Assert.Equal('A', result.Letter);
        }
    }
}
