using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExtensions.Tests
{
    [TestClass]
    public class ReflectionReplaceTest
    {
        [TestMethod]
        public void ReflectionReplace_1()
        {
            var obj = new Person { Name = "Miguel Angelo", Age = 30, Parent = new Person { Name = "Paulo Ricardo", Age = 77 } };
            var result = "Name: {Name}; Age: {Age}".ReflectionReplace(obj);
            Assert.AreEqual("Name: Miguel Angelo; Age: 30", result);
        }

        [TestMethod]
        public void ReflectionReplace_2()
        {
            var obj = new Person { Name = "Miguel Angelo", Age = 30, Parent = new Person { Name = "Paulo Ricardo", Age = 77 } };
            var result = "Name: {Name}; Age: {Age}; Parent name: {Parent.Name}; Parent age: {Parent.Age}".ReflectionReplace(obj);
            Assert.AreEqual("Name: Miguel Angelo; Age: 30; Parent name: Paulo Ricardo; Parent age: 77", result);
        }

        [TestMethod]
        public void ReflectionReplace_3()
        {
            var obj = new Person { Name = "Miguel Angelo", Age = 30, Parent = null };
            var result = "Name: {Name}; Age: {Age}; Parent name: {Parent.Name}; Parent age: {Parent.Age}".ReflectionReplace(obj);
            Assert.AreEqual("Name: Miguel Angelo; Age: 30; Parent name: ; Parent age: ", result);
        }
    }
}