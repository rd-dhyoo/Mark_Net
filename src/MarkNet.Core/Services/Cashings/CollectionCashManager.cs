using MarkNet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Cashings
{
    public class CollectionCashManager<T> where T : PropertyModel<T>, new()
    {
        private object _locker = new object();
        private List<T> _models = new List<T>();

        public IEnumerable<T> Get()
        {
            lock (_locker)
            {
                return _models.Select(row => row.Clone()).ToArray();
            }
        }

        public void Set(IEnumerable<T> collections)
        {
            lock (_locker)
            {
                _models.Clear();
                foreach (var collection in collections)
                {
                    var model = new T();
                    model.CopyValues(collection);
                    _models.Add(model);
                }
            }
        }
    }
}
