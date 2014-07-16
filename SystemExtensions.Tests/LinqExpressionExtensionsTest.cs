using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExtensions.Tests
{
    [TestClass]
    public class LinqExpressionExtensionsTest
    {
        [TestMethod]
        public void Combine_1()
        {
            Expression<Func<Person, Person>> getParent = person => person.Parent;
            var getGrandParent = getParent.Combine(getParent);
            var expected = (Expression<Func<Person, Person>>)(person => person.Parent.Parent);
            Assert.AreEqual(expected.ToString(), getGrandParent.ToString());
        }

        [TestMethod]
        public void Combine_2()
        {
            Expression<Func<Person, string>> getParent = person => person.Name;
            Expression<Func<string, int>> getLength = str => str.Length;
            var getNameLength = getParent.Combine(getLength);
            var expected = (Expression<Func<Person, int>>)(person => person.Name.Length);
            Assert.AreEqual(expected.ToString(), getNameLength.ToString());
        }
    }
}