using System.Collections.Generic;

namespace Roommate.Server.db
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

        public bool Remove(string key)
        {
            return mValues.Remove(key);
        }
    }
}