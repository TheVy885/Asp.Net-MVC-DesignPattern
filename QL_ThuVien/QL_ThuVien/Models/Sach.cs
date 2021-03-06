//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using QL_ThuVien.Models;
    using System.Data;
    using System.IO;
    using System.Linq;

    public interface BookPrototype //tao interface bookPrototype
    {
        BookPrototype Clone();
    }

    public partial class Sach :BookPrototype
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.CT_PM = new HashSet<CT_PM>();
            HinhAnh = "~/Content/Image/sach.jpg";
        }

        public int IDSach { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public string MoTa { get; set; }
        public string TacGia { get; set; }
        public Nullable<System.DateTime> NgayXuatBan { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PM> CT_PM { get; set; }
        public virtual TheLoai TheLoai1 { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }


        public BookPrototype Clone() //clone 1 quyen s?ch ?uoc chon
        {
            Sach sach = new Sach();
            int n = 0;
            foreach (var sachs in data.Saches)
            {
                n += 1;

            }
            sach.IDSach = n;
            sach.TenSach = TenSach;
            sach.TheLoai = TheLoai;
            sach.MoTa = MoTa;
            sach.TacGia = TacGia;
            sach.NgayXuatBan = NgayXuatBan;
            sach.SoLuong = SoLuong;
            sach.HinhAnh = HinhAnh;
            return sach;
        }
    }
}
