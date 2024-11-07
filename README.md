# TestGame Multiplayer PvP Game Documentation
This project demonstrates a multiplayer PvP game built with Unity and Google Firebase Realtime Database. Players can join the game world simultaneously, with their movements and actions synchronized in real-time across all connected clients.

## Core Components

### MultiplayerManager.cs
The central component handling all multiplayer functionality and Firebase integration.

Key features:
- Real-time player position synchronization
- Animation state synchronization
- Player spawning/despawning
- Firebase database connection management

### Player Components

#### thirdpersonmovement.cs
Handles individual player movement and animations:
- WASD movement with camera-relative direction
- Jump mechanics (double jump supported)
- Animation state management
- Smooth rotation and movement transitions

#### playermanager.cs
Singleton pattern implementation for managing the local player instance.

### Enemy System

#### enemyboi.cs
Basic enemy AI using Unity's NavMeshAgent:
- Automatically tracks and follows players
- Uses Unity's navigation system for pathfinding

#### EnemyManager.cs
Manages enemy spawning:
- Spawns enemies on 'F' key press
- Maintains list of active enemies

### Camera System

#### CameraManager.cs
Manages camera behavior using Cinemachine:
- Implements free-look camera
- Cursor locking for better game control
- Camera priority management

Demo Videos (Couldnt find any videos for the recent ones) 
# Multiplayer

https://photos.app.goo.gl/hdSTUqzT3DSqA8nJ8

https://photos.app.goo.gl/fJgU9NzSCnkatsfJ6

https://photos.app.goo.gl/8LvcRZjQxkoFhNo1A

# Place Holder
# Day/Night Cycle
# Sprinting and Fatigue
# Health and Regen and PVP

## Firebase Integration

### Database Structure

![image](https://github.com/user-attachments/assets/7f342d10-7a36-456f-a09f-839fca70b9bd)

