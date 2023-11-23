using Swack.Logic;
using Swack.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Swack.UI.ViewModels
{
    public class ChannelViewModel : INotifyPropertyChanged
    {
        private readonly IMessagingLogic messagingLogic;
        private int unreadMessages;
        private string currentMessage;

        public event PropertyChangedEventHandler PropertyChanged;
        public int UnreadMessages
        {
            get => unreadMessages;
            set
            {
                if (unreadMessages != value)
                {
                    unreadMessages = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnreadMessages)));
                }
            }
        }
        public string CurrentMessage
        {
            get => currentMessage;
            set
            {
                if (currentMessage != value)
                {
                    currentMessage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMessage)));
                }
            }
        }
        public ObservableCollection<Message> Messages { get; set; }
        public ICommand SendMessageCommand { get; init; }
        public Channel Channel { get; }

        public ChannelViewModel(Channel channel, IMessagingLogic messagingLogic)
        {
            SendMessageCommand = new AsyncDelegateCommand(
                SendMessageAsync,
                _ => !string.IsNullOrEmpty(CurrentMessage)
            );
            Messages = new ObservableCollection<Message>();
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
            this.messagingLogic = messagingLogic ?? throw new ArgumentNullException(nameof(messagingLogic));
        }

        private async Task SendMessageAsync(object arg)
        {
            await messagingLogic.SendMessageAsync(Channel, CurrentMessage);
            CurrentMessage = null;
        }
    }
}
