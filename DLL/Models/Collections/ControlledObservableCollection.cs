using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DLL.Models.Collections
{
    public class ControlledObservableCollection<T> : ObservableCollection<T>
    {
        public bool SuppressNotification = false;

        public event EventHandler? UpdateStarted;

        public void BeginUpdate()
        {
            SuppressNotification = true;
            //OnCollectionUpdateStarted();
        }

        public void BeginSettingDefault()
        {
            //_suppressNotification = true;
            OnCollectionUpdateStarted();
        }

        public void EndUpdate()
        {
            SuppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!SuppressNotification)
                base.OnCollectionChanged(e);
        }

        protected void OnCollectionUpdateStarted()
        {
            UpdateStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}