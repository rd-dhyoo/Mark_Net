using BaseNet.Core.Models;

namespace BaseNet.Core.Services.Cashings
{
    public class CashManager<T> where T : PropertyModel<T>, new()
    {
        private object _locker = new object();
        private T _model = new T();

        public T Get()
        {
            lock (_locker)
            {
                return _model.Clone();
            }
        }

        public void Set(T model)
        {
            lock (_locker)
            {
                _model.CopyValues(model);
            }
        }

        public void Patch(T model)
        {
            lock (_locker)
            {
                _model.PatchValues(model);
            }
        }
    }
}
