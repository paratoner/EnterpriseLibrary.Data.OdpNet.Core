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

using System.Configuration;
using EnterpriseLibrary.Data.OdpNet.Core.TestSupport;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport
{
    public static class OracleTestConfigurationSource
    {
        public const string OracleConnectionString = "DATA SOURCE=192.168.1.109:1521/orcldb;USER ID=C##ADMINTEST;PASSWORD=admin123;";
        public const string OracleConnectionStringName = "OracleTest";
        public const string OracleProviderName = "Oracle.ManagedDataAccess.Client";

        public static DictionaryConfigurationSource CreateConfigurationSource()
        {
            DictionaryConfigurationSource configSource = TestConfigurationSource.CreateConfigurationSource();

            var connectionString = new ConnectionStringSettings(
                OracleConnectionStringName,
                OracleConnectionString,
                OracleProviderName);

            var connectionStrings = new ConnectionStringsSection();
            connectionStrings.ConnectionStrings.Add(connectionString);

            configSource.Add("connectionStrings", connectionStrings);
            return configSource;
        }
    }
}
