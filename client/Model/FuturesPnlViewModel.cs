using CryptobotUi.Models.Cryptodb;
using System.Collections.Generic;
using System.ComponentModel;

namespace CryptobotUi.Client.Model
{
    public class FuturesPnlViewModel : Pnl, INotifyPropertyChanged
    {
        private IEnumerable<SignalCommand> commands;
        private int commandCount;

        public IEnumerable<SignalCommand> Commands
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

        public string SignalId =>  signal_id?.ToString();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}