using System;
using Xunit;

namespace GradeBook.test {
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod(){
            WriteLogDelegate log = ReturnMessage;

            // log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += IncrementCount;
            var results = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message){
            count++;
            return message.ToLower();
        }

        string ReturnMessage(string message){
            count++;
            return message;
        }

        [Fact]
        public void StringBehaveLikeValueTypes(){
            string name = "Mcebo";
            var nameUpper = MakeUpperCase(name);

            Assert.Equal("Mcebo", name);
            Assert.Equal("MCEBO", nameUpper);
        }

        private string MakeUpperCase(string name) {
            return name.ToUpper();
        }

        [Fact]
        public void ValuesTypeAlsoPassByValue(){
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x) {
            x = 42;
        }

        private int GetInt() {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef() {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Name");

            // act

            // ssert
            Assert.Equal("New Name", book1.Name);
        }

        void GetBookSetName(out Book book, string name) {
            book = new Book(name);
        }

        [Fact]
        public void CSharpIsPassByValue() {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            // act

            // ssert
            Assert.Equal("Book 1", book1.Name);
        }

        void GetBookSetName(Book book, string name) {
            book = new Book(name);
        }

        [Fact]
        public void CanSetNameFromReference() {
            // arrange
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            // act

            // ssert
            Assert.Equal("New Name", book1.Name);
        }

        void SetName(Book book, string name) {
            book.Name = name;
        }

        [Fact]
        public void CanTwoObjectsReferenceSameObject() {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            // act

            // ssert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }


        Book GetBook(string name){
            return new Book(name);
        }
    }
}
