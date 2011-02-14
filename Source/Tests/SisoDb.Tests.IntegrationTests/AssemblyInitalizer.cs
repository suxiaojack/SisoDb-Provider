﻿using NUnit.Framework;

namespace SisoDb.Tests.IntegrationTests
{
    [SetUpFixture]
    public class AssemblyInitalizer
    {
        [SetUp]
        public void Initialize()
        {
            var connectionInfo = new SisoConnectionInfo(LocalConstants.ConnectionStringName);
            var database = new SisoDbFactory().CreateDatabase(connectionInfo);
            database.EnsureNewDatabase();
        }
    }
}