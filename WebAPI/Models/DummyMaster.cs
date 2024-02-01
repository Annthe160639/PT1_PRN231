using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public partial class DummyMaster
    {
        public DummyMaster()
        {
            DummyDetails = new HashSet<DummyDetail>();
        }

        public int MasterId { get; set; }
        public string MasterName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<DummyDetail> DummyDetails { get; set; }
    }
}
