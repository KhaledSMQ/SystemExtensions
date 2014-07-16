using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemExtensions.Tests
{
    [TestClass]
    public class LooseSubstringTest
    {
        private const string TestString = "Miguel Angelo";

        private static readonly Func<int, char> PaddingFunc = i => "0123"[(4 + (i % 4)) % 4];

        [TestMethod]
        public void LooseSubstringWithFunc_1()
        {
            var result = TestString.LooseSubstring(-10, 16, PaddingFunc);
            Assert.AreEqual(result, "2301230123Miguel");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_2()
        {
            var result = TestString.LooseSubstring(0, 6, PaddingFunc);
            Assert.AreEqual(result, "Miguel");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_3()
        {
            var result = TestString.LooseSubstring(-10, 30, PaddingFunc);
            Assert.AreEqual(result, "2301230123Miguel Angelo1230123");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_4()
        {
            var result = TestString.LooseSubstring(0, 20, PaddingFunc);
            Assert.AreEqual(result, "Miguel Angelo1230123");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_5()
        {
            var result = TestString.LooseSubstring(7, 13, PaddingFunc);
            Assert.AreEqual(result, "Angelo1230123");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_6()
        {
            var result = TestString.LooseSubstring(7, 6, PaddingFunc);
            Assert.AreEqual(result, "Angelo");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_7()
        {
            var result = TestString.LooseSubstring(500, 0, PaddingFunc);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_8()
        {
            var result = TestString.LooseSubstring(-500, 0, PaddingFunc);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_9()
        {
            var result = TestString.LooseSubstring(-4, 4, PaddingFunc);
            Assert.AreEqual(result, "0123");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_10()
        {
            var result = TestString.LooseSubstring(13, 4, PaddingFunc);
            Assert.AreEqual(result, "1230");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_11()
        {
            var result = TestString.LooseSubstring(-8, 4, PaddingFunc);
            Assert.AreEqual(result, "0123");
        }

        [TestMethod]
        public void LooseSubstringWithFunc_12()
        {
            var result = TestString.LooseSubstring(17, 4, PaddingFunc);
            Assert.AreEqual(result, "1230");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithFunc_NegativeLength_1()
        {
            TestString.LooseSubstring(4, -4, i => "0123"[(4 + (i % 4)) % 4]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithFunc_NegativeLength_2()
        {
            TestString.LooseSubstring(100, -4, i => "0123"[(4 + (i % 4)) % 4]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithFunc_NegativeLength_3()
        {
            TestString.LooseSubstring(-100, -4, i => "0123"[(4 + (i % 4)) % 4]);
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_1()
        {
            var result = TestString.LooseSubstring(-10, 16, '_', '!');
            Assert.AreEqual(result, "__________Miguel");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_2()
        {
            var result = TestString.LooseSubstring(0, 6, '_', '!');
            Assert.AreEqual(result, "Miguel");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_3()
        {
            var result = TestString.LooseSubstring(-10, 30, '_', '!');
            Assert.AreEqual(result, "__________Miguel Angelo!!!!!!!");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_4()
        {
            var result = TestString.LooseSubstring(0, 20, '_', '!');
            Assert.AreEqual(result, "Miguel Angelo!!!!!!!");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_5()
        {
            var result = TestString.LooseSubstring(7, 13, '_', '!');
            Assert.AreEqual(result, "Angelo!!!!!!!");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_6()
        {
            var result = TestString.LooseSubstring(7, 6, '_', '!');
            Assert.AreEqual(result, "Angelo");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_7()
        {
            var result = TestString.LooseSubstring(500, 0, '_', '!');
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_8()
        {
            var result = TestString.LooseSubstring(-500, 0, '_', '!');
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_9()
        {
            var result = TestString.LooseSubstring(-4, 4, '_', '!');
            Assert.AreEqual(result, "____");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_10()
        {
            var result = TestString.LooseSubstring(13, 4, '_', '!');
            Assert.AreEqual(result, "!!!!");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_11()
        {
            var result = TestString.LooseSubstring(-8, 4, '_', '!');
            Assert.AreEqual(result, "____");
        }

        [TestMethod]
        public void LooseSubstringWithPadChar_12()
        {
            var result = TestString.LooseSubstring(17, 4, '_', '!');
            Assert.AreEqual(result, "!!!!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithPadChar_NegativeLength_1()
        {
            TestString.LooseSubstring(4, -4, '_');
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithPadChar_NegativeLength_2()
        {
            TestString.LooseSubstring(100, -4, '_');
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstringWithPadChar_NegativeLength_3()
        {
            TestString.LooseSubstring(-100, -4, '_');
        }

        [TestMethod]
        public void LooseSubstring_1()
        {
            var result = TestString.LooseSubstring(-10, 16);
            Assert.AreEqual(result, "Miguel");
        }

        [TestMethod]
        public void LooseSubstring_2()
        {
            var result = TestString.LooseSubstring(0, 6);
            Assert.AreEqual(result, "Miguel");
        }

        [TestMethod]
        public void LooseSubstring_3()
        {
            var result = TestString.LooseSubstring(-10, 30);
            Assert.AreEqual(result, "Miguel Angelo");
        }

        [TestMethod]
        public void LooseSubstring_4()
        {
            var result = TestString.LooseSubstring(0, 20);
            Assert.AreEqual(result, "Miguel Angelo");
        }

        [TestMethod]
        public void LooseSubstring_5()
        {
            var result = TestString.LooseSubstring(7, 13);
            Assert.AreEqual(result, "Angelo");
        }

        [TestMethod]
        public void LooseSubstring_6()
        {
            var result = TestString.LooseSubstring(7, 6);
            Assert.AreEqual(result, "Angelo");
        }

        [TestMethod]
        public void LooseSubstring_7()
        {
            var result = TestString.LooseSubstring(500, 0);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstring_8()
        {
            var result = TestString.LooseSubstring(-500, 0);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstring_9()
        {
            var result = TestString.LooseSubstring(-4, 4);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstring_10()
        {
            var result = TestString.LooseSubstring(13, 4);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstring_11()
        {
            var result = TestString.LooseSubstring(-8, 4);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void LooseSubstring_12()
        {
            var result = TestString.LooseSubstring(17, 4);
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstring_NegativeLength_1()
        {
            TestString.LooseSubstring(4, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstring_NegativeLength_2()
        {
            TestString.LooseSubstring(100, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LooseSubstring_NegativeLength_3()
        {
            TestString.LooseSubstring(-100, -4);
        }
    }
}
