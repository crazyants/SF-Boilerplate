/*******************************************************************************
* 命名空间: SF.Web.Filters
*
* 功 能： N/A
* 类 名： FilterCriteria
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 10:31:50 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Newtonsoft.Json;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Filters
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FilterCriteria : IComparable
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Value { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Or { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonConverter(typeof(OperatorConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FilterOperator Operator { get; set; }

        /// <summary>left, right or none parenthesis</summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Open { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PId { get; set; }

        // Metadata
        public int MatchCount { get; set; }
        public int DisplayOrder { get; set; }
        public int DisplayOrderValues { get; set; }
        public bool IsInactive { get; set; }
        public string NameLocalized { get; set; }
        public string ValueLocalized { get; set; }

        public string SqlName
        {
            get
            {
                if (Entity.IsCaseInsensitiveEqual("Manufacturer") && !Name.Contains('.'))
                    return $"{Entity}.{Name}";

                return Name;
            }
        }

        public bool IsRange
        {
            get
            {
                return (Value.HasValue() && Value.Contains('~') && (Operator == FilterOperator.RangeGreaterEqualLessEqual || Operator == FilterOperator.RangeGreaterEqualLess));
            }
        }

        int IComparable.CompareTo(object obj)
        {
            var filter = (FilterCriteria)obj;

            var compare = string.Compare(this.Entity, filter.Entity, true);

            if (compare == 0)
            {
                compare = string.Compare(this.Name, filter.Name, true);

                if (compare == 0)
                    compare = string.Compare(this.Value, filter.Value, true);
            }

            return compare;
        }

        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception exc)
            {
                exc.Dump();
            }
            return "";
        }
    }
}
