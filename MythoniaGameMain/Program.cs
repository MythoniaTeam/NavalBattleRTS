﻿using System;

namespace MythoniaGameMain
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
                game.Run();

        }
    }
}
