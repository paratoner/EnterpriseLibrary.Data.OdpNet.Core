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

using System.Data;
using System.Data.Common;
using EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport;

using Microsoft.Practices.EnterpriseLibrary.Data;
using NUnit.Framework;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests
{
    /// <summary>
    /// Tests SqlDatabase.GetSqlStringCommand when using a parameterized sql string
    /// </summary>
    public class OracleParameterizedSqlStringFixture
    {
        Database db;

        [SetUp]
        public void SetUp()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
        }

        [Test]
        public void ExecuteSqlStringCommandWithParameters()
        {
            string sql = "select * from Region where (RegionID=:Param1) and RegionDescription=:Param2";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":Param1", DbType.Int32, 1);
            db.AddInParameter(cmd, ":Param2", DbType.String, "Eastern");
            DataSet ds = db.ExecuteDataSet(cmd);
            Assert.AreEqual(1, ds.Tables[0].Rows.Count);
        }

        [Test]
        public void ExecuteSqlStringCommandWithNotEnoughParameterValues()
        {
            string sql = "select * from Region where RegionID=:Param1 and RegionDescription=:Param2";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":Param1", DbType.Int32, 1);
            DataSet ds = db.ExecuteDataSet(cmd);
        }

        [Test]
        public void ExecuteSqlStringCommandWithTooManyParameterValues()
        {
            string sql = "select * from Region where RegionID=:Param1 and RegionDescription=:Param2";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":Param1", DbType.Int32, 1);
            db.AddInParameter(cmd, ":Param2", DbType.String, "Eastern");
            db.AddInParameter(cmd, ":Param3", DbType.String, "Western");
            DataSet ds = db.ExecuteDataSet(cmd);
        }

        [Test]
        public void ExecuteSqlStringWithoutParametersButWithValues()
        {
            string sql = "select * from Region";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, ":Param1", DbType.Int32, 1);
            db.AddInParameter(cmd, ":Param2", DbType.String, "Eastern");
            DataSet ds = db.ExecuteDataSet(cmd);
            Assert.AreEqual(4, ds.Tables[0].Rows.Count);
        }
    }
}
