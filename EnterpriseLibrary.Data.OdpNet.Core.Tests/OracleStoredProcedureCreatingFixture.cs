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
using NUnit.Framework;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
    /// <summary>
    /// Use the Data Access Application Block to execute a create a stored procedure script using ExecNonQuery.
    /// </summary>
    public class OracleStoredProcedureCreatingFixture : StoredProcedureCreationBase
    {
        [SetUp]
        public void SetUp()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            var factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
            CompleteSetup(db);
        }

        [TearDown]
        public void TearDown()
        {
            Cleanup();
        }

        protected override void CreateStoredProcedure()
        {
            string storedProcedureCreation = "CREATE procedure TestProc " +
                                             "(vCount OUT INT, vCustomerId Orders.CustomerID%TYPE) as " +
                                             "BEGIN SELECT count(*)INTO vCount FROM Orders WHERE CustomerId = vCustomerId; END;";

            DbCommand command = db.GetSqlStringCommand(storedProcedureCreation);
            db.ExecuteNonQuery(command);
        }

        protected override void DeleteStoredProcedure()
        {
            string storedProcedureDeletion = "Drop procedure TestProc";
            DbCommand command = db.GetSqlStringCommand(storedProcedureDeletion);
            db.ExecuteNonQuery(command);
        }

        [Test]
        public void CanGetOutputValueFromStoredProcedure()
        {
            baseFixture.CanGetOutputValueFromStoredProcedure();
        }

        [Test]
        public void CanGetOutputValueFromStoredProcedureWithCachedParameters()
        {
            baseFixture.CanGetOutputValueFromStoredProcedureWithCachedParameters();
        }

        [Test]
        public void ArgumentExceptionWhenThereAreTooFewParameters()
        {
            Assert.That(() => baseFixture.ArgumentExceptionWhenThereAreTooFewParameters(), Throws.InvalidOperationException);
        }

        [Test]
        public void ArgumentExceptionWhenThereAreTooManyParameters()
        {
            Assert.That(() => baseFixture.ArgumentExceptionWhenThereAreTooFewParameters(), Throws.InvalidOperationException);
        }

        [Test]
        public void ExceptionThrownWhenReadingParametersFromCacheWithTooFewParameterValues()
        {
            Assert.That(() => baseFixture.ExceptionThrownWhenReadingParametersFromCacheWithTooFewParameterValues(), Throws.InvalidOperationException);
        }
    }
}
