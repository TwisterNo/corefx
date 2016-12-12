// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//------------------------------------------------------------------------------
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.DirectoryServices.ActiveDirectory
{
    using System;
    using System.Globalization;
    using System.Collections;

    public class AdamInstanceCollection : ReadOnlyCollectionBase
    {
        internal AdamInstanceCollection() { }

        internal AdamInstanceCollection(ArrayList values)
        {
            if (values != null)
            {
                InnerList.AddRange(values);
            }
        }

        public AdamInstance this[int index]
        {
            get
            {
                return (AdamInstance)InnerList[index];
            }
        }

        public bool Contains(AdamInstance adamInstance)
        {
            if (adamInstance == null)
            {
                throw new ArgumentNullException("adamInstance");
            }

            for (int i = 0; i < InnerList.Count; i++)
            {
                AdamInstance tmp = (AdamInstance)InnerList[i];
                if (Utils.Compare(tmp.Name, adamInstance.Name) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(AdamInstance adamInstance)
        {
            if (adamInstance == null)
            {
                throw new ArgumentNullException("adamInstance");
            }

            for (int i = 0; i < InnerList.Count; i++)
            {
                AdamInstance tmp = (AdamInstance)InnerList[i];
                if (Utils.Compare(tmp.Name, adamInstance.Name) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public void CopyTo(AdamInstance[] adamInstances, int index)
        {
            InnerList.CopyTo(adamInstances, index);
        }
    }
}
