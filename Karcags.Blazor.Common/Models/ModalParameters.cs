using System.Collections.Generic;

namespace Karcags.Blazor.Common.Models
{
    public class ModalParameters
    {
        private readonly Dictionary<string, object> _parameters;

        public ModalParameters()
        {
            this._parameters = new Dictionary<string, object>();
        }

        public void Add(string parameterName, object value)
        {
            this._parameters[parameterName] = value;
        }

        public T Get<T>(string parameterName)
        {
            if (!this._parameters.ContainsKey(parameterName))
            {
                throw new KeyNotFoundException("Not exist.");
            }
            return (T)this._parameters[parameterName];
        }

        public T TryGet<T>(string parameterName)
        {
            if (this._parameters.ContainsKey(parameterName))
            {
                return (T)this._parameters[parameterName];
            }
            return default;
        }
    }
}