using System;
using System.Collections.Generic;

namespace MyEStore.Entities;

public partial class ChuDe
{
    public int MaCd { get; set; }

    public string? TenCd { get; set; }

    public string? MaNv { get; set; }

    public virtual ICollection<GopY> Gopies { get; set; } = new List<GopY>();

    public virtual NhanVien? MaNvNavigation { get; set; }
}
