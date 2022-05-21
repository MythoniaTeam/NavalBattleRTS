using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources
{
    public class TempMClass : IMClass
    {
        //---------- Implement - IMClass ----------

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        //----------------------------------------



        //--------------- Props ---------------



        //--------------- Constructor ---------------

        private TempMClass(MGame game, string name) { _game = game; _name = name; }
        public TempMClass(string namePrefix, IMClass mClass) : this(mClass.MGame, namePrefix + mClass.Name) { }
            
        

        //--------------- Methods ---------------



        //---------- Implement - Interface ----------



    }
}
