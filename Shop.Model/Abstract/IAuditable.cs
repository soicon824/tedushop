using System;

namespace Shop.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreateTime { get; set; }
        string CreatedBy { get; set; }

        DateTime? UpdateDate { get; set; }

        string  UpdateBy { get; set; }

        string MetaKeyword { get; set; }

        string MetaDescription { get; set; }

        bool Status { get; set; }
    }
}
