
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

using Xylia.Extension;
using Xylia.Xml;

namespace Xylia.Preview.Data.Models.ZoneData.RegionData
{
    public class RegionFile
    {
        #region Fields
        /// <summary>
        /// 版本号
        /// </summary>
        public short Version;

        /// <summary>
        /// 文件总长度
        /// </summary>
        public long FileSize;

        /// <summary>
        /// 区域编号
        /// </summary>
        public int RegionID;

        public short Xmin;
        public short Xmax;
        public short Ymin;
        public short Ymax;

        /// <summary>
        /// X总范围
        /// </summary>
        public short XRange;

        /// <summary>
        /// Y总范围
        /// </summary>
        public short YRange;


        public List<RegionArea> RegionArea;
        #endregion


        #region Functions
        public void Read(string FilePath) => Read(new BinaryReader(new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)));

        public void Read(BinaryReader br)
        {
            #region 加载头部
            Version = br.ReadInt16();
            if (Version != 21) Console.WriteLine("地形版本号不为21");

            FileSize = br.ReadInt64();
            RegionID = br.ReadInt32();
            XRange = br.ReadInt16();
            YRange = br.ReadInt16();
            Xmin = br.ReadInt16();
            Xmax = br.ReadInt16();
            Ymin = br.ReadInt16();
            Ymax = br.ReadInt16();

            if (Xmax - Xmin + 1 != XRange) throw new Exception("X 边界异常");
            if (Ymax - Ymin + 1 != YRange) throw new Exception("Y 边界异常");
            Console.WriteLine($"Xmin={Xmin} | Xmax={Xmax} | Ymin={Ymin} | Ymax={Ymax} ({XRange * YRange} 区块)");

            long InfoSize = br.ReadInt64();   //头区域长度 (不包括版本号)
            long AreaOffset = br.ReadInt64(); //区块设置区偏移
            long UnkData = br.ReadInt64();    //未知数据
            if (UnkData != 0) throw new Exception("UnkData 异常");
            #endregion

            #region 获取区块信息
            //区块长度  XRange * YRange * 4
            var AreaReader = new BinaryReader(new MemoryStream(br.ReadBytes((int)(AreaOffset - InfoSize))));
            RegionArea = new();

            if (AreaReader.BaseStream.Length != XRange * YRange * 4)
                throw new Exception("缺失区块");

            for (int Param1 = 0; Param1 < XRange; Param1++)
            {
                for (int Param2 = 0; Param2 < YRange; Param2++)
                {
                    var Offset = AreaReader.ReadInt32();
                    if (Offset != -1) Trace.WriteLine($"[区块 {Param1} - {Param2}] {Offset}");

                    RegionArea.Add(new RegionArea()
                    {
                        X = Xmin + Param1 + 1,
                        Y = Ymin + Param2 + 1,

                        Offset = Offset,
                    });
                }
            }
            #endregion

            #region 读取区块设置区
            //如果所有区块的偏移点都为 -1，则读取结束。否则需要读取偏移区数据
            //偏移区域用于限制区域进入、龙脉和传送门对象的存在。
            if (AreaOffset != FileSize)
            {
                #region Initialize
                Console.WriteLine($"总字节: {br.BaseStream.Length}   剩余字节: " + (br.BaseStream.Length - br.BaseStream.Position));

                var OffsetArea = br.ReadToEnd();
                var OffsetAreaReader = new BinaryReader(new MemoryStream(OffsetArea));
                #endregion

                #region 获取结束偏移
                RegionArea LastArea = null;
                foreach (var a in RegionArea.Where(a => a.Offset != -1))
                {
                    if (LastArea != null) LastArea.EndOffset = a.Offset;
                    LastArea = a;
                }

                LastArea.EndOffset = OffsetArea.Length;
                #endregion

                #region 处理数据
                foreach (var a in RegionArea.Where(a => a.Offset != -1))
                {
                    //偏移区的数值变动似乎不会引起服务端报错
                    OffsetAreaReader.BaseStream.Position = a.Offset;
                    a.Data = OffsetAreaReader.ReadBytes(a.EndOffset - a.Offset);
                }
                #endregion
            }
            #endregion

            br.Close();
            br.Dispose();
        }

        public void Write(BinaryWriter bw)
        {
            XRange = (short)(Xmax - Xmin + 1);
            YRange = (short)(Ymax - Ymin + 1);

            #region 存储头部
            bw.Write(Version);
            bw.Write(0L);   //文件总大小
            bw.Write(RegionID);
            bw.Write(XRange);
            bw.Write(YRange);
            bw.Write(Xmin);
            bw.Write(Xmax);
            bw.Write(Ymin);
            bw.Write(Ymax);

            var InfoOffset = bw.BaseStream.Position;
            bw.Write(bw.BaseStream.Length + 22);  //InfoSize
            bw.Write(0L);     //AreaOffset			     
            bw.Write(0L);
            #endregion

            #region 存储区块信息
            int CurOffset = 0;
            foreach (var a in RegionArea)
            {
                if (a.Data is null || a.Data.Length == 0) bw.Write(-1);
                else
                {
                    bw.Write(CurOffset);
                    CurOffset += a.Data.Length;
                }
            }
            #endregion

            #region 存储区块设置区
            long OffsetArea = bw.BaseStream.Length;
            foreach (var a in RegionArea)
            {
                if (a.Data is not null && a.Data.Length != 0)
                    bw.Write(a.Data);
            }
            #endregion

            #region 重写区域偏移
            //重写区域数据区偏移
            bw.BaseStream.Position = InfoOffset + 8;
            bw.Write(OffsetArea - 2);

            //重写总长度信息
            bw.BaseStream.Position = 2;
            bw.Write(bw.BaseStream.Length - 2);
            #endregion
        }

