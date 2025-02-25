using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class ProjectTeKData
    {
        Model1 _context;
        public ProjectTeKData()
        {
            _context = new Model1();
        }
        public List<ProjectTeK> GetListProjectTeK()
        {
            var model = _context.ProjectTeKs
                    .OrderBy(kh => kh.Id)
                    .ToList();
            for (int i = 0; i < model.Count(); i++)
            {
                model[i].GhiChu = (i + 1).ToString();
            }
            model = model
                .OrderByDescending(kh => kh.Id)
                .ToList();
            return model;
        }
    }
}