#nullable enable

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiCollectionViewTest
{
    /// <summary>
    /// Observable object with <see cref="INotifyPropertyChanged"/> implemented
    /// using a weak event manager.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        private readonly WeakEventManager _weakEventManager = new WeakEventManager();

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => _weakEventManager.AddEventHandler(value);
            remove => _weakEventManager.RemoveEventHandler(value);
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        protected virtual bool SetProperty<T>(
            ref T backingStore,
            T value,
            [CallerMemberName] string? propertyName = "",
            Action? onChanging = null,
            Action? onChanged = null,
            Func<T, T, bool>? validateValue = null)
        {
            // if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            // if value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
            {
                return false;
            }

            onChanging?.Invoke();
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = "") =>
            _weakEventManager.HandleEvent(this, new PropertyChangedEventArgs(propertyName), nameof(PropertyChanged));
    }
}
