using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Actions
{
    public interface IAction
    {
        public float Duration { get; }

        public float DurationCount { get; }

        /// <summary>Action每次循环被调用的部分</summary>
        /// <param name="gameTime"></param>
        /// <returns>
        /// <b>true</b>: Action执行完毕<br/>
        /// <b>false</b>: Action继续
        /// </returns>
        public bool ActionUpdate(GameTime gameTime);
    }


    public interface IAction<ArgType> : IAction where ArgType : IActionArg<ArgType>
    {
        Action<ArgType> Main { get; }

        ArgType Arg { get; }



    }
}
