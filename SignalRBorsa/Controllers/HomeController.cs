using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace SignalRBorsa.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public List<Model> FillData()
        {
            List<Model> data = new List<Model>();
            Model mod = new Model();
            mod.Name = "ACSEL";
            mod.Son = 21.12;
            mod.Dun = 19.60;
            mod.Yuzde = 4.74;
            mod.Yuksek = 21.30;
            mod.Dusuk = 19.00;
            mod.Ort = 20.59;
            mod.HacimLot = 23.20;
            mod.HacimTl = 2.008;
            data.Add(mod);

            mod = new Model();
            mod.Name = "ADANA";
            mod.Son = 41.30;
            mod.Dun = 39.60;
            mod.Yuzde = 6.74;
            mod.Yuksek = 41.30;
            mod.Dusuk = 69.00;
            mod.Ort = 40.59;
            mod.HacimLot = 14.20;
            mod.HacimTl = 5.008;
            data.Add(mod);

            mod = new Model();
            mod.Name = "ADBGR";
            mod.Son = 14.30;
            mod.Dun = 34.60;
            mod.Yuzde = 6.74;
            mod.Yuksek = 51.30;
            mod.Dusuk = 13.00;
            mod.Ort = 45.59;
            mod.HacimLot = 31.20;
            mod.HacimTl = 3.012;
            data.Add(mod);

           mod = new Model();
            mod.Name = "ADEL";
            mod.Son = 1.30;
            mod.Dun = 23.60;
            mod.Yuzde = 12.74;
            mod.Yuksek = 34.30;
            mod.Dusuk = 17.00;
            mod.Ort = 20.59;
            mod.HacimLot = 27.20;
            mod.HacimTl = 3.008;
            data.Add(mod);

            mod = new Model();
            mod.Name = "ADESE";
            mod.Son = 31.30;
            mod.Dun = 29.60;
            mod.Yuzde = 5.74;
            mod.Yuksek = 31.30;
            mod.Dusuk = 29.00;
            mod.Ort = 30.59;
            mod.HacimLot = 33.20;
            mod.HacimTl = 1.008;
            data.Add(mod);

            return data;
        }
        public ActionResult Index()
        {
            var veri = System.Web.HttpContext.Current.Cache["data"];
            if (veri == null)
            {
                System.Web.HttpContext.Current.Cache.Insert("data", FillData(),null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1000));
                veri = System.Web.HttpContext.Current.Cache["data"];
            }
            return View(veri);
        }

        //
        // GET: /Home/Details/5


        public ActionResult Cms()
        {
            return View(FillData());
        }

        public class Borsa : Hub
        {
            private void setValue(Model list, string name, double dun, double dusuk, double hacimlot, double hacimtl, double ort, double son, double yuksek, double yuzde)
            {
                list.Name = name;
                list.Dun = dun;
                list.Dusuk = dusuk;
                list.HacimLot = hacimlot;
                list.HacimTl = hacimtl;
                list.Ort = ort;
                list.Son = son;
                list.Yuksek = yuksek;
                list.Yuzde = yuzde;
            }

            public void UpdateData(Model data)
            {
                try
                {
                    List<Model> veri = System.Web.HttpContext.Current.Cache["data"] as List<Model>;
                    /*
                         Model item = (from dat in veri where dat.Name == veri.FirstOrDefault().Name select dat).FirstOrDefault<Model>();
                    */
                    (System.Web.HttpContext.Current.Cache["data"] as List<Model>).Where(itm => itm.Name == data.Name).ToList()
                        .ForEach(b => setValue(b, data.Name, data.Dun, data.Dusuk, data.HacimLot, data.HacimTl, data.Ort, data.Son, data.Yuksek, data.Yuzde));

                    //(System.Web.HttpContext.Current.Cache["data"] as List<Model>).Remove((Model)item);
/*                    Model rmvItem = (System.Web.HttpContext.Current.Cache["data"] as List<Model>).Find(itm => itm.Name == data.Name);
                    int index = (System.Web.HttpContext.Current.Cache["data"] as List<Model>).FindIndex(itm => itm.Name == data.Name);
                    (System.Web.HttpContext.Current.Cache["data"] as List<Model>).Remove(rmvItem);

                    item.Name = data.Name;
                    item.Dun = data.Dun;
                    item.Dusuk = data.Dusuk;
                    item.HacimLot = data.HacimLot;
                    item.HacimTl = data.HacimTl;
                    item.Ort = data.Ort;
                    item.Son = data.Son;
                    item.Yuksek = data.Yuksek;
                    item.Yuzde = data.Yuzde;
                    (System.Web.HttpContext.Current.Cache["data"] as List<Model>).Insert(index,item);
 */ 
                    Clients.All.addData(data);
                }
                catch (Exception ex)
                {
 
                }
            } 
            public void ChangeBG(int ID)
            {
                Clients.All.changeBG(ID);
            }             
        }
    }
}
