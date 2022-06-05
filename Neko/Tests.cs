using NUnit.Framework;

namespace Neko.Tests
{
    [TestFixture]
    class NekoTest
    {
        [Test]
        public void TestCreateNuber100()
        {
            var gameScore = new GameScore("", true);
            Assert.AreEqual("九十三", gameScore.CreateNuber(93));
            Assert.AreEqual("八十四", gameScore.CreateNuber(84));
            Assert.AreEqual("一", gameScore.CreateNuber(1));
            Assert.AreEqual("八十五", gameScore.CreateNuber(85));
            Assert.AreEqual("六十四", gameScore.CreateNuber(64));
        }

        [Test]
        public void TestCreateNuber1000()
        {
            var gameScore = new GameScore("", true);
            Assert.AreEqual("九百九十三", gameScore.CreateNuber(993));
            Assert.AreEqual("二百八十四", gameScore.CreateNuber(284));
            Assert.AreEqual("百十", gameScore.CreateNuber(110));
            Assert.AreEqual("八百三十", gameScore.CreateNuber(830));
            Assert.AreEqual("三百六十四", gameScore.CreateNuber(364));
        }

        [Test]
        public void TestCreateNuber100000()
        {
            var gameScore = new GameScore("", true);
            Assert.AreEqual("千九十三", gameScore.CreateNuber(1093));
            Assert.AreEqual("二千八十四", gameScore.CreateNuber(2084));
            Assert.AreEqual("一万", gameScore.CreateNuber(10000));
            Assert.AreEqual("八万五百", gameScore.CreateNuber(80500));
            Assert.AreEqual("一万三百六十四", gameScore.CreateNuber(10364));
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(9)]
        public void TestGetSyllablesForLevel(int i)
        {
            var b = new bool[11];
            b[i] = true;
            var game = new Game(b, "Hiragana", false);
            string[] e;
            if (i == 0)
                e = new string[] { "あ/a/А", "い/i/И", "う/u/У", "え/e/Э", "お/o/О" };
            else if (i == 10)
                e = new string[] { "ん/n/Н" };
            else
                e = new string[] { "わ/wa/Ва", "を/o/О" };
            Assert.AreEqual(e, game.GetSyllablesForLevel());
        }
    }
}
