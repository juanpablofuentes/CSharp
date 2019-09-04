using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class Sessions : BaseEntity
    {
        public DateTime DateLastActivity { get; set; }
        public int SecondsExpiration { get; set; }
        public int? AndroidVersion { get; set; }
        public string AndroidRelease { get; set; }
        public int? AppVersion { get; set; }
        public int UserConfigurationId { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
    }
}
