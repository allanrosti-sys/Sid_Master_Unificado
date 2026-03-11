using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SID.Standard.Control
{
    public class CollectionView : List<ItemView>
    {
        

    }


    public class ItemView
    {

        public int Id { get; private set; }
        public string Text { get; private set; }

        public ItemView( int id,string text)
        {

            Id = id;
            Text = text;
        }


    }

}
