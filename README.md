## Unity Slot Machine Game

## Game Overview

This project is a 2D slot machine game developed using Unity and C#. The game features smooth reel animations, randomized outcomes using RNG logic, betting mechanics, payout calculations, audio feedback, and a WebGL browser build.

Players begin with a starting balance and can place different bet amounts before spinning the slot machine. Matching symbols reward the player with different payouts based on symbol rarity.

---

## Features

- Smooth reel spinning animation
- RNG-based randomized outcomes
- Betting system (10 / 50 / 100)
- Dynamic payout logic
- Matching symbol win detection
- Sequential reel stopping
- Arcade-style audio system
- Start screen UI
- Reel shake feedback
- WebGL browser support

---

## Symbol Payout Logic

|   Symbol   | Reward  |
|------------|---------|
| Cherry     | 2x Bet  |
| Bell       | 4x Bet  |
| Triple Bar | 6x Bet  |
| Diamond    | 8x Bet  |
| Seven      | 10x Bet |

- 3 matching symbols = Full reward
- 2 matching symbols = Bet amount returned

---

## Instructions to Run WebGL Build

### Option 1 — Play on itch.io
Playable browser version:
(https://isha-skylz.itch.io/lucky-jackpot-spin)

### Option 2 — Run Locally

Open the project in Unity 2022.3 LTS.

Go to:

File → Build Settings → WebGL

Then click:

Build And Run

The WebGL build files are included inside:

Build/WebGL

---

## Controls

- Press ENTER to start the game
- Select a bet amount
- Watch the reels spin
- Win rewards based on matching symbols

---

## Bonus Features

- Start screen panel
- Reel machine shake effect
- Handle pull animation
- Audio feedback system
- Sequential reel stopping for polished gameplay feel

---

## Thought Process / Approach

The main focus of this project was to create a clean and polished slot machine experience while maintaining simple and scalable code architecture.

The project was structured into separate gameplay systems including:
- Reel control system
- Betting and payout management
- Audio management
- UI handling
- Randomized result generation

To improve the player experience, additional polish elements such as reel shaking, sequential stopping, handle animations, and audio feedback were implemented.

The reels use randomized stopping positions combined with symbol mapping to simulate slot machine behavior while ensuring smooth gameplay and consistent symbol alignment.

---

## Tech Stack

- Unity 2022.3 LTS
- C#
- TextMeshPro
- WebGL Build Support
