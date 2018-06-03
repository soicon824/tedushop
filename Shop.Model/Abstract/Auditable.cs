using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreateTime { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        [MaxLength(256)]
        public string UpdateBy { get; set; }
        [MaxLength(256)]
        public string MetaKeyword { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
    }
}
