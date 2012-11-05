using NUnit.Framework;
using SisoDb.Resources;
using SisoDb.Sql2012;

namespace SisoDb.UnitTests.Providers.Connections
{
    [TestFixture]
    public class Sql2012ConnectionInfoTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenPassingNormalCnString_ItRocks()
        {
            var cnInfo = new Sql2012ConnectionInfo(@"data source=.;initial catalog=SisoDbTests.Temp;integrated security=SSPI;");

            Assert.AreEqual(StorageProviders.Sql2012, cnInfo.ProviderType);
            Assert.AreEqual(@"Data Source=.;Initial Catalog=;Integrated Security=True", cnInfo.ServerConnectionString);
            Assert.AreEqual(@"Data Source=.;Initial Catalog=SisoDbTests.Temp;Integrated Security=True", cnInfo.ClientConnectionString);
            Assert.AreEqual("SisoDbTests.Temp", cnInfo.DbName);
        }

        [Test]
        public void Ctor_WhenMissingDbName_ThrowsSisoDbException()
        {
            var ex = Assert.Throws<SisoDbException>(() => new Sql2012ConnectionInfo(@"data source=.;integrated security=SSPI;"));

            Assert.AreEqual(ExceptionMessages.ConnectionInfo_MissingName, ex.Message);
        }
    }
}