using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using SF.Web.Control.JqGrid.Infrastructure.Options;
using SF.Web.Control.JqGrid.Core.Response;

namespace SF.Web.Control.JqGrid.Core.Json.Converters
{
    /// <summary>
    /// Converts JqGridResponse to JSON. 
    /// </summary>
    internal sealed class JqGridResponseJsonConverter : JsonConverter
    {
        #region Fields
        private Type _jqGridResponseType = typeof(JqGridResponse);
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can write JSON.
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == _jqGridResponseType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WriteJqGridResponse(writer, serializer, (JqGridResponse)value);
        }

        private static void WriteJqGridResponse(JsonWriter writer, JsonSerializer serializer, JqGridResponse response)
        {
            JqGridJsonReader jsonReader = (response.Reader == null) ? JqGridResponse.JsonReader : response.Reader;

            writer.WriteStartObject();

            if (!response.IsSubgridResponse)
            {
                WritePagingProperties(writer, jsonReader, response);
                WrtieUserData(writer, serializer, jsonReader, response);
            }

            WriteJqGridRecords(writer, serializer, response.IsSubgridResponse, jsonReader, response.Records);

            writer.WriteEnd();
        }

        private static void WritePagingProperties(JsonWriter writer, JqGridJsonReader jsonReader, JqGridResponse response)
        {
            writer.WritePropertyName(jsonReader.PageIndex);
            writer.WriteValue(response.PageIndex + 1);

            writer.WritePropertyName(jsonReader.TotalRecordsCount);
            writer.WriteValue(response.TotalRecordsCount);

            writer.WritePropertyName(jsonReader.TotalPagesCount);
            writer.WriteValue(response.TotalPagesCount);
        }

        private static void WrtieUserData(JsonWriter writer, JsonSerializer serializer, JqGridJsonReader jsonReader, JqGridResponse response)
        {
            if (response.UserData != null)
            {
                writer.WritePropertyName(jsonReader.UserData);
                serializer.Serialize(writer, response.UserData);
            }
        }

        private static void WriteJqGridRecords(JsonWriter writer, JsonSerializer serializer, bool isSubgridResponse, JqGridJsonReader jsonReader, IList<JqGridRecord> records)
        {
            int recordIdIndex = 0;
            bool isRecordIndexInt = Int32.TryParse(jsonReader.RecordId, out recordIdIndex);
            bool isRecordValuesEmpty = String.IsNullOrWhiteSpace(jsonReader.RecordValues);
            bool repeatItems = isSubgridResponse ? jsonReader.SubgridReader.RepeatItems : jsonReader.RepeatItems;

            if (!isSubgridResponse)
            {
                if (repeatItems && isRecordValuesEmpty && !isRecordIndexInt)
                {
                    throw new InvalidOperationException("JqGridJsonReader.RecordId must be a number when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordValues is set to empty string.");
                }

                if (repeatItems && !isRecordValuesEmpty && isRecordIndexInt)
                {
                    throw new InvalidOperationException("JqGridJsonReader.RecordValues can't be an empty string when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordId is a number.");
                }
            }

            Func<JqGridJsonReader, JqGridRecord, bool, int, bool, object> serializeRecordFunction = ChooseSerializeFunction(repeatItems, isRecordValuesEmpty);
            IList<object> serializedRecords = new List<object>(records.Select(record => serializeRecordFunction(jsonReader, record, isRecordIndexInt, recordIdIndex, isSubgridResponse)));

            writer.WritePropertyName(isSubgridResponse ? jsonReader.SubgridReader.Records : jsonReader.Records);
            serializer.Serialize(writer, serializedRecords);
        }

        private static Func<JqGridJsonReader, JqGridRecord, bool, int, bool, object> ChooseSerializeFunction(bool repeatItems, bool isRecordValuesEmpty)
        {
            Func<JqGridJsonReader, JqGridRecord, bool, int, bool, object> serializeRecordFunction = null;

            if (repeatItems)
            {
                if (isRecordValuesEmpty)
                {
                    serializeRecordFunction = SerializeValuesRecordAsList;
                }
                else
                {

                    serializeRecordFunction = SerializeValuesRecordAsDictionary;
                }
            }
            else
            {
                serializeRecordFunction = SerializeValueRecordAsDictionary;
            }

            return serializeRecordFunction;

        }

