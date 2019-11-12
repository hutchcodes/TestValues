using NUnit.Framework;
using TestValues;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void ShouldGetStandardString()
        {
            var testValue = StandardValues.Get<string>("Foo");
            throw new System.Exception("Some Message");
        }
    }
}