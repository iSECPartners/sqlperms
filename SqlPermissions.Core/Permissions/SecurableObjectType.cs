// Copyright (c) 2011-2015 iSEC Partners
// Author: Peter Oehlert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


namespace SqlPermissions.Core.Permissions
{
    /// <summary>IMPORTANT: update SecurableObject.SecurableObjectIdentifier if you add or change this enum</summary>
    public enum SecurableObjectType
    {
        /// <summary>Database Principal Permission</summary>
        ApplicationRole,

        /// <summary>Assembly Permission</summary>
        Assembly,

        /// <summary>Assymetric Key Permission</summary>
        AsymmetricKey,

        /// <summary>Availability Group Permission</summary>
        AvailabilityGroup,
        
        /// <summary>Certificate Permission</summary>
        Certificate,

        /// <summary>Service Broker Permission</summary>
        Contract,

        /// <summary>Database Permission</summary>
        Database,

        /// <summary>Endpoint Permmission</summary>
        Endpoint,

        /// <summary>Full-Text Permission</summary>
        FullTextCatalog,

        /// <summary>Full-Text Permission</summary>
        FullTextStopList,

        /// <summary>Server Principal Permission</summary>
        Login,

        /// <summary>Service Broker Permission</summary>
        MessageType,

        /// <summary>Object Permission</summary>
        Object,

        /// <summary>Service Broker Permission</summary>
        RemoteServiceBinding,

        /// <summary>Database Principal Permission</summary>
        Role,

        /// <summary>Service Broker Permission</summary>
        Route,

        /// <summary>Schema Permission</summary>
        Schema,

        /// <summary>Search Properly List Permission</summary>
        SearchPropertyList,
        
        /// <summary>Server Permission</summary>
        Server,

        /// <summary>Server Principal Permission</summary>
        ServerRole,

        /// <summary>Service Broker Permission</summary>
        Service,

        /// <summary>Symmetric Key Permission</summary>
        SymmetricKey,

        /// <summary>Type Permission</summary>
        Type,
        
        /// <summary>Database Principal Permission</summary>
        User,

        /// <summary>XML Schema Collection Permission</summary>
        XmlSchemaCollection
    }
}
