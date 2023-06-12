using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xylia.Preview.Data.Models.ZoneData.RegionData
{
    public class RegionArea
    {
        public int X;

        public int Y;

        /// <summary>
        /// 读取偏移
        /// </summary>
        public int Offset;

        /// <summary>
        /// 结束偏移，读取时使用
        /// </summary>
        public int EndOffset;


        public byte[] Data;
    }
}
