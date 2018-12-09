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
using System.Data.Common;
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;
using NUnit.Framework;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
    /// <summary>
    /// Test the ExecuteScalar method on the Database class
    /// </summary>
    public class OracleExecuteScalarFixture
    {
        static Database db;
        static ExecuteScalarFixture baseFixture;

        [SetUp]
        public void SetUp()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
            DbCommand command = db.GetSqlStringCommand("Select count(*) from region");

            baseFixture = new ExecuteScalarFixture(db, command);
        }

        [Test]
        public void ExecuteScalarWithIDbCommand()
        {
            baseFixture.ExecuteScalarWithIDbCommand();
        }

        [Test]
        public void ExecuteScalarWithIDbTransaction()
        {
            baseFixture.ExecuteScalarWithIDbTransaction();
        }

        [Test]
        public void CanExecuteScalarDoAnInsertion()
        {
            baseFixture.CanExecuteScalarDoAnInsertion();
        }

        [Test]
        public void ExecuteScalarWithNullIDbCommand()
        {
            Assert.That(() => baseFixture.ExecuteScalarWithNullIDbCommand(), Throws.ArgumentNullException);
            ;
        }

        [Test]
        public void ExecuteScalarWithNullIDbTransaction()
        {
            Assert.That(() => baseFixture.ExecuteScalarWithNullIDbTransaction(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteScalarWithNullIDbCommandAndNullTransaction()
        {
            Assert.That(() => baseFixture.ExecuteScalarWithNullIDbCommandAndNullTransaction(), Throws.ArgumentException);
        }

        [Test]
        public void ExecuteScalarWithCommandTextAndTypeInTransaction()
        {
            baseFixture.ExecuteScalarWithCommandTextAndTypeInTransaction();
        }
    }
}
