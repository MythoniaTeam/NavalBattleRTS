


using Microsoft.Xna.Framework;



namespace Mythonia.Resources.Data
{
    /// <summary>
    /// 表示九个方位的向量, ( X, Y ∈ { -1, 0, +1 } )
    /// </summary>
    public enum VecDir
    {
        /// <summary> (-1, 1): 左上, 即第二象限 </summary>
        TopLeft = 1,
        /// <summary> (0, 1): 上, 即Y正半轴 </summary>
        Top = 2,
        /// <summary> (0, 1): 右上, 即第一象限 </summary>
        TopRight = 3,
        /// <summary> (-1, 0): 左, 即X负半轴 </summary>
        Left = 4,
        /// <summary> (0, 0): 中, 即位置重合 </summary>
        Center = 5,
        /// <summary> (1, 0): 右, 即X正半轴 </summary>
        Right = 6,
        /// <summary> (-1, -1): 左下, 即第三象限 </summary>
        BottomLeft = 7,
        /// <summary> (0, -1): 下, 即Y负半轴 </summary>
        Bottom = 8,
        /// <summary> (1, -1): 右下, 即第四象限 </summary>
        BottomRight = 9,
    }

    


    public struct MVector : IVector, IEquatable<MVector>
    {
        #region Implement - IVector

        private Vector2 _vec;
        public float X
        {
            get => _vec.X;
            set => _vec.X = value;
        }
        public float Y
        {
            get => _vec.Y;
            set => _vec.Y = value;
        }


        public Vector2 Vec => _vec;

        #endregion



        #region Props

        /// <summary>向量的长度 (需要开方运算, 建议多使用 <see cref="LengthSquared"/> 替代)</summary>
        public float Length => _vec.Length();
        /// <summary>向量长度的平方</summary>
        public float LengthSquared => _vec.LengthSquared();
        /// <summary>向量的方向</summary>
        public float Direction => MathF.Atan2(Y, X);

        public MVector GetSign => new MVector(MathF.Sign(X), MathF.Sign(Y));

        /// <summary>获取一个基于该向量角度的<seealso cref="MAngle"/>对象</summary>
        public MAngle AsAngle => (MAngle)this;
        /// <summary>获取一个基于 (<see cref="X"/>, <see cref="Y"/>) 的, (<see cref="float"/>, <see cref="float"/>) 浮点元组对象</summary>
        public (float, float) AsFloat => (X, Y);
        /// <summary>获取一个基于该向量 大概方向 的<seealso cref="VecDir"/>对象</summary>
        public VecDir AsVecDir => (VecDir)this;

        #endregion



        #region Constructors

        public MVector(Vector2 vec) => _vec = vec;
        public MVector(float x, float y) => _vec = new(x, y);
        public MVector(VecDir dir, float x = 1, float y = 1) => _vec = new MVector(x, y) * (MVector)dir;

        public MVector(float x) : this(x, x) { }

        #endregion



        #region Methods - Cast on Self

        /// <summary>将向量四舍五入, 返回至自身</summary>
        public void Round() => _vec.Round();
        /// <summary>将向量向下取整, 返回至自身</summary>
        public void Floor() => _vec.Floor();
        /// <summary>将向量向上取整, 返回至自身</summary>
        public void Ceiling() => _vec.Ceiling();

        /// <summary>化为单位向量, 返回至自身</summary>
        public void Normalize() => _vec.Normalize();
        /// <summary>将向量的 X, Y 限制至一个范围内, 返回至自身</summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        public void Clamp(Vector2 min, Vector2 max) => _vec = _vec.Clamp(min, max);

        /// <summary>将向量的 X, Y 值交换, 返回至自身</summary>
        public void RevertXY() => _vec.RevertXY();

        /// <summary>将向量的 X, Y 转为绝对值, 返回至自身</summary>
        public void Abs() => _vec.Abs();

        /// <summary>沿着指定点, 反射该向量, 返回至自身</summary>
        /// <param name="normal"></param>
        public void Reflect(Vector2 normal) => _vec.Reflect(normal);


        /// <summary>将 <see cref="_vec"/> * (<paramref name="xSign"/>, <paramref name="ySign"/>), 将结果返回至一个新的对象</summary>
        /// <param name="xSign">x 的符号</param>
        /// <param name="ySign">y 的符号</param>
        /// <returns>一个新的 <see cref="MVector"/> 对象</returns>
        public MVector ChangeSign(float xSign, float ySign) => ChangeSign(new(xSign, ySign));

