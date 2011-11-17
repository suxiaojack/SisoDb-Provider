﻿namespace SisoDb.Sql2008
{
    public class Sql2008DbFactory : ISisoDbFactory
    {
        static Sql2008DbFactory()
        {
            SisoEnvironment.ProviderFactories.Register(StorageProviders.Sql2008, new Sql2008ProviderFactory());
        }

        public virtual ISisoDatabase CreateDatabase(ISisoConnectionInfo connectionInfo)
        {
            return new Sql2008Database((Sql2008ConnectionInfo)connectionInfo);
        }
    }
}