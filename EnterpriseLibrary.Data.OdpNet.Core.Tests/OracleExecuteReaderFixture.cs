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
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
    /// <summary>
    /// Test the ExecuteReader method on the Database class
    /// </summary>
    public class OracleExecuteReaderFixture
    {
        const string insertString = "Insert into Region values (99, 'Midwest')";
        const string queryString = "Select * from Region";
        Database db;
        ExecuteReaderFixture baseFixture;

        [SetUp]
        public void SetUp()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();

            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");

            DbCommand insertCommand = db.GetSqlStringCommand(insertString);
            DbCommand queryCommand = db.GetSqlStringCommand(queryString);

            baseFixture = new ExecuteReaderFixture(db, insertString, insertCommand, queryString, queryCommand);
        }

        [Test]
        public void CanExecuteReaderWithCommandText()
        {
            baseFixture.CanExecuteReaderWithCommandText();
        }

        [Test]
        //[Ignore]
        public void Bug869Test()
        {
            object[] paramarray = new object[2];
            paramarray[0] = "BLAUS";
            paramarray[1] = null;

            using (IDataReader dataReader = db.ExecuteReader("PKGNORTHWIND.NWND_GetCustomersTest", paramarray))
            {
                while (dataReader.Read())
                {
                    // Get the value of the 'Name' column in the DataReader
                    Assert.IsNotNull(dataReader["ContactName"]);
                }
                dataReader.Close();
            }
        }

        [Test]
        public void CanExecuteReaderFromDbCommand()
        {
            baseFixture.CanExecuteReaderFromDbCommand();
        }

        [Test]
        public void WhatGetsReturnedWhenWeDoAnInsertThroughDbCommandExecute()
        {
            baseFixture.WhatGetsReturnedWhenWeDoAnInsertThroughDbCommandExecute();
        }

        [Test]
        public void CanExecuteQueryThroughDataReaderUsingTransaction()
        {
            baseFixture.CanExecuteQueryThroughDataReaderUsingTransaction();
        }

        [Test]
        public void ExecuteQueryThroughDataReaderUsingNullCommandThrows()
        {
            Assert.That(() => baseFixture.ExecuteQueryThroughDataReaderUsingNullCommandThrows(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteQueryThroughDataReaderUsingNullCommandAndNullTransactionThrows()
        {
            Assert.That(() => baseFixture.ExecuteQueryThroughDataReaderUsingNullCommandAndNullTransactionThrows(), Throws.ArgumentException);
        }

        [Test]
        public void ExecuteQueryThroughDataReaderUsingNullTransactionThrows()
        {
            Assert.That(() => baseFixture.ExecuteQueryThroughDataReaderUsingNullTransactionThrows(), Throws.ArgumentNullException);
        }

        [Test]
        public void ExecuteReaderWithNullCommand()
        {
            Assert.That(() => baseFixture.ExecuteReaderWithNullCommand(), Throws.ArgumentNullException);
        }

        [Test]
        public void NullQueryStringTest()
        {
            Assert.That(() => baseFixture.NullQueryStringTest(), Throws.ArgumentException);
        }

        [Test]
        public void EmptyQueryStringTest()
        {
            ;
            Assert.That(() => baseFixture.EmptyQueryStringTest(), Throws.ArgumentException);
        }

        [Test]
        public void CanGetTheInnerDataReader()
        {
            DbCommand queryCommand = db.GetSqlStringCommand(queryString);
            IDataReader reader = db.ExecuteReader(queryCommand);
            string accumulator = "";

            int descriptionIndex = reader.GetOrdinal("RegionDescription");
            OracleDataReader innerReader = ((OracleDataReaderWrapper)reader).InnerReader;
            Assert.IsNotNull(innerReader);

            while (reader.Read())
            {
                accumulator += innerReader.GetOracleString(descriptionIndex).Value.Trim();
            }

            reader.Close();

            Assert.AreEqual("EasternWesternNorthernSouthern", accumulator);
            Assert.AreEqual(ConnectionState.Closed, queryCommand.Connection.State);
        }
    }
}
