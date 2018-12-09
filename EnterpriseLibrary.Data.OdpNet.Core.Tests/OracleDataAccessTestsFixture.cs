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
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;
using NUnit.Framework;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{

    public class OracleDataAccessTestsFixture
    {
        Database db;
        DataAccessTestsFixture baseFixture;

        [SetUp]
        public void TestInitialize()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
            baseFixture = new DataAccessTestsFixture(db);
        }

        [Test]
        public void CanGetResultSetBackWithParamaterizedQuery()
        {
            string sqlCommand = "SELECT RegionDescription FROM Region where regionId = :ID";
            DataSet ds = new DataSet();
            DbCommand cmd = db.GetSqlStringCommand(sqlCommand);
            db.AddInParameter(cmd, ":ID", DbType.Int32, 4);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    db.LoadDataSet(cmd, ds, "Foo", transaction);
                    Assert.AreEqual(1, ds.Tables[0].Rows.Count);
                }
            }
        }

        // ********************************

        [Test]
        public void CanGetNonEmptyResultSet()
        {
            baseFixture.CanGetNonEmptyResultSet();
        }

        [Test]
        public void CanGetTablePositionally()
        {
            baseFixture.CanGetTablePositionally();
        }

        [Test]
        public void CanGetNonEmptyResultSetUsingTransaction()
        {
            baseFixture.CanGetNonEmptyResultSetUsingTransaction();
        }

        [Test]
        public void CanGetNonEmptyResultSetUsingTransactionWithNullTableName()
        {
            baseFixture.CanGetNonEmptyResultSetUsingTransactionWithNullTableName();
        }

        [Test]
        public void ExecuteDataSetWithCommand()
        {
            baseFixture.ExecuteDataSetWithCommand();
        }

        [Test]
        public void ExecuteDataSetWithDbTransaction()
        {
            baseFixture.ExecuteDataSetWithDbTransaction();
        }

        [Test]
        public void CannotLoadDataSetWithEmptyTableName()
        {
            Assert.That(() => baseFixture.CannotLoadDataSetWithEmptyTableName(), Throws.ArgumentException);
        }

        [Test]
        public void ExecuteNullCommand()
        {
            Assert.That(() => baseFixture.ExecuteNullCommand(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteCommandNullTransaction()
        {
            Assert.That(() => baseFixture.ExecuteCommandNullTransaction(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteCommandNullDataset()
        {
            Assert.That(() => baseFixture.ExecuteCommandNullDataset(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteCommandNullCommand()
        {
            Assert.That(() => baseFixture.ExecuteCommandNullCommand(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteCommandNullTableName()
        {
            Assert.That(() => baseFixture.ExecuteCommandNullTableName(), Throws.ArgumentException);
        }
    }
}