        /// <summary>将 <see cref="_vec"/> * <paramref name="Sign"/>, 将结果返回至一个新的对象</summary>
        /// <param name="Sign">x, y 的符号</param>
        /// <returns>一个新的 <see cref="MVector"/> 对象</returns>
        public MVector ChangeSign(Vector2 Sign) => _vec * Sign;

        /// <summary>将 <see cref="_vec"/>.Y * -1, 将结果返回至一个新的对象</summary>
        /// <returns>一个新的 <see cref="MVector"/> 对象</returns>
        public MVector ChangeSignY() => ChangeSign(1, -1);

        /// <summary>
        /// 给定一点, 获取该点位于自身的哪个方向 / 象限
        /// </summary>
        /// <param name="pt">目标点</param>
        /// <returns>
        /// (<see cref="MVector"/>, <seealse cref="VecDir"/>) 元组类型<br/>
        /// 后者表示<paramref name="pt"/>所处的象限, 前者为自身对象<br/>
        /// <para>
        /// 可配合拓展函数 For(..), 获取一个位移的点, eg:
        /// <code>TopLeftPoint.DirectTo(CenterPoint).For(20, 10)</code>
        /// 即可获得从左上角, 朝中心 <see cref="VecDir"/> 方向 xy 分别位移 20, 10 单位的坐标
        /// </para>
        /// </returns>
        public (MVector pos, VecDir dir) DirectTo(MVector pt) => (this, (VecDir)(pt - this));

        #endregion



        #region Static Methods 

        public static MVector Round(MVector v) => Vector2.Round(v);
        public static MVector Floor(MVector v) => Vector2.Floor(v);
        public static MVector Ceiling(MVector v) => Vector2.Ceiling(v);
        public static MVector Normalize(MVector v) => Vector2.Normalize(v);

        public static MVector Clamp(MVector v, MVector min, MVector max) => Vector2.Clamp(v, min, max);
        public static MVector Min(MVector v1, MVector v2) => Vector2.Min(v1, v2);
        public static MVector Max(MVector v1, MVector v2) => Vector2.Max(v1, v2);

        public static MVector Min(IList<MVector> vs)
        {
            MVector vMin = vs[0];
            foreach (MVector v in vs) vMin = Min(vMin, v);
            return vMin;
        }
        public static MVector Max(IList<MVector> vs)
        {
            MVector vMax = vs[0];
            foreach (MVector v in vs) vMax = Min(vMax, v);
            return vMax;
        }


        public static float Distance(Vector2 v1, Vector2 v2) => Vector2.Distance(v1, v2);
        public static float DistanceSquared(Vector2 v1, Vector2 v2) => Vector2.DistanceSquared(v1, v2);

        public static Vector2 Reflect(Vector2 v, Vector2 normal) => Vector2.Reflect(v, normal);

        #endregion



        #region Operators 

        #region Operators - Calculation
        public static MVector operator +(MVector v1, MVector v2) => v1.Vec + v2.Vec;
        public static MVector operator -(MVector v1, MVector v2) => v1.Vec - v2.Vec;
        public static MVector operator *(MVector v1, MVector v2) => v1.Vec * v2.Vec;
        public static MVector operator /(MVector v1, MVector v2) => v1.Vec / v2.Vec;
        public static MVector operator %(MVector v1, MVector v2) => new(v1.X % v2.X, v1.Y % v2.Y);


        public static MVector operator +(MVector v1, float v2) => v1.Vec + new Vector2(v2);
        public static MVector operator -(MVector v1, float v2) => v1.Vec - new Vector2(v2);
        public static MVector operator *(MVector v1, float v2) => v1.Vec * v2;
        public static MVector operator /(MVector v1, float v2) => v1.Vec / v2;

        public static MVector operator +(float v2, MVector v1) => v1.Vec + new Vector2(v2);
        public static MVector operator -(float v2, MVector v1) => v1.Vec - new Vector2(v2);
        public static MVector operator *(float v2, MVector v1) => v1.Vec * v2;
        public static MVector operator /(float v2, MVector v1) => v1.Vec / v2;

        public static MVector operator -(MVector v1) => -v1.Vec;

        #endregion


        #region Operators - Logic

        public static bool operator <(MVector v1, MVector v2) => v1.X < v2.X && v2.Y < v2.Y;
        public static bool operator >(MVector v1, MVector v2) => v1.X > v2.X && v2.Y > v2.Y;

