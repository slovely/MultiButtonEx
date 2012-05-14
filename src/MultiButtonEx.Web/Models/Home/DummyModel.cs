using System.Collections.Generic;

namespace MultiButtonEx.Web.Models.Home
{
    public class DummyModel
    {
        public DummyModel()
        {
            Items = new List<DummyItem>();
        }

        public IList<DummyItem> Items { get; private set; }
    }

    public class DummyItem
    {
        public int Id { get; set; }
        public string Other { get; set; }
    }
}