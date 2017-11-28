using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBar : ContentPage
    {
        private List<string> _colors = new List<string>
        {
            "Red", "Blue", "Green","Magenta", "Yellow", "Navy", "Orange"
        };

        public SearchBar()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = ColorsSearchBar.Text;
            var suggestions = _colors.Where(c => c.ToLower().Contains(keyword.ToLower()));
            SuggestionListView.ItemsSource = suggestions;
        }
    }
}