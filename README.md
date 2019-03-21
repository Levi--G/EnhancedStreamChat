# Mod Info
EnhancedTwitchChat is a rich text Twitch chat integration mod with full unicode, emote, cheermote, and emoji support.

# Features
- Full Rich Text Support, see ALL of your Twitch chat when immersed in Beat Saber!
  - This includes all Twitch badges, emotes, cheermotes, BetterTwitchTV emotes, FrankerFaceZ emotes, all Emojis and even animated emotes!
  - This also includes full Unicode Support! This means you can enjoy the chat in any language!
- Song Requests!
  - Automatically queue/process song requests directly from your Twitch chat!
  - Use commands !bsr, !request, or !add (by default) to request songs by name or BeatSaver ID!
  - These commands are fully customizable! See the [config](https://github.com/brian91292/BeatSaber-EnhancedTwitchChat#config) section below for more information!
- Customizable User Interface
  - Change the scale, width, text/background colors, and chat order!
- *Coming soon:tm:*
  - Popular alert API integration with services such as streamlabs
  - More chat customization options!
  
# Dependencies
Enhanced Twitch Chat depends on [CustomUI](https://www.modsaber.org/mod/customui/), [SongLoader](https://www.modsaber.org/mod/song-loader/), and [AsyncTwitch](https://www.modsaber.org/mod/asynctwitchlib/). Make sure to install them, or Enhanced Twitch Chat won't work!
  
# Installation
Copy EnhancedTwitchChat.dll to your Beat Saber\Plugins folder, and install all of its dependencies. That's it!

# Usage
All you need to enter is the channel name which you want to join (see the `Setup` section below), the chat will show up to your right in game, and you can move it by pointing at it with the laser from your controller and grabbing it with the trigger. You can also move the chat towards you and away from you by pressing up and down on the analog stick or trackpad on your controller. Finally, you can use the lock button in the corner of the chat to lock the chat position in place so you don't accidentally move it.

# Setup
Most common options can be configured directly via the Beat Saber settings menu in the Enhanced Twitch Chat submenu, as seen in the image below. *Keep in mind that in order for song requests to appear in chat, you need to enter your TwitchUsername and TwitchOAuthToken in EnhancedTwitchChat.ini! If you do not do this, only YOU will see the song requests being accepted in the Enhanced Twitch Chat display!* For more information, see the `Config` section below.
![Enhanced Twitch Chat settings menu](https://i.imgur.com/GSPmjPb.jpg)

# Config
For the rest of the config options, you will have to manually edit the config files in the `UserData\EnhancedTwitchChat` folder.  *Keep in mind all config options will update in realtime when you save the file! This means you don't have to restart the game to see your changes!* Use the tables below as a guide for setting these values:

### TwitchLoginInfo.ini
| Option | Description |
| - | - |
| **TwitchChannelName** | The name of the Twitch channel whos chat you want to join (this is your Twitch username if you want to join your own channel) |
| **TwitchUsername** | Your twitch username for the account you want to send messages as in chat (only matters if you're using the request bot) |
| **TwitchOAuthToken** | The oauth token corresponding to the TwitchUsername entered above ([Click here to generate an oauth token](https://twitchapps.com/tmi/))  |

### EnhancedTwitchChat.ini
| Option | Description |
| - | - |
| **FontName** | The name of the system font that should be used for chat messages. You can specify any font installed on your computer! |
| **ChatScale** | How large the chat messages/emotes should be displayed. |
| **ChatWidth** | The width of the chat, regardless of ChatScale. |
| **LineSpacing** | Determines the amount of extra spacing between lines of chat |
| **MaxChatLines** | The maximum number of lines allowed in chat at once. |
| **PositionX/Y/Z** | The location of the chat in game (this can be adjusted in game, see description above!) |
| **RotationX/Y/Z** | The rotation of the chat in game (this can be adjusted in game, see description above!) |
| **TextColorR/G/B/A** | The color of chat messages, on a scale of 0-1. If your colors are between 0-255, just divide by 255 to get this value! |
| **BackgroundColorR/G/B/A** | The color of the chat background, on a scale of 0-1. If your colors are between 0-255, just divide by 255 to get this value! |
| **BackgroundPadding** | Determines how much empty space there will be around the borders of the chat. |
| **LockChatPosition** | Whether or not the chat can be moved by pointing at it with the controller laser and gripping with the trigger. |
| **ReverseChatOrder** | When set to true, chat messages will enter from the top and exit on bottom instead of entering on bottom and exiting on top. |
| **AnimatedEmotes** | When set to false, animated emotes/cheermotes will not move at all. |
| **DrawShadows** | When set to true, shadows will be drawn behind emotes/text (looks nicer in windowed view, not really noticeable in headset). |

### RequestBotSettings.ini
| Option | Description |
| - | - |
| **RequestBotEnabled** | When set to true, the song request bot will be accessible, and chat commands will be parsed. |
| **RequestQueueOpen** | When set to true, regular users can make song requests in chat (mods/broadcaster can make requests with queue closed). |
| **PersistentRequestQueue** | Whether or not the history of previously played song requests will be saved when you restart the game. |
| **RequestHistoryLimit** | The maximum number of requests to save a history of. |
| **UserRequestLimit** | The maximum number of requests a regular chat user can have in queue at a time. |
| **SubRequestLimit** | The maximum number of requests a subscriber can have in queue at a time. |
| **ModRequestLimit** | The maximum number of requests a moderator can have in queue at a time. |
| **VipBonusRequests** | Extra song requests for users with the VIP badge. |
| **SessionResetAfterXHours** | After this many hours, the duplicate song list will be cleared (this means you won't be able to have a song requested more than once within this amount of hours). |
| **LowestAllowedRating** | The minimum rating on BeatSaver to allow the song request to go through. Setting this value will allow for songs of any rating to be requested. |
| **AutoPickFirstSong** | When set to true, the song request bot will automatically choose the first request that matches if there are multiple results. Otherwise, will ask the user to narrow down their search. |
| **AllowModAddClosedQueue** | When set to true, moderators can add songs to the queue even when it is closed. |
| **SendNextSongBeingPlayedtoChat** | When set to true, the request bot will send a message in chat with info about the song when you play it from the queue. |
| **UpdateQueueStatusFiles** | When set to true, text files will be created and populated with information about the current request queue for use in streaming programs like OBS. |
| **MaximumQueueTextEntries** | The max number of entries that will be written to the queue text file (described above). |
| **BotPrefix** | A prefix that will be added to every message the bot sends to the chat, useful if you want to use a certain prefix to disable text to speech notifications for certain messages. |

# Compiling
To compile this mod simply clone the repo and update the project references to reference the corresponding assemblies in the `Beat Saber\Beat Saber_Data\Managed` folder, then compile. You may need to remove the post build event if your Beat Saber directory isn't at the same location as mine.

# Download
[Click here to download the latest EnhancedTwitchChat.dll](https://www.modsaber.org/mod/enhanced-twitch-chat/1.1.0)
