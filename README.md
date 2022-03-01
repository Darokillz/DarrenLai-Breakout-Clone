# DarrenLai-Breakout-Clone
My submission to the programming Test from Phat Loot Studio

## 1. Features
**Single Player Component**
- Make this in Unity 2020.3 LTS and onward. Don't use Unity 2021, This will be a 3D game with a 2D view.
- At least 5 layers of different colour bricks 10 across.
- A player-controlled paddle that moves left and right but doesn't go off-screen.
- The ball starts on the center of the paddle.
- When the player presses space with the ball on the paddle it will then launch up in a Random direction within the 90-degree angle shown below.
- If the ball falls into the red zone below the players this zone should not be visibl in-game then the ball must respawn on the player that lost it.
- There should be walls on the top, left, and right sides of the screen so the ball can continue to bounce off them.
- When the ball collides with a brick it should destroy the brick and bounce the ball
-  Show a Score counter at the top of the level to display how many blocks you have destroyed. Award 100 points per block destroyed.
-  Graphically the game can look however you want. The polishing stage is up to you.

**Multiplayer Component**

- Using Mirror API
- No lobby required. Use the Network Manager HUD to let two players join a level.
- Have the player movement sync for both clients with each client controlling their own paddle.
- Have a score that is synced to both players.
- Have a ball for each player.
- When the ball collides with the red surface, spawn it back on the player that launched it

## 2. Game Overview
**Single Player Component**
- Select the SinglePlayer Icon (left) to enter singleplayer mode
- use mouse to control the paddle movement
- press esc to pause, mute or return to menu

**Multiplayer  Component**
- HOST
  - Select the Multiplayer Icon (right) to enter Multiplayer mode
  - Use the NetworkManager HUD to host 
- Client
  - Select the Multiplayer Icon (right) to enter Multiplayer mode
  -  Use the NetworkManager HUD to join as client
