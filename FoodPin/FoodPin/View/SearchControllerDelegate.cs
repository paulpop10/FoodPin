using System;
using FoodPin.Controller;
using UIKit;

namespace FoodPin.View
{
    public class SearchControllerDelegate : UISearchResultsUpdating
    {
        private Action<string> _updateSearchTextDelegate;
      
        public SearchControllerDelegate(Action<string> updateSearchTextDelegate)
        {
            _updateSearchTextDelegate = updateSearchTextDelegate;
        }

        public override void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            var searchText = searchController.SearchBar.Text;
            _updateSearchTextDelegate(searchText);
        }
    }
}