        public static bool operator ==(MVector v1, float v2) => v1.X == v2 && v1.Y == v2;
        public static bool operator !=(MVector v1, float v2) => v1.X != v2 && v1.Y != v2;
        public static bool operator <=(MVector v1, float v2) => v1.X <= v2 && v1.Y <= v2;
        public static bool operator >=(MVector v1, float v2) => v1.X >= v2 && v1.Y >= v2;
        public static bool operator <(MVector v1, float v2) => v1.X < v2 && v1.Y < v2;
        public static bool operator >(MVector v1, float v2) => v1.X > v2 && v1.Y > v2;
        public static bool operator ==(float v1, MVector v2) => v1 == v2.X && v1 == v2.Y;
        public static bool operator !=(float v1, MVector v2) => v1 != v2.X && v1 != v2.Y;
        public static bool operator <=(float v1, MVector v2) => v1 <= v2.X && v1 <= v2.Y;
        public static bool operator >=(float v1, MVector v2) => v1 >= v2.X && v1 >= v2.Y;
        public static bool operator <(float v1, MVector v2) => v1 < v2.X && v1 < v2.Y;
        public static bool operator >(float v1, MVector v2) => v1 > v2.X && v1 > v2.Y;

        #endregion

        #endregion



        #region Override Methods

        public MVector Clone() => new MVector(Vec);

        public bool Equals(IVector v) => X == v.X && Y == v.Y;
        public bool Equals(MVector v) => Equals((IVector)v);
        public override bool Equals(object obj) => (obj is IVector vec) ? Equals(vec) : false;
        public override string ToString() => $"(X: {X}, Y: {Y})";

        #endregion



        #region Operators - Type Convert

        /// <summary>MVector 转 XNA.Vector2</summary>
        public static implicit operator Vector2(MVector v) => v.Vec;
        public static implicit operator Point(MVector v) => v.Vec.ToPoint();

        /// <summary>XNA.Vector2 转 MVector</summary>
        public static implicit operator MVector(Vector2 v) => new MVector(v);
        public static implicit operator MVector(Point v) => new MVector(v.X, v.Y);


        ///// <summary>Record 转 MVector</summary>
        //public static implicit operator MVector(VectorRecord v) => new MVector(v.Vec);

        public static implicit operator (float, float)(MVector v) => (v.X, v.Y);
        public static implicit operator MVector((float, float) v) => new(v.Item1, v.Item2);


        public static explicit operator MVector(string v)
        {
            string[] vSplited = v.Split(',');
            if (vSplited.Length != 2) throw new Exception($"The Given String has {vSplited.Length} elements after Splited by ',' but there Should have 2");
            float x = float.Parse(vSplited[0]);
            float y = float.Parse(vSplited[1]);
            return new(x, y);
        }



        ///// <summary>
        ///// 给定一个以中心点为 (0, 0), 左下 / 右上角为 -/+ (1, 1) 的比例向量, 和一个尺寸, 算出该比例代表的坐标 (以左上为原点的坐标系)
        ///// </summary>
        ///// <returns></returns>
        //public MVector ScaleToScreenPosition(MVector size, bool topLeft = true)
        //{
        //    MVector v = this.ChangeSignY();
        //    v = (v + (topLeft ? 1 : 0)) / 2;
        //    return v * size;
        //}

        
        public static implicit operator MVector(VecDir v) => v switch
        {
            VecDir.TopLeft => (-1, 1),
            VecDir.Top => (0, 1),
            VecDir.TopRight => (1, 1),
            VecDir.Left => (-1, 0),
            VecDir.Center => (0, 0),
            VecDir.Right => (1, 0),
            VecDir.BottomLeft => (-1, -1),
            VecDir.Bottom => (0, -1),
            VecDir.BottomRight => (1, -1),
            _ => throw new IndexOutOfRangeException($"The Value of Enum \"VecDir\" should belong the range [1, 9], but it's {v} now"),
        };
        /// <summary>
        /// 给一个向量, 将它转换为VecDir类型 (符号相同的单位向量)
        /// </summary>
        public static explicit operator VecDir(MVector v) => v.GetSign.AsFloat switch
        {
            (-1, 1) => VecDir.TopLeft,
            (0, 1) => VecDir.Top,
            (1, 1) => VecDir.TopRight,
            (-1, 0) => VecDir.Left,
            (0, 0) => VecDir.Center,
            (1, 0) => VecDir.Right,
            (-1, -1) => VecDir.BottomLeft,
            (0, -1) => VecDir.Bottom,
            (1, -1) => VecDir.BottomRight,
            _ => throw new Exception($"The Given MVector is not Unitize (value should be -1, 0, or 1), it's value is {v} now"),
        };


        #endregion



    }






}
