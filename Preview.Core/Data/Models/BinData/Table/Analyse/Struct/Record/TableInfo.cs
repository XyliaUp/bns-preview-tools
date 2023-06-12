using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xylia.Preview.Data.Models.BinData.Analyse.Enums;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq.Type;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record
{
    /// <summary>
    /// 配置表数据
    /// </summary>
    public sealed class TableInfo
    {
        #region List根节点数据
        public ushort ConfigMajorVersion = 0;

        public ushort ConfigMinorVersion = 1;

        public List<RecordDef> Records;

        public Dictionary<short, TypeRecordTable> RecordTables;



        /// <summary>
        /// 是否需要检查长度
        /// </summary>
        public bool CheckSize = false;

        /// <summary>
        /// 是否需要检查版本
        /// </summary>
        public bool CheckVersion = false;

        /// <summary>
        /// 自动编号起始值
        /// </summary>
        public uint AutoStart = 1;

        /// <summary>
        /// 存储相对路径
        /// </summary>
        public string RelativePath;


        public short Type = 0;

        /// <summary>
        /// 数据类型
        /// </summary>
        public string TypeName = "";

        /// <summary>
        /// 模块
        /// </summary>
        public string Module = "TextData";
        #endregion




        #region Fields
        /// <summary>
        /// 执行优先级 (等级越小越高)
        /// </summary>
        public int Priority = 1;

        /// <summary>
        /// 配置文件规则类型
        /// </summary>
        public List<ListRule> Rules = new();

        /// <summary>
        /// 类别记录器信息
        /// </summary>
        public TypeInfo TypeInfo;
        #endregion


        #region Functions
        /// <summary>
        /// 获取类型对应的记录器
        /// </summary>
        /// <param name="is64"></param>
        /// <param name="IsClient"></param>
        /// <returns></returns>
        public Dictionary<short, TypeRecordTable> GetRecordTable(bool is64, bool IsClient = true) => GetRecordTable(is64, r => IsClient ? r.Client :
           //生成服务端文件需要处理以下几种情况
           //非服务端属性 => 移除
           //需分类属性   => 分类
           //拥有字典类型的属性 => 进行数值转换
           /*r.SortBy || */r.HasSeq || r.Server || !r.Server);

        public Dictionary<short, TypeRecordTable> GetRecordTable(bool is64, Func<RecordDef, bool> predicate)
        {
            //Initialize对应类型的record数组
            var result = new Dictionary<short, TypeRecordTable>();
            foreach (var t in TypeInfo) result[t.Key] = new();

            #region 遍历记录器内容
            foreach (var Record in Records.Where(predicate))
            {
                #region 获取筛选类型
                var CurTypes = new List<short>();

                //检测当前属性是否满足此数据的类型 (type)
                if (Record.Filter.Count == 0) CurTypes.Add(-1);
                else Record.Filter.ForEach(f => CurTypes.Add((short)f));
                #endregion


                #region 插入对象
                foreach (var type in CurTypes)
                {
                    //如果存在TypeInfo中不存在的Filter值，进行提示
                    var RecordCopy = (RecordDef)Record.Clone();
                    if (!result.ContainsKey(type))
                    {
                        Console.WriteLine($"[信息] 筛选类型: {type} 未定义在类型组中");
                        result[type] = new();
                    }

                    result[type].Add(RecordCopy);
                }
                #endregion
            }
            #endregion


            var DefaultType = result[-1];
            DefaultType.GetOffsetAndSize(is64);

            foreach (var type in result)
            {
                if (type.Key == -1) continue;

                type.Value.GetOffsetAndSize(is64, DefaultType.Size);

                //合并通用类型和特殊类型专用数据
                type.Value.Records.InsertRange(0, DefaultType.Records);
            }


            return result;
        }
        #endregion
    }
}