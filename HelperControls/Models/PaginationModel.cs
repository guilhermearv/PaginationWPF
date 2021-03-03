using Haley.Models;
using System;

namespace HelperControls.Models
{
    public class PaginationModel : ChangeNotifier
    {
        public int maxiitemsperpage { get; set; }
        private int _total_items;
        public int total_items
        {
            get { return _total_items; }
            set { _total_items = value; onPropertyChanged(); }
        }

        private int _items_per_page;
        public int items_per_page
        {
            get { return _items_per_page; }
            set { _items_per_page = value; onPropertyChanged(); }
        }

        private int _current_page;
        public int current_page
        {
            get { return _current_page; }
            set 
            {
                if (value < total_pages+1 && value > 0)
                {
                    _current_page = value; 
                    onPropertyChanged();
                }
                
            }
        }

        private int _total_pages;
        public int total_pages
        {
            get { return _total_pages; }
            set { _total_pages = value; onPropertyChanged(); }
        }

        private void _initiation(int _totalitemscount, int _pageitemcount)
        {   
            total_items = _totalitemscount;
            items_per_page = _pageitemcount;

            int remainder_check = 0;
            Math.DivRem(total_items, items_per_page, out remainder_check);
            total_pages = total_items / items_per_page;
            if (remainder_check != 0) total_pages++;

            current_page = 1;
            maxiitemsperpage = 15;
        }

        public PaginationModel(int _totalitemscount, int _pageitemcount)
        {
            _initiation(_totalitemscount, _pageitemcount);
        }
    }
}
