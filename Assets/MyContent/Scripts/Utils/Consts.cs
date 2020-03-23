using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Consts
{
    public static class UserGame
    {
        public const int PLAYER_MAX_LIVES = 3;

        public const string PLAYER_LIVES = "PlayerLives";
        public const string PLAYER_ENTRY = "PlayerEntry";
        public const string PLAYER_READY = "IsPlayerReady";
        public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
        public const float PLAYER_RESPAWN_TIME = 3.0f;
        public const float PLAYER_FORCE_TO_EXPLOTION = 10000;
    }

    public static class Layers
    {
        public const int ENEMIES_NUM_LAYER = 8;
        public const string ENEMIES_LABEL_LAYER = "Enemies";
        public const int PLAYER_NUM_LAYER = 9;
        public const string PLAYER_LABEL_LAYER = "Player";
        public const int BULLETS_NUM_LAYER = 10;
        public const string BULLETS_LABEL_LAYER = "Bullets";
    }

    public static class Methods
    {
        public static readonly Action Noob = () => { };
    }
}