        /// <summary>
        /// 存储测试
        /// </summary>
        /// <param name="bw"></param>
        public void WriteTest(BinaryWriter bw)
        {
            XRange = (short)(Xmax - Xmin + 1);
            YRange = (short)(Ymax - Ymin + 1);

            if (true)
            {
                RegionArea = new();
                for (int i = 0; i < XRange * YRange; i++) RegionArea.Add(new RegionArea());
            }

            #region 存储头部
            bw.Write(Version);
            bw.Write(0L);   //文件总大小
            bw.Write(RegionID);
            bw.Write(XRange);
            bw.Write(YRange);
            bw.Write(Xmin);
            bw.Write(Xmax);
            bw.Write(Ymin);
            bw.Write(Ymax);

            var InfoOffset = bw.BaseStream.Position;
            bw.Write(bw.BaseStream.Length + 22);  //InfoSize
            bw.Write(0L);     //AreaOffset			     
            bw.Write(0L);       //UnkData
            #endregion

            #region 存储区块信息
            foreach (var a in RegionArea)
            {
                bw.Write(-1);
            }
            #endregion

            #region 存储区块设置区
            bw.BaseStream.Position = InfoOffset + 8;
            bw.Write(bw.BaseStream.Length - 2);

            if (false)
            {
                bw.BaseStream.Position = bw.BaseStream.Length;

                //var SubData = "00000000000000000000000000000000000000000000000000000000006500000000000000000000000000006500000000000000000000650065000000000000000065006500650000000000650065006500650000000065650065006500650000000065006500650065000000000000000000000000000000650000650000000000000000006500000000650000000000000065000000000000000000000000000000650065000000000000000000000000000000650065000065000000000000000000650065006500000000000000000000000065000000000000000000000000000000000000650065006500000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000".ToBytes();
                //for (int i = 0; i < SubData.Length; i++)
                //{
                //	if (SubData[i] == 0) 
                //		SubData[i] = (byte)0;
                //}
                //bw.Write(SubData);

                bw.Write(new byte[128]);
                bw.Write(new byte[128]);
                bw.Write(new byte[128]);
                bw.Write(new byte[128]);
            }
            #endregion

            //重写总长度信息
            bw.BaseStream.Position = 2;
            bw.Write(bw.BaseStream.Length - 2);
        }
        #endregion

        #region Functions
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="IsTest"></param>
        public void Save(string FilePath, bool IsTest = true)
        {
            using BinaryWriter fw = new(new FileStream(FilePath, FileMode.Create));
            if (IsTest) WriteTest(fw);
            else Write(fw);

            Console.WriteLine("执行全部结束");
        }

        public void OutputTest(string Path)
        {
            XmlInfo xi = new();

            #region Main
            var Region = (XmlElement)xi.AppendChild(xi.CreateElement("region"));
            Region.SetAttribute("id", RegionID.ToString());
            Region.SetAttribute("Xmin", Xmin.ToString());
            Region.SetAttribute("Xmax", Xmax.ToString());
            Region.SetAttribute("Ymin", Ymin.ToString());
            Region.SetAttribute("Ymax", Ymax.ToString());
            Region.SetAttribute("XRange", XRange.ToString());
            Region.SetAttribute("YRange", YRange.ToString());
            #endregion

            #region Area
            foreach (var area in RegionArea)
            {
                var Area = (XmlElement)Region.AppendChild(xi.CreateElement("area"));
                Area.SetAttribute("X", area.X.ToString());
                Area.SetAttribute("Y", area.Y.ToString());

                if (area.Data is not null)
                {
                    var data = (XmlElement)Area.AppendChild(xi.CreateElement("data"));
                    data.SetAttribute("length", area.Data.Length.ToString());
                    data.InnerText = area.Data.ToHex(true);
                }
            }
            #endregion

            xi.Save(Path, true);
        }

        public void InputTest(string Path)
        {
            #region Initialize
            RegionArea = new List<RegionArea>();

            XmlDocument XmlDoc = new();
            XmlDoc.Load(Path);

            var Region = XmlDoc.SelectSingleNode("table/region");
            Xmin = short.Parse(Region.Attributes["Xmin"].Value);
            Xmax = short.Parse(Region.Attributes["Xmax"].Value);
            Ymin = short.Parse(Region.Attributes["Ymin"].Value);
            Ymax = short.Parse(Region.Attributes["Ymax"].Value);
            #endregion

            #region Area
            RegionArea = new List<RegionArea>();
            foreach (var Area in Region.SelectNodes("./area").OfType<XmlElement>())
            {
                //实际不会使用到，只是给用户看的
                var CurArea = new RegionArea()
                {
                    X = int.Parse(Area.Attributes["X"].Value),
                    Y = int.Parse(Area.Attributes["Y"].Value),
                };

                if (Area.HasChildNodes)
                {
                    var DataNode = Area.SelectSingleNode("./data");
                    if (DataNode is not null) CurArea.Data = DataNode.InnerText.ToBytes();
                }

                RegionArea.Add(CurArea);
            }
            #endregion
        }
        #endregion
    }
}