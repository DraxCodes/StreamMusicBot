using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;

namespace StreamMusicBot.Services
{
    public class MusicService
    {
        private LavaRestClient _lavaRestClient;
        private LavaSocketClient _lavaSocketClient;
        private DiscordSocketClient _client;
        private LavaPlayer _player;

        public MusicService(LavaRestClient lavaRestClient, DiscordSocketClient client, LavaSocketClient lavaSocketClient)
        {
            _client = client;
            _lavaRestClient = lavaRestClient;
            _lavaSocketClient = lavaSocketClient;
        }

        public Task InitializeAsync()
        {
            _client.Ready += ClientReadyAsync;
            _lavaSocketClient.Log += LogAsync;
            _lavaSocketClient.OnTrackFinished += TrackFinished;
            return Task.CompletedTask;
        }

        public async Task ConnectAsync(SocketVoiceChannel voiceChannel, ITextChannel textChannel)
            => await _lavaSocketClient.ConnectAsync(voiceChannel, textChannel);

        public async Task LeaveAsync(SocketVoiceChannel voiceChannel)
            => await _lavaSocketClient.DisconnectAsync(voiceChannel);

        public async Task<string> PlayAsync(string query, ulong guildId)
        {
            _player = _lavaSocketClient.GetPlayer(guildId);
            var results = await _lavaRestClient.SearchYouTubeAsync(query);
            if (results.LoadType == LoadType.NoMatches || results.LoadType == LoadType.LoadFailed)
            {
                return "No matches found.";
            }

            var track = results.Tracks.FirstOrDefault();

            if (_player.IsPlaying)
            {
                _player.Queue.Enqueue(track);
                return $"{track.Title} has been added to the queue.";
            }
            else
            {
                await _player.PlayAsync(track);
                return $"Now Playing: {track.Title}";
            }
        }

        public async Task StopAsync()
        {
            if (_player is null)
                return;
            await _player.StopAsync();
        }

        public async Task<string> SkipAsync()
        {
            if (_player is null || _player.Queue.Items.Count() is 0)
                return "Nothing in queue.";

            var oldTrack = _player.CurrentTrack;
            await _player.SkipAsync();
            return $"Skiped: {oldTrack.Title} \nNow Playing: {_player.CurrentTrack.Title}";
        }

        public async Task<string> SetVolumeAsync(int vol)
        {
            if (_player is null)
                return "Player isn't playing.";

            if (vol > 150 || vol <= 2)
            {
                return "Please use a number between 2 - 150";
            }

            await _player.SetVolumeAsync(vol);
            return $"Volume set to: {vol}";
        }

        public async Task<string> PauseOrResumeAsync()
        {
            if (_player is null)
                return "Player isn't playing.";

            if (!_player.IsPaused)
            {
                await _player.PauseAsync();
                return "Player is Paused.";
            }
            else
            {
                await _player.ResumeAsync();
                return "Playback resumed.";
            }
        }

        public async Task<string> ResumeAsync()
        {
            if(_player is null)
                return "Player isn't playing.";

            if (_player.IsPaused)
            {
                await _player.ResumeAsync();
                return "Playback resumed.";
            }

            return "Player is not paused.";
        }


        private async Task ClientReadyAsync()
        {
            await _lavaSocketClient.StartAsync(_client);
        }

        private async Task TrackFinished(LavaPlayer player, LavaTrack track, TrackEndReason reason)
        {
            if (!reason.ShouldPlayNext())
                return;

            if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
            {
                await player.TextChannel.SendMessageAsync("There are no more tracks in the queue.");
                return;
            }

            await player.PlayAsync(nextTrack);
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }
    }
}
