using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LoudInSight.Entities
{
    public class Base
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        //[DataMember]
        //public string MongoId
        //{
        //    get { return _id.ToString(); }
        //    set { _id = ObjectId.Parse(value); }
        //}
        //public string _id { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
