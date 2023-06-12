using System.Collections.Generic;
using System.Linq;
using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Extension;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record
{
    /// <summary>
    /// 记录器集合
    /// </summary>
    public sealed class TypeRecordTable
    {
        #region Constructor
        public TypeRecordTable() { }

        public TypeRecordTable(List<RecordDef> Records) => AddRange(Records);

        public TypeRecordTable(TypeRecordTable RecordTable) => AddRange(RecordTable.Records);
        #endregion


        #region Fields
        /// <summary>
        /// 当前类型的记录器
        /// </summary>
        public List<RecordDef> Records = new();

        /// <summary>
        /// 最大数据长度
        /// </summary>
        public int Size;
        #endregion


        #region ICloneable
        public TypeRecordTable Clone() => (TypeRecordTable)MemberwiseClone();
        #endregion

        #region Functions
        public void Add(RecordDef record)
        {
            Records.Add(record);
            RefreshInfo(record);
        }

        public void AddRange(TypeRecordTable RecordTable) => AddRange(RecordTable.Records);

        public void AddRange(List<RecordDef> Records) => Records.ForEach(r => Add(r));




        public void GetOffsetAndSize(bool is64, int IdxOffset = 0)
        {
            bool HasKey = false;
            bool HasCommon = IdxOffset != 0;


            foreach (var record in Records)
            {
                if (!record.Client) continue;


                #region 校验数据对齐关系
                if (record.IsKey)
                {
                    if (!HasKey)
                    {
                        IdxOffset = 8;
                        HasKey = true;
                    }
                }
                else
                {
                    if (!HasCommon)
                    {
                        IdxOffset = 16;
                        HasCommon = true;
                    }
                }


                //已定义起始信息时
                if (record.Offset != 0) IdxOffset = record.Offset;

                //获取类型长度
                record.Size = (ushort)record.Type.GetLength(is64);
                if (record.Size == 2) IdxOffset = IdxOffset.Align(2);
                else if (record.Size != 1) IdxOffset = IdxOffset.Align(4);
                #endregion

                #region 赋值对象
                record.Offset = (ushort)IdxOffset;

                //生成新的别名
                if (record.Alias.MyEquals("unk-"))
                    record.Alias = "unk" + record.Offset;
                #endregion

                //计算下一个起始索引
                IdxOffset += record.Size * record.Repeat;
            }

            Size = IdxOffset.Align(4);
        }










        /// <summary>
        /// 在增加时刷新长度信息
        /// </summary>
        /// <param name="record"></param>
        private void RefreshInfo(RecordDef record)
        {
            var CurEndIndex = record.EndIndex;
            if (CurEndIndex <= Size) return;

            //总长度肯定是4的倍数
            Size = CurEndIndex.Align(4);
        }

        /// <summary>
        /// 增加全部完成后刷新长度信息
        /// </summary>
        public void RefreshInfo() => Size = Records.Count == 0 ? 0 : Records.Max(r => r.EndIndex).Align(4);
        #endregion
    }
}