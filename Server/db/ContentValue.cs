using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Server.db
{
    public class ContentValue
    {
        private readonly Dictionary<string, object> mValues;

        public Dictionary<string, object>.KeyCollection Keys
        {
            get { return mValues.Keys; }
        }

        public ContentValue()
        {
            mValues = new Dictionary<string, object>();
        }

        public void Put(string key, object value)
        {
            mValues.Add(key,value);
        }

        public object Get(string key)
        {
            return mValues[key];
        }

    }
}