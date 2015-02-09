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


using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace SqlPermissions.Core.Trace.Event
{
    public partial class EventFactory
    {
        // TODO: should make the Tuple<CreateEvent, CreateLoader> into a well defined class.
        private delegate EventClassLoaderInfoBase CreateLoader(IDataRecord record);

        private delegate IEventBase CreateEvent(IDataRecord record, EventClassLoaderInfoBase loadInfoBase);

        private readonly SortedList<String, EventClassLoaderInfoBase> _metadata = new SortedList<String, EventClassLoaderInfoBase>(0x10);

        private readonly Int32 _eventIdOrdinal;
        private readonly Boolean _isEventIdAnInt;

        public EventFactory(IDataRecord record)
        {
            Contract.Requires(null != record, "The record must be valid.");

            _eventIdOrdinal = record.GetOrdinal("EventClass");

            var type = record.GetFieldType(_eventIdOrdinal);
            _isEventIdAnInt = typeof(Int32) == type;
            Debug.Assert(typeof(String) == type || typeof(Int32) == type, "The type should be a string or int.", "Type:" + type.FullName);
        }

        public IEventBase Build(IDataRecord record)
        {
            Contract.Requires(null != record, "The record must be valid.");

            String eventId;
            if (_isEventIdAnInt)
            {
                var eventIdInt = record.GetInt32(_eventIdOrdinal);
                if (!IdLookup.TryGetValue(eventIdInt, out eventId))
                    throw new Exception("What id is this?"); // we can't process this event, it's unknown
            }
            else
            {
                eventId = record.GetString(_eventIdOrdinal);
            }

            Tuple<CreateLoader, CreateEvent> tuple;
            if (!Creation.TryGetValue(eventId, out tuple))
            {
                Debug.WriteLine("Unhandled eventId [" + eventId + "]");
                return null; // we don't know how to process this event, return null
            }

            // get the loader
            EventClassLoaderInfoBase loader;
            if (!_metadata.TryGetValue(eventId, out loader))
            {
                // we don't have one already, create it and store in dict
                loader = tuple.Item1(record);
                _metadata[eventId] = loader;
            }
            
            // create the event
            return tuple.Item2(record, loader);
        }
    }
}
