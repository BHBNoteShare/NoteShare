namespace NoteShare.Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void Test2()
        {
            Assert.NotEqual(5, 2 + 2);
        }
    }
}