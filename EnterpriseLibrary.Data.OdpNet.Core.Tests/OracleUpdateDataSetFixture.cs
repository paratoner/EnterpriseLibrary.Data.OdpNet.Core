﻿//===============================================================================
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
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NUnit.Framework;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
    /// <summary>
    /// Tests executing a batch of commands with insert, delete and update 
    /// using ExecuteUpdateDataTable
    /// </summary>
    public class OracleUpdateDataSetFixture : UpdateDataSetFixture
    {
        [SetUp]
        public void TestInitialize()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
            try
            {
                DeleteStoredProcedures();
            }
            catch { }
            CreateStoredProcedures();
            base.SetUp();
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            base.TearDown();
            DeleteStoredProcedures();
        }

        [Test]
        public void OracleModifyRowWithStoredProcedure()
        {
            base.ModifyRowWithStoredProcedure();
        }

        [Test]
        public void OracleDeleteRowWithStoredProcedure()
        {
            base.DeleteRowWithStoredProcedure();
        }

        [Test]
        public void OracleInsertRowWithStoredProcedure()
        {
            base.InsertRowWithStoredProcedure();
        }

        [Test]
        public void OracleDeleteRowWithMissingInsertAndUpdateCommands()
        {
            base.DeleteRowWithMissingInsertAndUpdateCommands();
        }

        [Test]
        public void OracleUpdateRowWithMissingInsertAndDeleteCommands()
        {
            base.UpdateRowWithMissingInsertAndDeleteCommands();
        }

        [Test]
        public void OracleInsertRowWithMissingUpdateAndDeleteCommands()
        {
            base.InsertRowWithMissingUpdateAndDeleteCommands();
        }

        [Test]
        public void OracleUpdateDataSetWithAllCommandsMissing()
        {
            Assert.That(() => base.UpdateDataSetWithAllCommandsMissing(), Throws.ArgumentException);
        }

        [Test]
        public void OracleUpdateDataSetWithNullTable()
        {
            Assert.That(() => base.UpdateDataSetWithNullTable(), Throws.ArgumentException);
        }

        protected override void CreateDataAdapterCommands()
        {
            OracleDataSetHelper.CreateDataAdapterCommands(db, ref insertCommand, ref updateCommand, ref deleteCommand);
        }

        protected override void CreateStoredProcedures()
        {
            OracleDataSetHelper.CreateStoredProcedures(db);
        }

        protected override void DeleteStoredProcedures()
        {
            OracleDataSetHelper.DeleteStoredProcedures(db);
        }

        protected override void AddTestData()
        {
            OracleDataSetHelper.AddTestData(db);
        }
    }
}
