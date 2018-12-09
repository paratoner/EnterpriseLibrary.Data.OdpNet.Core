//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
#pragma warning disable 612, 618
    public class OracleDatabaseFixture
    {
        public OracleDatabaseFixture()
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
        }
        Microsoft.Practices.EnterpriseLibrary.Common.Configuration.IConfigurationSource configurationSource;

        [SetUp]
        public void SetUp()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            configurationSource = OracleTestConfigurationSource.CreateConfigurationSource();
        }

        [Test]
        public void CanConnectToOracleAndExecuteAReader()
        {
            var oracleDatabase = new DatabaseSyntheticConfigSettings(this.configurationSource).GetDatabase("OracleTest").BuildDatabase();

            DbConnection connection = oracleDatabase.CreateConnection();
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection is OracleConnection);
            connection.Open();
            DbCommand cmd = oracleDatabase.GetSqlStringCommand("Select * from Region");
            cmd.CommandTimeout = 0;
        }

        [Test]
        public void CanExecuteCommandWithEmptyPackages()
        {
            ConnectionStringSettings data = ConfigurationManager.ConnectionStrings["OracleTest"];

            OracleDatabase oracleDatabase = new OracleDatabase(data.ConnectionString);
            DbConnection connection = oracleDatabase.CreateConnection();
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection is OracleConnection);
            connection.Open();
            DbCommand cmd = oracleDatabase.GetSqlStringCommand("Select * from Region");
            cmd.CommandTimeout = 0;
        }

        [Test]
        public void ConstructingAnOracleDatabaseWithNullPackageListThrows()
        {
            ConnectionStringSettings data = ConfigurationManager.ConnectionStrings["OracleTest"];

            Assert.That(() => new OracleDatabase(data.ConnectionString, null), Throws.ArgumentNullException);
        }
    }
#pragma warning restore 612, 618
}
