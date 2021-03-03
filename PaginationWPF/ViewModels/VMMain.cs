using Haley.Models;
using HelperControls.ViewModels;
using HelperControls.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

namespace PaginationWPF.ViewModels
{
    public class VMMain: ChangeNotifier
    {
        private VMPagination _topPagination;

        public VMPagination topPagination
        {
            get { return _topPagination; }
            set { _topPagination = value; onPropertyChanged(); }
        }

        private VMPagination _bottomPagination;

        public VMPagination bottomPagination
        {
            get { return _bottomPagination; }
            set { _bottomPagination = value; onPropertyChanged(); }
        }

        private ObservableCollection<string> _demo_list;

        public ObservableCollection<string> demo_list
        {
            get { return _demo_list; }
            set { _demo_list = value; onPropertyChanged(); }
        }

        private List<string> _full_list;

        public List<string> full_list
        {
            get { return _full_list; }
            set { _full_list = value; onPropertyChanged(); }
        }

        private void temporary_seed()
        {
            try
            {
                List<string> newList = new List<string>();
                string test = "Hello I'm from test Demo. My id is ";
                for (int i = 0; i < 36; i++)
                {
                    newList.Add($@"{test} {i} .");
                }
                full_list = newList;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void processPage(object param)
        {
            try
            {
                List<string> page_list = new List<string>();

                int starting_count = (topPagination.pagination.current_page-1) * topPagination.pagination.items_per_page;
                int ending_count = starting_count + topPagination.pagination.items_per_page;

                if (ending_count > full_list.Count) ending_count = full_list.Count;

                //for (int i = starting_count; i < ending_count; i++)
                //{
                //    page_list.Add(full_list[i]);
                //}

                page_list = full_list.Skip(starting_count).Take(topPagination.pagination.items_per_page).ToList();

                demo_list = new ObservableCollection<string>(page_list);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void _initiation()
        {
            demo_list = new ObservableCollection<string>();
            full_list = new List<string>();
            temporary_seed();

            topPagination = new VMPagination();
            bottomPagination = new VMPagination();
            PaginationModel topPaginationModel = new PaginationModel(full_list.Count, 10);
            PaginationModel bottomPaginationModel = new PaginationModel(2500, 30);
            topPagination.seed(topPaginationModel);
            bottomPagination.seed(bottomPaginationModel);
            topPagination.pagination.PropertyChanged += Pagination_PropertyChanged;
            
            processPage(null);
        }

        private void Pagination_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "current_page")
            {
                processPage(null);
            }
        }

        public VMMain()
        {
            _initiation();
            
        }
    }
}
