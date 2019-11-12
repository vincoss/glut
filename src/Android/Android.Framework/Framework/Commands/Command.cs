using System;
using System.Windows.Input;


namespace Android.Framework.Commands
{
    public abstract class Command : ICommand
    {
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        #endregion

        protected void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}