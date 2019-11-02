### Simple MusicBot Built On [Stream](https://www.youtube.com/watch?v=QwYmRNlgzaA)
##### This is a super simple starter example of how you can build your very own Music Bot in C# (Dotnet Core) using the following libraries. Please note that it also has a vod of the stream I did, in which I explain each aspect of the code as I write it. (Linked above in the title) 

Music Library Used: [Victoria](https://github.com/Yucked/Victoria) - [Wiki](https://github.com/Yucked/Victoria/wiki)

Discord Library Used: [Discord.Net](https://github.com/discord-net/Discord.Net) - [Wiki](https://docs.stillu.cc/)

#### Before Setup

Before you get going on writing the code for your music bot. Make sure you have the latest java runtime installed as-well as LavaLink. Information about how to do this can be found on the Victoria Wiki (Linked above).

#### Setup

1. Start by forking this repository or simply download this repo as a zip file.
2. Once you have it on your PC, open the solution file which will then open the whole project in Visual Studio.
3. Rename `Config_Template.json` to `Config.json` and set your bot token in the area that has `CHANGE ME`.
      * Ensure you are providing a valid bot token otherwise it wont work.
      * Ensure you save all for the changes to take effect. <kbd>ctrl</kbd> + <kbd>s</kbd>
4. Edit what you want to edit, this part is up to you. If you get stuck, join the Victoria Discord server.
      - This can be found linked on the Victoria repository (linked above).
5. Ensure LavaLink is setup and online.
      - See the [Before Setup](#before-setup) section above for further info.
6. Start the bot in debug and wait for it to connect to Discord.
7. Ensure it is connected to LavaLink by waiting for the connection output in the console window.
8. If all goes well and you are happy, publish your bot however you see fit.
9. Start using your new Personal Music Bot.

#### Further Information

- If you find any issues with this example, please ensure you either let me know via the Victoria Discord Server or create an issue here on Github.
- The repo here is more up to date than the VOD of the stream I did. Please refer to the latest commits for things that may have changed or been fixed.
- This bot requires LavaLink to be running alongside the bot itself. You can start the proccess with the bot automatically. If you wish to do this then just come join the Victoria Discord Server and we can help you out to get that setup.
