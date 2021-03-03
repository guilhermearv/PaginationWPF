using Haley.Models;
using HelperControls.Models;
using System;
using System.Windows.Input;

namespace HelperControls.ViewModels
{
    public class VMPagination : ChangeNotifier
    {
        #region Properties
        private PaginationModel _pagination;

        public PaginationModel pagination
        {
            get { return _pagination; }
            set { _pagination = value; onPropertyChanged(); }
        }

        #endregion

        #region Commands
        public ICommand cmd_change_page { get; set; }
        #endregion

        #region CommandMethods
        private void changePage(object obj)
        {
            try
            {
                int parameter = int.Parse((string)obj);
                int newpage = _pagination.current_page;

                switch (parameter)
                {
                    case 0:
                        newpage--;
                        if (newpage < 1) newpage = 1;
                        break;
                    case 1:
                        newpage++;
                        if (newpage > _pagination.total_pages) newpage = _pagination.total_pages;
                        break;
                }                

                _pagination.current_page = newpage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void calculatePagination()
        {
            try
            {
                if (pagination.items_per_page == 0) pagination.items_per_page = 1;
                if (pagination.items_per_page > pagination.total_items) pagination.items_per_page = pagination.total_items;
                pagination.total_pages = pagination.total_items / pagination.items_per_page;
                pagination.current_page = 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Methods
        private void _initiation()
        {
            cmd_change_page = new DelCommand(changePage, null);
        }

        public void seed(PaginationModel pagination)
        {
            _pagination = pagination;
        }

        #endregion

        public VMPagination()
        {
            _initiation();
        }
    }
}