        private static IList<object> SerializeValuesRecordAsList(JqGridJsonReader jsonReader, JqGridRecord record, bool isRecordIndexInt, int recordIdIndex, bool isSubgridResponse)
        {
            IList<object> recordValues = new List<object>();

            if (!isSubgridResponse && Convert.ToString(recordValues[recordIdIndex]) != record.Id)
            {
                recordValues.Add(record.Id);
            }
            
            return AppendValuesToValuesList(recordValues, record, isSubgridResponse);
        }

        private static IDictionary<string, object> SerializeValuesRecordAsDictionary(JqGridJsonReader jsonReader, JqGridRecord record, bool isRecordIndexInt, int recordIdIndex, bool isSubgridResponse)
        {
            IDictionary<string, object> recordValues = new Dictionary<string, object>();

            if (!isSubgridResponse)
            {
                recordValues.Add(jsonReader.RecordId, record.Id);
            }

            recordValues.Add(isSubgridResponse ? jsonReader.SubgridReader.RecordValues : jsonReader.RecordValues, AppendValuesToValuesList(new List<object>(), record, isSubgridResponse));

            return recordValues;
        }

        private static IList<object> AppendValuesToValuesList(IList<object> valuesList, JqGridRecord record, bool isSubgridResponse)
        {
            foreach (object value in record.Values)
            {
                valuesList.Add(value);
            }

            if (!isSubgridResponse)
            {
                valuesList = AppendTreeGridValuesToValuesList(valuesList, record);
            }

            return valuesList;
        }

        private static IList<object> AppendTreeGridValuesToValuesList(IList<object> valuesList, JqGridRecord record)
        {
            JqGridAdjacencyTreeRecord adjacencyTreeRecord = record as JqGridAdjacencyTreeRecord;
            JqGridNestedSetTreeRecord nestedSetTreeRecord = record as JqGridNestedSetTreeRecord;

            if (adjacencyTreeRecord != null)
            {
                valuesList.Add(adjacencyTreeRecord.Level);
                valuesList.Add(adjacencyTreeRecord.ParentId);
                valuesList.Add(adjacencyTreeRecord.Leaf);
                valuesList.Add(adjacencyTreeRecord.Expanded);
            }
            else if (nestedSetTreeRecord != null)
            {
                valuesList.Add(nestedSetTreeRecord.Level);
                valuesList.Add(nestedSetTreeRecord.LeftField);
                valuesList.Add(nestedSetTreeRecord.RightField);
                valuesList.Add(nestedSetTreeRecord.Leaf);
                valuesList.Add(nestedSetTreeRecord.Expanded);
            }

            return valuesList;
        }

        private static IDictionary<string, object> SerializeValueRecordAsDictionary(JqGridJsonReader jsonReader, JqGridRecord record, bool isRecordIndexInt, int recordIdIndex, bool isSubgridResponse)
        {
            if (record.Value == null)
            {
                throw new InvalidOperationException("JqGridRecord.Value can't be null when JqGridJsonReader.RepeatItems is set to false.");
            }

            IDictionary<string, object> recordValues = record.GetValueAsDictionary();

            if (!isSubgridResponse && !isRecordIndexInt && !recordValues.ContainsKey(jsonReader.RecordId))
            {
                recordValues.Add(jsonReader.RecordId, record.Id);
            }

            if (!isSubgridResponse)
            {
                recordValues = AppendTreeGridValuesToValuesDictionary(recordValues, record);
            }

            return recordValues;
        }

        private static IDictionary<string, object> AppendTreeGridValuesToValuesDictionary(IDictionary<string, object> valuesDictionary, JqGridRecord record)
        {
            JqGridAdjacencyTreeRecord adjacencyTreeRecord = record as JqGridAdjacencyTreeRecord;
            JqGridNestedSetTreeRecord nestedSetTreeRecord = record as JqGridNestedSetTreeRecord;

            if (adjacencyTreeRecord != null)
            {
                valuesDictionary.Add("level", adjacencyTreeRecord.Level);
                valuesDictionary.Add("parent", adjacencyTreeRecord.ParentId);
                valuesDictionary.Add("isLeaf", adjacencyTreeRecord.Leaf);
                valuesDictionary.Add("expanded", adjacencyTreeRecord.Expanded);
            }
            else if (nestedSetTreeRecord != null)
            {
                valuesDictionary.Add("level", nestedSetTreeRecord.Level);
                valuesDictionary.Add("lft", nestedSetTreeRecord.LeftField);
                valuesDictionary.Add("rgt", nestedSetTreeRecord.RightField);
                valuesDictionary.Add("isLeaf", nestedSetTreeRecord.Leaf);
                valuesDictionary.Add("expanded", nestedSetTreeRecord.Expanded);
            }

            return valuesDictionary;
        }
        #endregion
    }
}
