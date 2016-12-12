// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
Copyright (c) 2004  Microsoft Corporation

Module Name:


Abstract:

    This class wraps the PropertyCollection and ResultPropertyCollection collection from S.DS.
    This provides a consistent interface for the internal functions that read properties while still allowing
    use of either a DirectoryEntry or SearchResult as the data source.

History:

    20-Feb-2007    TQuerec     Created

--*/

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using System.Text;
using System.Net;
using System.Collections;

namespace System.DirectoryServices.AccountManagement
{
    internal class dSPropertyCollection
    {
        private PropertyCollection _pc;
        private ResultPropertyCollection _rp;

        private dSPropertyCollection() { }
        internal dSPropertyCollection(PropertyCollection pc) { _pc = pc; }
        internal dSPropertyCollection(ResultPropertyCollection rp) { _rp = rp; }

        public dSPropertyValueCollection this[string propertyName]
        {
            // <SecurityKernel Critical="True" Ring="0">
            // <SatisfiesLinkDemand Name="PropertyCollection.get_Item(System.String):System.DirectoryServices.PropertyValueCollection" />
            // </SecurityKernel>
            [System.Security.SecurityCritical]
            get
            {
                if (propertyName == null)
                    throw new ArgumentNullException("propertyName");

                if (null != _pc)
                {
                    return new dSPropertyValueCollection(_pc[propertyName]);
                }
                else
                {
                    return new dSPropertyValueCollection(_rp[propertyName]);
                }
            }
        }
    }

    internal class dSPropertyValueCollection
    {
        private PropertyValueCollection _pc;
        private ResultPropertyValueCollection _rc;

        private dSPropertyValueCollection() { }
        internal dSPropertyValueCollection(PropertyValueCollection pc) { _pc = pc; }
        internal dSPropertyValueCollection(ResultPropertyValueCollection rc) { _rc = rc; }

        public object this[int index]
        {
            // <SecurityKernel Critical="True" Ring="0">
            // <SatisfiesLinkDemand Name="PropertyValueCollection.get_Item(System.Int32):System.Object" />
            // </SecurityKernel>
            [System.Security.SecurityCritical]
            get
            {
                if (_pc != null)
                {
                    return _pc[index];
                }
                else
                {
                    return _rc[index];
                }
            }
        }
        public int Count
        {
            get
            {
                return (_pc != null ? _pc.Count : _rc.Count);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (_pc != null ? _pc.GetEnumerator() : _rc.GetEnumerator());
        }
    }
}
