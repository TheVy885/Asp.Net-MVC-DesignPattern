using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QL_ThuVien.Models;
using System.Collections.Generic;


namespace QL_ThuVien
{
    public sealed class CategorySingleton
    {
        ThuVienEntities3 data = new ThuVienEntities3();
        public static CategorySingleton Instance { get; } = new CategorySingleton();
        public List<TheLoai> listCategory { get; } = new List<TheLoai>();
        public CategorySingleton(){}

        public void Init() 
        {
            if (listCategory.Count == 0)
            {
                var theLoais = data.TheLoais.ToList();
                foreach (var item in theLoais)
                {
                    listCategory.Add(item);
                }
            }
            
        }

        public void Update() 
        {
            listCategory.Clear();
            Init();
        }

    }
}