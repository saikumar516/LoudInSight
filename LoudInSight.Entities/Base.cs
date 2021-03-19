using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LoudInSight.Entities
{
    public class Base: Error
    {
        private DateTime _CreatedDate = DateTime.UtcNow;
        private DateTime _UpdatedDate = DateTime.UtcNow;
        private bool _IsActive=true;

        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedDate { get { return _CreatedDate; } }
        public DateTime UpdatedDate { get { return _UpdatedDate; } }
        public bool IsActive { get { return _IsActive; } }
	}
}
