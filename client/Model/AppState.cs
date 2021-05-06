using System.ComponentModel;

namespace CryptobotUi.Client.Model
{
    public class AppState : INotifyPropertyChanged
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy; set
            {
                var raisePropChanged = value != isBusy;
                isBusy = value;
                if (raisePropChanged)
                {
                    PropertyChanged?.Invoke(this, 
                        new System.ComponentModel.PropertyChangedEventArgs(nameof(IsBusy)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}