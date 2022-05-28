


namespace Mythonia.Resources
{
    public class TempMClass : IMClass
    {

        #region Implement - IMClass 

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Props 

        #endregion



        #region Constructor 

        private TempMClass(MGame game, string name) { _game = game; _name = name; }
        public TempMClass(string namePrefix, IMClass mClass) : this(mClass.MGame, namePrefix + mClass.Name) { }

        #endregion



        #region Methods 

        #endregion



        #region Implement - Interface 

        #endregion

    }
}
