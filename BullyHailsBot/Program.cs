using System;
using System.IO;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;


namespace BullyHailsBot
{
    class Program
    {
        static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        protected SocketUser hailey;
        private protected string token; 

        public async Task MainAsync()
        {
            token = OpenToken();
            DiscordSocketConfig conf = new DiscordSocketConfig { AlwaysDownloadUsers = true };
            client = new DiscordSocketClient(conf);
            client.Log += Log;

            client.Ready += GuildsReady;
            client.MessageReceived += MessageRecievedAsync;

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            

            await Task.Delay(-1);
            await client.StopAsync();
            await client.LogoutAsync();
        }

        async Task GuildsReady()
        {
            await client.DownloadUsersAsync(client.Guilds);
            Console.WriteLine("Users Downoaded");
            hailey = client.GetUser(407625977594904587);//client.GetUser("tokyoshi", "1291");
            Console.WriteLine(hailey.Id);
            
        }

        async Task MessageRecievedAsync(SocketMessage msg)
        {
            //await msg.Channel.SendMessageAsync($"Got Message from {msg.Author.Username}");
            Console.WriteLine($"{msg.Author}    {hailey}    {msg.Author == hailey}");
            if(msg.Author.Id.Equals(hailey.Id))
            {
                Console.WriteLine("Bullied Hailey :)");
                await msg.AddReactionAsync(new Emoji("\uD83D\uDC4E"));
            }
        }

        private string OpenToken()
        {
            StreamReader reader = new StreamReader("Token.txt");
            return reader.ReadToEnd();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
