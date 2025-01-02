using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using View.UI;

namespace Core
{
    public class GameLog
    {
        private static GameLogView gameLogView;

        public static void SetGameLogView(GameLogView gameLogView)
        {
            GameLog.gameLogView = gameLogView;
        }

        public static void AddMassage(string massage)
        {
            gameLogView.PrintMassage(massage);
        }
    }
}