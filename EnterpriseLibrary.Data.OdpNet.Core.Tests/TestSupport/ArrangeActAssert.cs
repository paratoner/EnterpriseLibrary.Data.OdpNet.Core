﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Core
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseLibrary.Data.OdpNet.Core.Tests.TestSupport
{
    /// <summary>
    /// A base class for tests written in the BDD style that provide standard
    /// methods to set up test actions and the "when" statements. "Then" is
    /// encapsulated by the [TestMethod]s themselves.
    /// </summary>
    public abstract class ArrangeActAssert
    {
        /// <summary>
        /// When overridden in a derived class, this method is used to
        /// set up the current state of the specs context.
        /// </summary>
        /// <remarks>This method is called automatically before every test,
        /// before the <see cref="Act"/> method.</remarks>
        protected virtual void Arrange()
        {

        }

        /// <summary>
        /// When overridden in a derived class, this method is used to
        /// perform interactions against the system under test.
        /// </summary>
        /// <remarks>This method is called automatically after <see cref="Arrange"/>
        /// and before each test method runs.</remarks>
        protected virtual void Act()
        {

        }

        /// <summary>
        /// When overridden in a derived class, this method is used to
        /// reset the state of the system after a test method has completed.
        /// </summary>
        /// <remarks>This method is called automatically after each TestMethod has run.</remarks>
        protected virtual void Teardown()
        {

        }

        #region MSTEST integration methods

        [SetUp]
        public void MainSetup()
        {
            try
            {
                Arrange();
                Act();
            }
            catch
            {
                this.MainTeardown();
                throw;
            }
        }

        [TearDown]
        public void MainTeardown()
        {
            Teardown();
        }

        #endregion
    }
}
