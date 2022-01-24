using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BW_Mobile
{
    internal class PropertiesIO<T>
    {
        private IDictionary<string, object> _settings;

        public PropertiesIO()
        {
            _settings = Application.Current.Properties;
        }

        public T this[string key]
        {
            get
            {
                return (T)(_settings[key]);
            }
            set
            {
                if (_settings.ContainsKey(key))
                {
                    _settings[key] = value;
                }
                else
                {
                    _settings.Add(key, value);
                }
                
            }
        }

        public T InitGet(string key, T initValue)
        {
            if (_settings.ContainsKey(key))
            {
                return this[key];
            }
            else
            {
                this[key] = initValue;
                return initValue;
            }
        }
    }
}
