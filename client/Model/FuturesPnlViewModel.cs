using CryptobotUi.Models.Cryptodb;
using System.Collections.Generic;
using System.ComponentModel;

namespace CryptobotUi.Client.Model
{
    public class FuturesPnlViewModel : FuturesPnl, INotifyPropertyChanged
    {
        private IEnumerable<FuturesSignalCommand> commands;
        private int commandCount;

        public IEnumerable<FuturesSignalCommand> Commands
        {
            get => commands;
            set
            {
                var raisePropChanged = value != commands;
                commands = value;
                if (raisePropChanged)
                {
                    PropertyChanged?.Invoke(this,
                        new System.ComponentModel.PropertyChangedEventArgs(nameof(Commands)));
                }
            }
        }

        /// <summary>
        /// Total count of commands (since we use server side paging)
        /// </summary>
        public int CommandCount
        {
            get => commandCount;
            set
            {
                var raisePropChanged = value != commandCount;
                commandCount = value;
                if (raisePropChanged)
                {
                    PropertyChanged?.Invoke(this,
                        new System.ComponentModel.PropertyChangedEventArgs(nameof(CommandCount)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}