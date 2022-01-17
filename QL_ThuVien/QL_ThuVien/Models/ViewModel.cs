using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_ThuVien.Models
{
    public class ViewModel
    {
       

        public string TenSach { get; set; }

        public  string TheLoai { get; set; }
        public string MoTa { get; set; }
        public string TacGia { get; set; }
        public string HinhAnh { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public Sach sach { get; set; }
        public PhieuMuon phieumuon { get; set; }
        public CT_PM ctpm { get; set; }
        public IEnumerable<Sach> dsSach { get; set; }
        public string IDSach { get; set; }
        public int? Top5_Quantity { get; set; }
        public int? TongSoLuong { get; set; }
    }
}