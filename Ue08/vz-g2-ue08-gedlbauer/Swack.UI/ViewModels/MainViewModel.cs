using Swack.Logic;
using Swack.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swack.UI.ViewModels
{
    public class MainViewModel
    {
        private readonly IMessagingLogic messagingLogic;
        private ChannelViewModel currentChannel;

        public ObservableCollection<ChannelViewModel> Channels { get; private set; }
        public ChannelViewModel CurrentChannel
        {
            get => currentChannel;
            set
            {
                if (currentChannel != value)
                {
                    currentChannel = value;
                    currentChannel.UnreadMessages = 0;
                }
            }
        }

        public MainViewModel(IMessagingLogic messagingLogic)
        {
            Channels = new ObservableCollection<ChannelViewModel>();
            this.messagingLogic = messagingLogic ?? throw new ArgumentNullException(nameof(messagingLogic));
        }

        public async Task InitializeAsync()
        {
            foreach (var channel in await messagingLogic.GetChannelsAsync())
            {
                Channels.Add(new ChannelViewModel(channel, messagingLogic));
            }

            messagingLogic.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(Message message)
        {
            var channelViewModel = Channels.SingleOrDefault(x => x.Channel.Name == message.Channel.Name);
            if (channelViewModel is not null)
            {
                channelViewModel.Messages.Add(message);
                if (channelViewModel != CurrentChannel)
                {
                    channelViewModel.UnreadMessages++;
                }
            }
        }
    }
}
